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
            Assert.Equal(1048576, fileUtil.CalculateBytes(bufferEven.ToString()));
            Assert.Equal(1048572, fileUtil.CalculateBytes(bufferOdd.ToString()));
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
