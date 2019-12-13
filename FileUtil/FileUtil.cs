using System;
using System.IO;

namespace FileUtil
{
    public class FileUtil
    {
        readonly string _path;
        readonly string _fileName;

        public FileUtil(string path)
        { 
            Directory.CreateDirectory(path);
            _path = path;
            _fileName = $"\\{DateTime.Now.ToString("YYYY-MM-DD-HHmmss")}-arquivo-gerado.txt";
        }

        public void WriteFile(string text)
        {
            using (StreamWriter stream = File.AppendText(_path + _fileName))
            {
                stream.Write(text);
            }
        }
    }
}
