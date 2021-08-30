using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Command_Line_Interface;
using Ini_lib;

namespace LogerApp
{
    class Program
    {
        static void Main()
        {
            string parametrs = ",[Menu],MyNewParametr=Fasle,twoParametr=70,SizeWindows=800x600";
            Ini settings = new Ini(@"C:\dir\L2.ini");
            Cli.Show.Menu(settings, parametrs);

            //ShowFileStream();
            //ShowFileSeeker();
            //ShowStreamWriter();
            //ShowStreamReader();
        }

        static void ShowFileStream()
        {
            // создаем каталог для файла
            string path = @"C:\Dir";
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }

            Console.WriteLine("Введите строку для записи в файл:");
            string text = Console.ReadLine();

            //запись в файл
            using (FileStream fstream = new FileStream($@"{path}\note.txt", FileMode.Create))
            {
                // преобразуем строку в байты
                byte[] array = System.Text.Encoding.Default.GetBytes(text);
                // запись массива байтов в файл
                fstream.Write(array, 0, array.Length);
                Console.WriteLine("Текст записан в файл");
            }

            // чтение из файла
            using (FileStream fstream = File.OpenRead($@"{path}\note.txt"))
            {
                // Создаем массив с таким же размером как у потока
                byte[] array = new byte[fstream.Length];
                // считываем данные в массив
                fstream.Read(array, 0, array.Length);
                // декодируем байты в строку
                string textFromFile = System.Text.Encoding.Default.GetString(array);
                Console.WriteLine($"Текст из файла: {textFromFile}");
            }

            Console.ReadLine();
            // запись в файл
            // Append:
            //////////// если файл существует, то текст добавляется в конец файл.Если файла нет, то он создается.Файл
            ////////     открывается только для записи.
            // 
            // Create:
            ///////////// создается новый файл.Если такой файл уже существует, то он
            ///////////// перезаписывается
            // 
            // CreateNew:
            //////////// создается новый файл.Если такой файл уже существует, то он приложение выбрасывает ошибку
            //
            // Open:
            //////////// открывает файл.Если файл не существует, выбрасывается исключение
            //
            // OpenOrCreate:
            //////////// если файл существует, он открывается, если нет - создается новый
            //
            // Truncate:
            //////////// если файл существует, то он перезаписывается.Файл открывается только для записи.
        }

        static void ShowFileSeeker()
        {
            string text = "hello world";

            // запись в файл
            using (FileStream fstream = new FileStream(@"C:\dir\note.dat", FileMode.OpenOrCreate))
            {
                // преобразуем строку в байты
                byte[] input = Encoding.Default.GetBytes(text);
                // запись массива байтов в файл
                fstream.Write(input, 0, input.Length);
                Console.WriteLine("Текст записан в файл");

                // перемещаем указатель в конец файла, до конца файла- пять байт
                fstream.Seek(-5, SeekOrigin.End); // минус 5 символов с конца потока


                // считываем четыре символов с текущей позиции
                byte[] output = new byte[5];
                fstream.Read(output, 0, output.Length);
                // декодируем байты в строку
                string textFromFile = Encoding.Default.GetString(output);
                Console.WriteLine($"Текст из файла: {textFromFile}"); // worl

                // заменим в файле слово world на слово house
                string replaceText = "house";
                fstream.Seek(-5, SeekOrigin.End); // минус 5 символов с конца потока
                input = Encoding.Default.GetBytes(replaceText);
                fstream.Write(input, 0, input.Length);

                // считываем весь файл
                // возвращаем указатель в начало файла
                fstream.Seek(0, SeekOrigin.Begin);
                output = new byte[fstream.Length];
                fstream.Read(output, 0, output.Length);
                // декодируем байты в строку
                textFromFile = Encoding.Default.GetString(output);
                Console.WriteLine($"Текст из файла: {textFromFile}"); // hello house
            }

            Console.Read();
        }

        static void ShowStreamWriter()
        {
            string writePath = $@"C:\dir\hta";
            DirectoryInfo dirInfo = new DirectoryInfo(writePath);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }

            string text = @"Привет мир!\nПока мир...";
            try
            {
                using (StreamWriter sw = new StreamWriter($@"{writePath}\note.txt", false,
                    System.Text.Encoding.Default))
                {
                    sw.WriteLine(text);
                }

                using (StreamWriter sw = new StreamWriter($@"{writePath}\note.txt", true, System.Text.Encoding.Default))
                {
                    sw.WriteLine("Дозапись");
                    sw.Write(4.5);
                }

                Console.WriteLine("Запись выполнена");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static void ShowStreamReader()
        {
            string ReadPath = $@"C:\dir\hta\note.txt";

            try
            {
                //Полное считывание до конца
                using (StreamReader sr = new StreamReader(ReadPath, System.Text.Encoding.Default))
                {
                    Console.WriteLine(sr.ReadToEnd());
                }

                //Построчное считывание
                using (StreamReader sr = new StreamReader(ReadPath, System.Text.Encoding.Default))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        Console.WriteLine(line);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}