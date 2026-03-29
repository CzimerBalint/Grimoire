using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace Grimoire.Services;

public class SubtitleSyncService
{
    private readonly string _ffmpegPath;
    private readonly string _alassPath;

    public SubtitleSyncService()
    {
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;
        _ffmpegPath = Path.Combine(baseDir, "ffmpeg.exe");
        _alassPath = Path.Combine(baseDir, "alass.exe");
    }

    public bool ValidateTools()
    {
        return File.Exists(_ffmpegPath) && File.Exists(_alassPath);
    }

    // ---- FELIRAT TRACK LISTA ----
    public async Task<List<string>> GetSubtitleTracksAsync(string videoPath)
    {
        var tracks = new List<string>();

        if (!ValidateTools())
            return tracks;

        await Task.Run(() =>
        {
            try
            {
                var psi = new ProcessStartInfo
                {
                    FileName = _ffmpegPath,
                    Arguments = $"-i \"{videoPath}\"",
                    UseShellExecute = false,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                };

                using var p = Process.Start(psi);
                if (p == null) return;

                string output = p.StandardError.ReadToEnd();
                p.WaitForExit();

                var lines = output.Split('\n');

                foreach (var line in lines)
                {
                    if (line.Contains("Subtitle:"))
                        tracks.Add(line.Trim());
                }
            }
            catch { }
        });

        return tracks;
    }

    // ---- SZINKRONIZÁLÁS FELIRAT ALAPJÁN ----
    public async Task<bool> SyncSubtitleAsync(string videoPath, string targetSubPath, int subtitleTrackIndex)
    {
        if (!ValidateTools()) return false;

        return await Task.Run(() =>
        {
            string tempId = Guid.NewGuid().ToString();
            string tempDir = Path.GetTempPath();

            // A kinyert referencia felirat és az elkészült felirat helye
            string tempRefSub = Path.Combine(tempDir, $"ref_{tempId}.ass");
            string tempSynced = Path.Combine(tempDir, $"synced_{tempId}.ass");

            try
            {
                // 1. Referencia felirat kinyerése a videóból (-map 0:s az audió helyett)
                string extractArgs = $"-i \"{videoPath}\" -map 0:s:{subtitleTrackIndex} \"{tempRefSub}\" -y";

                ExecuteProcess(_ffmpegPath, extractArgs);

                if (!File.Exists(tempRefSub))
                    return false;

                // 2. ALASS szinkron: (kinyert videós felirat -> magyar felirat -> új szinkronizált felirat)
                string alassArgs = $"\"{tempRefSub}\" \"{targetSubPath}\" \"{tempSynced}\"";

                ExecuteProcess(_alassPath, alassArgs);

                if (!File.Exists(tempSynced))
                    return false;

                // 3. Eredeti célfelirat felülírása a kész, időzített verzióval
                File.Copy(tempSynced, targetSubPath, true);

                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                // 4. Ideiglenes fájlok takarítása
                try
                {
                    if (File.Exists(tempRefSub)) File.Delete(tempRefSub);
                    if (File.Exists(tempSynced)) File.Delete(tempSynced);
                }
                catch { }
            }
        });
    }

    private void ExecuteProcess(string fileName, string arguments)
    {
        using var p = Process.Start(new ProcessStartInfo
        {
            FileName = fileName,
            Arguments = arguments,
            UseShellExecute = false,
            CreateNoWindow = true
        });

        p?.WaitForExit();
    }
}