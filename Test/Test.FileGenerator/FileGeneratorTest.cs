using System;
using System.IO;
using Xunit;

namespace Test.FileGenerator
{
    public class FileGeneratorTest
    {
        [Theory]
        [InlineData("Desde ontem a noite a otimizacao", "", 32)]
        [InlineData("Desde ontem a noite a otimizacaoa", "Desde ontem a noite a otimizacao", 65)]
        [InlineData("", "", 0)]
        [InlineData(null, null, 0)]
        public void CalculateBytes(string text1, string text2, long expected)
        {
            var fileUtil = new FileUtil.FileGenerator("", 1, 1);
            var lengthBytes = fileUtil.CalculateBytes(text1, text2);
            
            Assert.Equal(expected, lengthBytes);
        }

        [Theory]
        [InlineData("Desde ontem a noite a otimização de performance da renderizaco", 1048576)]
        [InlineData("Desde ontem a noite a otimização de performance da renderizac", 1048572)]
        public void GenerateBuffer(string text, long expected)
        {
            var fileUtil = new FileUtil.FileGenerator("", 1, 1);
            var buffer = fileUtil.GenerateBuffer(text);

            Assert.NotNull(buffer);
            Assert.Equal(expected, fileUtil.CalculateBytes(buffer.ToString()));
        }

        [Theory]
        [InlineData("Desde ontem a noite a otimização de performance da renderizaco", 1, 1, 1048576)]
        [InlineData("Desde ontem a noite a otimização de performance da renderizaco", 2, 2, 2097152)]
        [InlineData("Desde ontem a noite a otimização de performance da renderizaco", 2, 1, 2097152)]
        [InlineData("Desde ontem a noite a otimização de performance da renderizaco", 3, 2, 2097152)]
        [InlineData("Desde ontem a noite a otimização de performance da renderizaco", 1, 2, 0)]
        [InlineData("Desde ontem a noite a otimizaç", 2, 2, 2097150)]
        public void GenerateFile(string text, long FileSizeBytes, long bufferSize, long expected)
        {
            var file = new FileUtil.FileGenerator(AppDomain.CurrentDomain.BaseDirectory, FileSizeBytes, bufferSize);
            file.GenerateFile(text);

            var fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, file.FileName);
            var fileInfo = new FileInfo(fileName);

            Assert.True(File.Exists(fileName));
            Assert.Equal(expected, file.FileSizeBytes);
            Assert.Equal(file.FileSizeBytes, fileInfo.Length);

            File.Delete(fileName);
        }
    }
}
