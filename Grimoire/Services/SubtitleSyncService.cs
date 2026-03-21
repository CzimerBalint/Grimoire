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

    // ---- FELIRAT TRACK LISTA (egyszerű) ----
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

    // ---- AUDIO TRACK (dummy, hogy ne legyen hiba) ----
    public Task<List<string>> GetAudioTracksAsync(string videoPath)
    {
        return Task.FromResult(new List<string>());
    }

    // ---- SZINKRONIZÁLÁS ----
    public async Task<bool> SyncSubtitleAsync(string videoPath, string subPath, int audioTrackIndex)
    {
        if (!ValidateTools()) return false;

        return await Task.Run(() =>
        {
            string tempId = Guid.NewGuid().ToString();
            string tempDir = Path.GetTempPath();

            string tempAudio = Path.Combine(tempDir, $"ref_{tempId}.wav");
            string tempSynced = Path.Combine(tempDir, $"synced_{tempId}.ass");

            try
            {
                // 1. referencia hang kinyerése
                string extractArgs =
                    $"-i \"{videoPath}\" -map 0:a:{audioTrackIndex} -vn -af \"highpass=f=400,lowpass=f=3400\" -ac 1 -ar 16000 \"{tempAudio}\" -y";

                ExecuteProcess(_ffmpegPath, extractArgs);

                if (!File.Exists(tempAudio))
                    return false;

                // 2. ALASS szinkron
                string alassArgs =
                    $"\"{tempAudio}\" \"{subPath}\" \"{tempSynced}\"";

                ExecuteProcess(_alassPath, alassArgs);

                if (!File.Exists(tempSynced))
                    return false;

                // 3. eredeti felirat felülírása
                File.Copy(tempSynced, subPath, true);

                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                try
                {
                    if (File.Exists(tempAudio)) File.Delete(tempAudio);
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