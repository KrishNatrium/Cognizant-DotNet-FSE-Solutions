using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FirstWebApi.Filters
{
    public class CustomExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            // Log to a file (e.g., C:\Temp\log.txt)
            var exception = context.Exception;
            var logPath = @"C:\Temp\ExceptionLog.txt";
            Directory.CreateDirectory(Path.GetDirectoryName(logPath)!);  // ensures folder exists
            File.AppendAllText(logPath, $"[{DateTime.Now}] {exception.Message}{Environment.NewLine}");
            // Set the result as 500
            context.Result = new ObjectResult("An unexpected error occurred.")
            {
                StatusCode = 500
            };

            context.ExceptionHandled = true;
        }
    }
}
