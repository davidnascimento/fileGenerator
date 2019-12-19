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
        public void GenerateFile()
        {
            //Tamanho e buffer impar
            var path1 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "1");
            var file1 = new FileUtil.FileGenerator(path1, 1, 1);
            file1.GenerateFile("Desde ontem a noite a otimização de performance da renderizaco");

            var fileName1 = Path.Combine(path1, file1.FileName);
            var fileInfo1 = new FileInfo(fileName1);

            //Tamanho e buffer par
            var path2 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "2");
            var file2 = new FileUtil.FileGenerator(path2, 2, 2);
            file2.GenerateFile("Desde ontem a noite a otimização de performance da renderizaco");

            var fileName2 = Path.Combine(path2, file2.FileName);
            var fileInfo2 = new FileInfo(fileName2);

            //Tamannho do arquivo par e buffer impar
            var path3 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "3");
            var file3 = new FileUtil.FileGenerator(path3, 2, 1);
            file3.GenerateFile("Desde ontem a noite a otimização de performance da renderizaco");

            var fileName3 = Path.Combine(path3, file3.FileName);
            var fileInfo3 = new FileInfo(fileName3);

            //Tamanho arquivo impar e buffer par
            var path4 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "4");
            var file4 = new FileUtil.FileGenerator(path4, 3, 2);
            file4.GenerateFile("Desde ontem a noite a otimização de performance da renderizaco");

            var fileName4 = Path.Combine(path4, file4.FileName);
            var fileInfo4 = new FileInfo(fileName4);
           
            //Buffer Maior que Arquivo
            var path5 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "5");
            var file5 = new FileUtil.FileGenerator(path5, 1, 2);
            file5.GenerateFile("Desde ontem a noite a otimização de performance da renderizaco");

            var fileName5 = Path.Combine(path5, file5.FileName);
            var fileInfo5 = new FileInfo(fileName5);

            //Buffer Impar
            var path6 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "6");
            var file6 = new FileUtil.FileGenerator(path6, 2, 2);
            file6.GenerateFile("Desde ontem a noite a otimizaç");

            var fileName6 = Path.Combine(path6, file6.FileName);
            var fileInfo6 = new FileInfo(fileName6);

            //

            Assert.True(File.Exists(fileName1));
            Assert.Equal(1048576, fileInfo1.Length);

            Assert.True(File.Exists(fileName2));
            Assert.Equal(2097152, fileInfo2.Length);

            Assert.True(File.Exists(fileName3));
            Assert.Equal(2097152, fileInfo3.Length);

            Assert.True(File.Exists(fileName4));
            Assert.Equal(2097152, fileInfo4.Length);

            Assert.True(File.Exists(fileName5));
            Assert.Equal(0, fileInfo5.Length);

            Assert.True(File.Exists(fileName6));
            Assert.Equal(2097150, fileInfo6.Length);

            Directory.Delete(path1, true);
            Directory.Delete(path2, true);
            Directory.Delete(path3, true);
            Directory.Delete(path4, true);
            Directory.Delete(path5, true);
            Directory.Delete(path6, true);
        }
    }
}
