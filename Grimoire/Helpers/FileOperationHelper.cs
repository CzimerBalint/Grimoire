using Grimoire.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Grimoire.Helpers;

public static class FileOperationHelper
{
    // Rendezi a listát a fájlnévben található számok alapján
    public static List<FileItem> SortFilesByNumber(IEnumerable<FileItem> items)
    {
        return items
            .OrderBy(item =>
            {
                var match = Regex.Match(item.FileName, @"\d+");
                return match.Success ? int.Parse(match.Value) : int.MaxValue;
            })
            .ThenBy(item => item.FileName)
            .ToList();
    }

    // Tömeges átnevezés logika
    public static void BatchRenameFiles(IEnumerable<FileItem> items, string baseName, int startCounter)
    {
        int counter = startCounter;

        foreach (var item in items)
        {
            if (!File.Exists(item.FullPath)) continue;

            string newName = $"{baseName} {counter:D2}{item.Extension}";
            string newPath = Path.Combine(item.Directory, newName);

            try
            {
                // 1. Fizikai átnevezés
                File.Move(item.FullPath, newPath);

                // 2. Modell frissítése (hogy a UI ne hivatkozzon halott linkre)
                item.FullPath = newPath;

                counter++;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error renaming '{item.FileName}':\n{ex.Message}", "Rename Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}