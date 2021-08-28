using System;

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
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"[Exeption] {message}");
                    Console.ResetColor();
                }
                public static void Warning(string message)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"[Warning] {message}");
                    Console.ResetColor();
                }
                public static void Info(string message)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine($"[INFO] {message}");
                    Console.ResetColor();
                }

                public static void Menu()
                {
                    Info("Выберите режим работы ");
                    Info("1. Оформление заказа");
                    Info("2. Экспорт списка продуктов");
                    Info("3. Импорт списка продуктов");
                }
            }
    }
}