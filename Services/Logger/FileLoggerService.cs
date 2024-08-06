using Newtonsoft.Json;

namespace ComputerStore.Services.Logger
{
    public sealed class FileLoggerService : ILoggerService
    {
        private readonly string _logFilePath;
        private static object? _lock;

        public FileLoggerService(string logFilePath)
        {
            _logFilePath = logFilePath;
            _lock = new object();
        }

        public void LogError(Exception ex)
        {
            lock (_lock!)
            {
                var errorLog = new
                {
                    Timestamp = DateTime.UtcNow,
                    ex.Message,
                    ex.StackTrace,
                    ex.InnerException,
                    ex.Source
                };

                List<object> errors;
                if (File.Exists(_logFilePath))
                {
                    var json = File.ReadAllText(_logFilePath);
                    errors = JsonConvert.DeserializeObject<List<object>>(json)!;
                    errors ??= new List<object>();
                }
                else
                    errors = new List<object>();

                errors.Add(errorLog);
                var updatedJson = JsonConvert.SerializeObject(errors, Formatting.Indented);
                File.WriteAllText(_logFilePath, updatedJson);
            }
        }
    }
}
