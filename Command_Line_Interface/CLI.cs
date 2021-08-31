using System;
using System.IO;
using Ini_lib;
using Log_file;
using User;

namespace Command_Line_Interface
{
    public class Cli
    {
        public static class Show
        {
            public static void Error(string message)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[ERROR] {message}");
                Console.ResetColor();
            }

            public static void Exeption(string message)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"[Exeption] {message}");
                Console.ResetColor();
            }

            public static void Warning(string message)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"[Warning] {message}");
                Console.ResetColor();
            }

            public static void Info(string message)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"[INFO] {message}");
                Console.ResetColor();
            }

            public static void AppendToLog(string path, string namemethod)
            {
                string path_log = path.Replace("ini", "log");
                UserClass user = new UserClass
                {
                    User = "Alex"
                };
                DateTime dateTime = DateTime.Now;
                LogFile.RecordAction(path_log, $"В {dateTime} " +
                                               $",{user.User} изменил или прочитал файл {path}" +
                                               $", и вызвал метод {namemethod},,");
            }

            public static void Menu(Ini settings, string parametrs)
            {
                Info("Выберите режим работы ");
                Info("1. Прочитать файл");
                Info("2. Записать в файл");
                Info("3. Изменение файла");

                char key = Convert.ToChar(Console.ReadLine());
                switch (key)
                {
                    case '1':
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(settings.Reader());
                        Console.ResetColor();
                        break;
                    case '2':
                        settings.Writer(parametrs);
                        break;
                    case '3':
                        Console.WriteLine("Введите section,key,parametr разделяи их запятыми");
                        string String = Convert.ToString(Console.ReadLine());
                        string[] str = String.Split(',');
                        //settings.Seeker("[Menu2]", "MyNewParametr1", "True ");
                        settings.Seeker(str[0], str[1], str[2] + " ");
                        break;
                }
            }
        }
    }
}
