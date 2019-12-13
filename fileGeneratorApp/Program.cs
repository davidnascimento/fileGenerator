using System;
using System.IO;

namespace fileGeneratorApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Informe o tamanho do arquivo a ser gerado! Default(100MB) ");
            int.TryParse(Console.ReadLine(), out int fileSize);

            if (fileSize <= 0)
            {
                Console.WriteLine("Tamanho de arquivo invalido! Sera considerado o valor default 100MB!");
                fileSize = 100;
            }

            Console.WriteLine("Informe o tamanho do buffer! Default(1MB)");
            int.TryParse(Console.ReadLine(), out int bufferSize);

            if (bufferSize <= 0)
            {
                Console.WriteLine("Tamanho do buffer invalido! Sera considerado o valor default 1MB!");
                bufferSize = 1;
            }

            var path = string.Empty;
            var existPath = false;
            while (!existPath)
            {
                Console.WriteLine("Informe o caminho para geracao do arquivo!");
                path = Console.ReadLine();

                if (!Directory.Exists(path))
                    Console.WriteLine("Caminho invalido, favor informar um caminho valido!");
            }

            //var fileUtil = new FileUtil.FileUtil(path);

            //fileUtil.WriteFile("Texto");

            Console.ReadKey();
        }
    }
}
