namespace Fintech_App.Model.DTO
{
    public record ErrorStackTraceDTO(
    string Type,
    string Message,
    string StackTrace,
    string Source,
    string? InnerException
);
}
