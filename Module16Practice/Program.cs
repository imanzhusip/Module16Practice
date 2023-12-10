using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module16Practice
{
    public class Program
    {
        static void Main()
        {
            try
            {
                Console.WriteLine("Введите путь к отслеживаемой директории:");
                string directoryPath = Console.ReadLine();
                Console.WriteLine("Введите путь к лог-файлу:");
                string logFilePath = Console.ReadLine();
                using (var watcher = new FileSystemWatcher(directoryPath))
                {
                    watcher.IncludeSubdirectories = true; 
                    watcher.EnableRaisingEvents = true;
                    watcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName;
                    watcher.Changed += (sender, e) => LogChange("Changed", e.FullPath, logFilePath);
                    watcher.Created += (sender, e) => LogChange("Created", e.FullPath, logFilePath);
                    watcher.Deleted += (sender, e) => LogChange("Deleted", e.FullPath, logFilePath);
                    watcher.Renamed += (sender, e) => LogChange($"Переименован из {e.OldFullPath}", e.FullPath, logFilePath);
                    Console.WriteLine($"Отслеживание изменений в директории {directoryPath} запущено. Лог будет записан в {logFilePath}.");
                    Console.ReadLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }
        static void LogChange(string changeType, string fullPath, string logFilePath)
        {
            try
            {
                string logMessage = $"{DateTime.Now} - {changeType}: {fullPath}";
                Console.WriteLine(logMessage);
                File.AppendAllText(logFilePath, logMessage + Environment.NewLine);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при записи в лог-файл: {ex.Message}");
            }
        }
    }
}
