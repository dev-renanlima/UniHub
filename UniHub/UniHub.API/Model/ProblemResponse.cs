namespace UniHub.API.Model
{
    public class ProblemResponse
    {
        public int StatusCode { get; set; }
        public string? ErrorCode { get; set; }
        public string Detail { get; set; } = string.Empty;
        public string? CorrelationId { get; set; } = Guid.NewGuid().ToString("N");
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
