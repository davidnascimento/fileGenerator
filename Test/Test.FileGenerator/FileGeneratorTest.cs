using System;
using System.IO;
using Xunit;

namespace Test.FileGenerator
{
    public class FileGeneratorTest
    {
        [Fact]
        public void CalculateBytes()
        {
            var fileUtil = new FileUtil.FileGenerator("", 1, 1);
            var lengthBytes = fileUtil.CalculateBytes("Desde ontem a noite a otimizacao", "");
            var sumSize = fileUtil.CalculateBytes("Desde ontem a noite a otimizacaoa", "Desde ontem a noite a otimizacao");
            var emptySize = fileUtil.CalculateBytes("", "");
            var stringNull = fileUtil.CalculateBytes(null, null);

            Assert.Equal(32, lengthBytes);
            Assert.Equal(65, sumSize);
            Assert.Equal(0, emptySize);
            Assert.Equal(0, stringNull);
        }

        [Fact]
        public void GenerateBuffer()
        {
            var fileUtil = new FileUtil.FileGenerator("", 1, 1);
            var bufferEven = fileUtil.GenerateBuffer("Desde ontem a noite a otimização de performance da renderizaco");
            var bufferOdd = fileUtil.GenerateBuffer("Desde ontem a noite a otimização de performance da renderizac");

            Assert.NotNull(bufferEven);
            Assert.NotNull(bufferOdd);
            Assert.NotEmpty(bufferEven?.ToString());
            Assert.NotEmpty(bufferOdd?.ToString());
            Assert.Equal(1048576, fileUtil.CalculateBytes(bufferEven.ToString()));
            Assert.Equal(1048572, fileUtil.CalculateBytes(bufferOdd.ToString()));
        }

        [Fact]
        public void GenerateFileOddFileSizeAndOddBuffer()
        {
            var file = new FileUtil.FileGenerator(AppDomain.CurrentDomain.BaseDirectory, 1, 1);
            file.GenerateFile("Desde ontem a noite a otimização de performance da renderizaco");

            var fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, file.FileName);
            var fileInfo = new FileInfo(fileName);

            Assert.True(File.Exists(fileName));
            Assert.Equal(1048576, file.FileSize);
            Assert.Equal(file.FileSize, fileInfo.Length);

            File.Delete(fileName);
        }

        [Fact]
        public void GenerateFileEvenFileSizeAndEvenBuffer()
        {
            var file = new FileUtil.FileGenerator(AppDomain.CurrentDomain.BaseDirectory, 2, 2);
            file.GenerateFile("Desde ontem a noite a otimização de performance da renderizaco");

            var fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, file.FileName);
            var fileInfo = new FileInfo(fileName);

            Assert.True(File.Exists(fileName));
            Assert.Equal(2097152, fileInfo.Length);
            Assert.Equal(file.FileSize, fileInfo.Length);

            File.Delete(fileName);
        }

        [Fact]
        public void GenerateFileEvenFileAndOddBuffer()
        {
            var file = new FileUtil.FileGenerator(AppDomain.CurrentDomain.BaseDirectory, 2, 1);
            file.GenerateFile("Desde ontem a noite a otimização de performance da renderizaco");

            var fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, file.FileName);
            var fileInfo = new FileInfo(fileName);

            Assert.True(File.Exists(fileName));
            Assert.Equal(2097152, fileInfo.Length);
            Assert.Equal(file.FileSize, fileInfo.Length);

            File.Delete(fileName);
        }

        [Fact]
        public void GenerateFileOddFileSizeAndEvenBuffer()
        {
            var file = new FileUtil.FileGenerator(AppDomain.CurrentDomain.BaseDirectory, 3, 2);
            file.GenerateFile("Desde ontem a noite a otimização de performance da renderizaco");

            var fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, file.FileName);
            var fileInfo = new FileInfo(fileName);

            Assert.True(File.Exists(fileName));
            Assert.Equal(2097152, fileInfo.Length);
            Assert.Equal(file.FileSize, fileInfo.Length);

            File.Delete(fileName);
        }

        [Fact]
        public void GenerateFileBufferLargerThanFileSize()
        {
            var file = new FileUtil.FileGenerator(AppDomain.CurrentDomain.BaseDirectory, 1, 2);
            file.GenerateFile("Desde ontem a noite a otimização de performance da renderizaco");

            var fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, file.FileName);
            var fileInfo = new FileInfo(fileName);

            Assert.True(File.Exists(fileName));
            Assert.Equal(0, fileInfo.Length);
            Assert.Equal(file.FileSize, fileInfo.Length);

            File.Delete(fileName);
        }

        [Fact]
        public void GenerateFileWithOddTextSize()
        {
            var file = new FileUtil.FileGenerator(AppDomain.CurrentDomain.BaseDirectory, 2, 2);
            file.GenerateFile("Desde ontem a noite a otimizaç");

            var fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, file.FileName);
            var fileInfo = new FileInfo(fileName);

            Assert.True(File.Exists(fileName));
            Assert.Equal(2097150, fileInfo.Length);
            Assert.Equal(file.FileSize, fileInfo.Length);

            File.Delete(fileName);
        }
    }
}
