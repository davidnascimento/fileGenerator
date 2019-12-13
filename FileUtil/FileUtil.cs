using System;
using System.IO;

namespace FileUtil
{
    public class FileUtil
    {
        readonly string _path;

        public string FileName { get; }

        public FileUtil(string path)
        {
            _path = path;
            Directory.CreateDirectory(_path);
            FileName = $"\\{DateTime.Now.ToString("YYYY-MM-DD-HHmmss")}-arquivo-gerado.txt";
        }

        public void WriteFile(string text)
        {
            using (StreamWriter stream = File.AppendText(_path + FileName))
            {
                stream.Write(text);
            }
        }
    }
}
