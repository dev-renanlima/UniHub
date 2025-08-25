namespace UniHub.API.Responses;

public class ProblemResponse
{
    public int StatusCode { get; set; }
    public string? ErrorCode { get; set; }
    public string? Detail { get; set; }
    public string CorrelationId { get; set; } = Guid.NewGuid().ToString("N");
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}
