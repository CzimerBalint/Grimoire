using Grimoire.Helpers;
using Grimoire.Models;
using Grimoire.Services;
using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grimoire;

public partial class MainForm : Form
{
    private readonly BindingList<FileItem> _fileList = new();
    private readonly SubtitleSyncService _syncService = new();

    public MainForm()
    {
        InitializeComponent();
        InitializeCustomComponents();
    }

    private void InitializeCustomComponents()
    {
        files_listBox.DataSource = _fileList;
        files_listBox.AllowDrop = true;

        files_listBox.DragEnter += FilesListBox_DragEnter;
        files_listBox.DragDrop += FilesListBox_DragDrop;

        selectButton.Click += selectButton_Click;
        removeButton.Click += removeButton_Click;
        sortButton.Click += sortButton_Click;
        renameButton.Click += renameButton_Click;
        syncButton.Click += syncButton_Click;

        if (modeComboBox.Items.Count == 0)
        {
            modeComboBox.Items.Add("Hang alapú (Beszédfelismerés)");
            modeComboBox.Items.Add("Felirat alapú (Sáv kinyerés)");
            modeComboBox.SelectedIndex = 0;
        }
        else
        {
            modeComboBox.SelectedIndex = 0;
        }

        // ComboBox csak olvashatóvá tétele (választani lehet, írni nem)
        modeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;

        this.Text = "Grimoire - Média Varázsló";
    }

    // --- DRAG & DROP LOGIKA ---
    private void FilesListBox_DragEnter(object sender, DragEventArgs e)
    {
        if (e.Data != null && e.Data.GetDataPresent(DataFormats.FileDrop))
            e.Effect = DragDropEffects.Copy;
        else
            e.Effect = DragDropEffects.None;
    }

    private void FilesListBox_DragDrop(object sender, DragEventArgs e)
    {
        if (e.Data == null) return;
        var paths = (string[]?)e.Data.GetData(DataFormats.FileDrop);
        if (paths == null) return;

        foreach (var path in paths)
        {
            if (File.Exists(path) && !_fileList.Any(x => x.FullPath == path))
            {
                _fileList.Add(new FileItem { FullPath = path });
            }
        }
    }

    // --- GOMB ESEMÉNYEK ---
    private void selectButton_Click(object? sender, EventArgs e)
    {
        using var ofd = new OpenFileDialog { Multiselect = true, Title = "Médiafájlok kiválasztása" };
        if (ofd.ShowDialog() == DialogResult.OK)
        {
            foreach (var path in ofd.FileNames)
            {
                if (!_fileList.Any(x => x.FullPath == path))
                    _fileList.Add(new FileItem { FullPath = path });
            }
        }
    }

    private void removeButton_Click(object? sender, EventArgs e)
    {
        if (files_listBox.SelectedIndex == -1) return;
        var selectedItems = files_listBox.SelectedItems.Cast<FileItem>().ToList();
        foreach (var item in selectedItems) _fileList.Remove(item);
    }

    private void sortButton_Click(object? sender, EventArgs e)
    {
        if (_fileList.Count < 2) return;
        var sorted = FileOperationHelper.SortFilesByNumber(_fileList);
        _fileList.Clear();
        foreach (var item in sorted) _fileList.Add(item);
    }

    private void renameButton_Click(object? sender, EventArgs e)
    {
        if (_fileList.Count == 0)
        {
            MessageBox.Show("A lista üres.", "Teendő szükséges", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        string baseName = nameTextBox.Text.Trim();
        if (string.IsNullOrEmpty(baseName))
        {
            MessageBox.Show("Kérlek, adj meg egy alapnevet.", "Bemenet szükséges", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            nameTextBox.Focus();
            return;
        }

        char[] invalidChars = Path.GetInvalidFileNameChars();
        if (baseName.IndexOfAny(invalidChars) >= 0)
        {
            MessageBox.Show("A név érvénytelen karaktereket tartalmaz.", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        try
        {
            FileOperationHelper.BatchRenameFiles(_fileList, baseName, (int)numberNumericUpDown.Value);
            _fileList.ResetBindings();
            MessageBox.Show("Sikeres átnevezés!", "Siker", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Hiba: {ex.Message}", "Kritikus hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    // --- A JAVÍTOTT SZINKRONIZÁLÁS LOGIKA ---
    private async void syncButton_Click(object? sender, EventArgs e)
    {
        if (!_syncService.ValidateTools())
        {
            MessageBox.Show("Hiányzó 'ffmpeg.exe' vagy 'alass.exe'!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        var videos = _fileList.Where(f => new[] { ".mkv", ".mp4", ".avi", ".webm" }.Contains(f.Extension)).ToList();
        var subtitles = _fileList.Where(f => new[] { ".srt", ".ass", ".ssa" }.Contains(f.Extension)).ToList();

        if (videos.Count == 0 || subtitles.Count == 0)
        {
            MessageBox.Show("Nincs elég fájl (min. 1 videó és 1 felirat kell).", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (videos.Count != subtitles.Count)
        {
            if (MessageBox.Show($"Darabszám eltérés! (Videó: {videos.Count}, Felirat: {subtitles.Count}). Folytatod?", "Figyelem", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;
        }

        int maxCount = Math.Min(videos.Count, subtitles.Count);
        progressBar1.Maximum = maxCount;
        progressBar1.Value = 0;
        progressBar1.Visible = true;
        syncButton.Enabled = false;

        bool useSubtitleReference = modeComboBox.SelectedIndex == 1;

        // Változók a "Mindegyikre alkalmaz" funkcióhoz
        bool applyToAllTracks = false;
        int savedTrackIndex = 0;

        try
        {
            for (int i = 0; i < maxCount; i++)
            {
                var video = videos[i];
                var sub = subtitles[i];
                int currentTrackIndex = 0;

                // Ha felirat alapú a mód, le kell kérdezni a sávokat
                if (useSubtitleReference)
                {
                    var tracks = await _syncService.GetSubtitleTracksAsync(video.FullPath);

                    if (tracks.Count > 1)
                    {
                        // Ha korábban bepipálták a "mindegyikre" opciót:
                        if (applyToAllTracks)
                        {
                            // Biztonsági ellenőrzés: ha a mentett index (pl. 2) nem létezik ebben a videóban, visszaváltunk 0-ra
                            if (savedTrackIndex < tracks.Count)
                                currentTrackIndex = savedTrackIndex;
                            else
                                currentTrackIndex = 0;
                        }
                        else
                        {
                            // Ha még nem választottak, vagy nem pipálták be: Feldobjuk az ablakot
                            using var dialog = new TrackSelectionDialog(video.FileName, tracks);
                            if (dialog.ShowDialog() == DialogResult.OK)
                            {
                                currentTrackIndex = dialog.SelectedTrackIndex;
                                applyToAllTracks = dialog.ApplyToAll; // Megjegyezzük a pipát
                                savedTrackIndex = currentTrackIndex;  // Megjegyezzük az indexet
                            }
                            else
                            {
                                // Ha a Mégse gombra nyomott, ugorjuk ezt a fájlt, vagy állítsuk le? 
                                // Most csak az alapértelmezett 0-t használjuk.
                                currentTrackIndex = 0;
                            }
                        }
                    }
                }

                bool success = await _syncService.SyncSubtitleAsync(video.FullPath, sub.FullPath, 0);
                if (!success) Debug.WriteLine($"Hiba: {video.FileName}");

                progressBar1.Value = i + 1;
            }

            MessageBox.Show("Kész!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Hiba: {ex.Message}", "Kritikus hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            syncButton.Enabled = true;
            progressBar1.Visible = false;
        }
    }
}