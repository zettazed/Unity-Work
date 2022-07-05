using UnityEditor;
using System.IO;

public class FileManager
{
    public static void WriteStringAtEnd(string path, string text)
    {
        StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine(text);
        writer.Close();
        AssetDatabase.ImportAsset(path);
    }

    public static string ReadFile(string path)
    {
        StreamReader reader = new StreamReader(path);
        string textInFile = reader.ReadToEnd();
        reader.Close();
        return textInFile;
        
    }
}