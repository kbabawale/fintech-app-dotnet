using System.Text.Json;
using Fintech_App.Model.DTO;

namespace Fintech_App.Util
{
    public class ErrorStackTrace
    {
        public static string ExceptionToJson(Exception ex)
        {
            var dto = new ErrorStackTraceDTO(
                ex.GetType().FullName!,
                ex.Message,
                ex.StackTrace ?? string.Empty,
                ex.Source ?? string.Empty,
                ex.InnerException?.Message
            );

            return JsonSerializer.Serialize(dto, new JsonSerializerOptions
            {
                WriteIndented = true
            });
        }
    }
}
