using System;
using System.IO;
using System.Text;
using Command_Line_Interface;

namespace Ini_lib
{
    public class Ini
    {
        public string Path { get; set; }

        public Ini(string path)
        {
            Path = path;
        }

        public void CreatDirectory()
        {
            if (Path != null)
            {
                DirectoryInfo Dir = new DirectoryInfo(Path);
                if (!Dir.Exists)
                {
                    Dir.Create();
                }

                Cli.Show.AppendToLog(Path, "CreatDirectory");
            }
        }


        public bool Contains(byte[] file, string section, string key)
        {
            string str = System.Text.Encoding.Default.GetString(file);
            //Блок Exeption
            ///////////////////////////////////////////
            if (!str.Contains(section)) //Exeption Не найденна секция
            {
                Cli.Show.Exeption($"Файл не содержит секцию {section}");
                return false;
            }
            else if (!str.Contains(key)) //Exeption Не найдено ключей в секции
            {
                Cli.Show.Exeption($"По указанной секции - {section} ключ - {key} не найден");
                return false;
            }

            return true;
            ///////////////////////////////////////////
        }

        public void Seeker(string section, string key, string parametr)
        {
            if (Path == null) return;
            bool boolKey = false;
            long offset = 0;

            using (FileStream fileSeeker = new FileStream(Path, FileMode.OpenOrCreate))
            {
                long StreamLength = fileSeeker.Length;
                byte[] array = new byte[fileSeeker.Length];
                fileSeeker.Read(array, 0, array.Length);
                if (!Contains(array, section, key)) return;
                byte[] Key = Encoding.Default.GetBytes(key);


                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(section);
                Console.WriteLine($@"{key} = {parametr}");
                Console.ResetColor();

                for (int i = 0; i < StreamLength; ++i, ++offset)
                {
                    if (array[i] == Key[0] && boolKey == false)
                    {
                        int count = 0;
                        for (int y = 0; y < Key.Length && (i + y) != StreamLength; ++y)
                        {
                            if (array[i + y] == Key[y])
                            {
                                ++count;
                            }
                            else break;

                            if (count == Key.Length)
                            {
                                Console.WriteLine($"True  offset = {offset} endset = {offset + Key.Length} ");
                                offset += Key.Length;
                                if (array[offset] != '=')
                                {
                                    Cli.Show.Warning("Неверный ключ!");
                                    Cli.Show.Exeption("После ключа должен быть символ'='");
                                    return;
                                }

                                ++offset;
                                boolKey = true;
                                break;
                            }
                        }
                    }


                    if (boolKey == true)
                    {
                        Console.WriteLine(offset);
                        //Преобразуем параметр в байты
                        byte[] array2 = System.Text.Encoding.Default.GetBytes(parametr);
                        //Переводим каретку в начало со смещением
                        fileSeeker.Seek(offset, SeekOrigin.Begin);
                        // записываем переведенный в байты аргумент (parametr)
                        fileSeeker.Write(array2, 0, array2.Length);
                        break;
                    }
                }

                Cli.Show.AppendToLog(Path, $"Seeker который изменил ключ {key} на {parametr}\n" +
                                           $"В файле {Path}\n" +
                                           $"Смещение - {offset}");
            }
        }


        public string Reader()
        {
            if (Path == null) return string.Empty;
            string str;

            using (FileStream fileRead = File.OpenRead(Path))
            {
                byte[] array = new byte[fileRead.Length]; // Создаем массив с таким же размером как у потока
                fileRead.Read(array, 0, array.Length); // считываем данные в массив
                str = System.Text.Encoding.Default.GetString(array);
            }

            Cli.Show.AppendToLog(Path, "Reader");
            return str;
        }

        public void Writer(string String)
        {
            if (Path == null) return;

            string[] parametrs = String.Split(',', '/');
            using (FileStream fileWrite = new FileStream(Path, FileMode.Append))
            {
                for (int i = 0; i < parametrs.Length; ++i)
                {
                    byte[] array = System.Text.Encoding.Default.GetBytes(parametrs[i] + '\n');
                    fileWrite.Write(array, 0, array.Length);
                }
            }

            Cli.Show.AppendToLog(Path, "Writer");
        }
    }
}