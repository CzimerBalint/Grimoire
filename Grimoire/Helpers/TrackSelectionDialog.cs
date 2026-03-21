using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Grimoire.Helpers;

public class TrackSelectionDialog : Form
{
    public int SelectedTrackIndex { get; private set; } = 0;
    public bool ApplyToAll { get; private set; } = false;

    private readonly ListBox _trackListBox;
    private readonly CheckBox _applyToAllCheckBox;
    private readonly Button _okButton;

    public TrackSelectionDialog(string fileName, List<string> tracks)
    {
        // Ablak beállításai
        this.Text = "Sáv választása";
        this.Width = 450;
        this.Height = 350;
        this.FormBorderStyle = FormBorderStyle.FixedDialog;
        this.StartPosition = FormStartPosition.CenterParent;
        this.MaximizeBox = false;
        this.MinimizeBox = false;

        // Címke
        var label = new Label
        {
            Text = $"A '{fileName}' fájlban több feliratsáv található.\nMelyik legyen a referencia?",
            Left = 10,
            Top = 10,
            Width = 410,
            Height = 40
        };

        // Lista
        _trackListBox = new ListBox
        {
            Left = 10,
            Top = 50,
            Width = 410,
            Height = 150
        };
        // Feltöltés
        for (int i = 0; i < tracks.Count; i++)
        {
            _trackListBox.Items.Add($"{i}: {tracks[i]}");
        }
        _trackListBox.SelectedIndex = 0;

        // Checkbox (A kért funkció!)
        _applyToAllCheckBox = new CheckBox
        {
            Text = "Ugyanezt a sorszámú sávot használd a többi fájlnál is",
            Left = 10,
            Top = 210,
            Width = 410,
            Checked = false
        };

        // OK Gomb
        _okButton = new Button
        {
            Text = "Kiválasztás",
            Left = 310,
            Top = 260,
            Width = 110,
            DialogResult = DialogResult.OK
        };

        // Gomb esemény
        _okButton.Click += (s, e) =>
        {
            SelectedTrackIndex = _trackListBox.SelectedIndex;
            ApplyToAll = _applyToAllCheckBox.Checked;
        };

        // Hozzáadás a formhoz
        this.Controls.Add(label);
        this.Controls.Add(_trackListBox);
        this.Controls.Add(_applyToAllCheckBox);
        this.Controls.Add(_okButton);
        this.AcceptButton = _okButton;
    }
}