using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using ByteCounterCrawler;

namespace FileUtil
{
    public class FileGenerator
    {
        readonly string _path;
        readonly int _oneMegaByteInBytes = 1048576;
        readonly long _maximumFileSize;
        readonly long _maximumBufferSize;

        private ByteCounter byteCounter = new ByteCounter();

        public long FileSizeBytes { get; private set; }
        public string FileSize
        {
            get
            {
                return HumanizeFileSize(FileSizeBytes);
            }
        }
        public string FileName { get; }

        private Stopwatch _totalTime;
        public TimeSpan TotalTime
        {
            get
            {
                return _totalTime.Elapsed;
            }
        }

        private Stopwatch _totalTimeIterations;
        public int TotalIterations { get; private set; }
        public TimeSpan AvgTimeIterations
        {
            get
            {
                return TimeSpan.FromSeconds(_totalTimeIterations.Elapsed.TotalSeconds / TotalIterations);
            }
        }

        private string HumanizeFileSize(double bytes)
        {
            string[] sizes = { "B", "KB", "MB", "GB", "TB" };
            double len = bytes;
            int order = 0;
            while (len >= 1024 && order < sizes.Length - 1)
            {
                order++;
                len = len / 1024;
            }

            return string.Format("{0:0} {1}", len, sizes[order]);
        }
        
        /// <summary>
        /// Contructor
        /// </summary>
        /// <param name="path">Path of file to be generated</param>
        /// <param name="maximumFileSize">Maximum file size in MB</param>
        /// <param name="maximumBufferSize">Maximun buffer size in MB</param>
        public FileGenerator(string path, long maximumFileSize, long maximumBufferSize)
        {
            _totalTime = new Stopwatch();

            _path = path;
            _maximumFileSize = maximumFileSize * _oneMegaByteInBytes;
            _maximumBufferSize = maximumBufferSize * _oneMegaByteInBytes;

            FileName = $"{DateTime.Now.ToString("yyyy-MM-dd-HHmmss")}-arquivo-gerado.txt";
        }

        /// <summary>
        /// Method to generate the file
        /// </summary>
        /// <param name="text"></param>
        public void GenerateFile(string text)
        {
            if (!Directory.Exists(_path))
                Directory.CreateDirectory(_path);

            var length = byteCounter.Count(text);

            _totalTime = new Stopwatch();
            _totalTime.Start();

            _totalTimeIterations = new Stopwatch();
            TotalIterations = 0;

            var fileText = new StringBuilder();
            var buffer = new StringBuilder();
            using (StreamWriter stream = File.AppendText(Path.Combine(_path, FileName)))
            {
                while (CalculateBytes(fileText.ToString()) < _maximumFileSize)
                {
                    _totalTimeIterations.Start();
                    buffer = GenerateBuffer(text);
                    _totalTimeIterations.Stop();

                    if (CalculateBytes(fileText.ToString(), buffer.ToString()) <= _maximumFileSize)
                    {
                        fileText.Append(buffer);
                        stream.Write(buffer);
                    }
                    else
                        break;

                    TotalIterations++;
                }
            }

            _totalTime.Stop();
            FileSizeBytes = CalculateBytes(fileText.ToString());
        }

        /// <summary>
        /// Method to generate buffer for writing to file
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public StringBuilder GenerateBuffer(string text)
        {
            var bufferText = new StringBuilder();
            var auxBuffer = new StringBuilder(text);

            while (CalculateBytes(bufferText.ToString()) <= _maximumBufferSize)
            {
                //Checking if it is possible to double the buffer
                if (CalculateBytes(bufferText.ToString(), auxBuffer.ToString()) <= _maximumBufferSize)
                {
                    bufferText.Append(auxBuffer);
                    auxBuffer.Append(auxBuffer);
                }
                //Checks if it is still possible to increment the buffer
                else if (CalculateBytes(bufferText.ToString(), text) <= _maximumBufferSize)
                {
                    auxBuffer.Clear();
                    auxBuffer.Append(text);
                    bufferText.Append(auxBuffer);
                }
                else
                    break;
            }

            return bufferText;
        }

        /// <summary>
        /// Method for calculating size of text or result of concatenation of two texts 
        /// </summary>
        /// <param name="textOne"></param>
        /// <param name="textTwo"></param>
        /// <returns></returns>
        public long CalculateBytes(string textOne, string textTwo = null)
        {
            return Encoding.UTF8.GetByteCount(textOne ?? string.Empty) +
                   Encoding.UTF8.GetByteCount(textTwo ?? string.Empty);
        }
    }
}