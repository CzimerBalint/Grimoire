using System.IO;

namespace Grimoire.Models;

public class FileItem
{
    // A 'required' kulcsszó kötelezővé teszi az inicializálást (modern C# feature)
    public required string FullPath { get; set; }

    public string FileName => Path.GetFileName(FullPath);
    public string Extension => Path.GetExtension(FullPath).ToLowerInvariant();
    public string Directory => Path.GetDirectoryName(FullPath) ?? string.Empty;

    // Ez határozza meg, mit lát a felhasználó a ListBoxban
    public override string ToString() => FileName;
}