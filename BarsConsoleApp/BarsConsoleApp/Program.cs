using System;
using System.IO;

namespace BarsConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //путь
            string path = @"C:\Users\marty\Desktop\Log.txt";

            Exception exception = new OutOfMemoryException();

            var log1 = new LocalFileLogger<string>(path);
            log1.LogInfo("Привет");
            log1.LogWarning("Предупреждение");
            log1.LogError("Ошибка", exception);

            var log2 = new LocalFileLogger<int>(path);
            log2.LogInfo("Привет");
            log2.LogWarning("Предупреждение");
            log2.LogError("Ошибка", exception);

            var log3 = new LocalFileLogger<bool>(path);
            log3.LogInfo("Привет");
            log3.LogWarning("Предупреждение");
            log3.LogError("Ошибка", exception);

            Console.ReadLine();
        }
    }
   
    public class LocalFileLogger<TLog> : ILogger
    {
        private string _path;
        private string GenericTypeName = typeof(TLog).Name;
        private DateTime localDate = DateTime.Now;

        public LocalFileLogger(string path)
        {
            _path = path;
            if (true)
            {
                if (!File.Exists(path))
                {
                    string startLog = "Начало логов\n";
                    File.WriteAllText(path, startLog);
                }
            }
        }

        public void LogInfo(string message)
        {
            File.AppendAllText(_path,$"[{localDate}][Info] : [{GenericTypeName}] : {message}\n");
            Console.WriteLine("Info успешно");
        }
        public void LogWarning(string message)
        {
            File.AppendAllText(_path, $"[{localDate}][Warning] : [{GenericTypeName}] : {message}\n");
            Console.WriteLine("Warning успешно");
        }
        public void LogError(string message, Exception ex)
        {
            File.AppendAllText(_path, $"[{localDate}][Error] : [{GenericTypeName}] : {message}.{ex.Message}\n");
            Console.WriteLine("Error успешно");
        }
    }

    public interface ILogger
    {
        void LogInfo(string message);
        void LogWarning(string message);
        void LogError(string message, Exception ex);
    }
   
}
