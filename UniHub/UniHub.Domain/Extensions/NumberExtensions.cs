namespace UniHub.Domain.Extensions;

public class NumberExtensions
{
    public static long GenerateRandomCodeByDate()
    {
        var today = DateTime.UtcNow;
        string datePart = today.ToString("yyMMdd");

        var random = new Random();
        string randomPart = random.Next(0, 10000).ToString("D4");

        string fullCode = $"{datePart}{randomPart}";

        return long.Parse(fullCode);
    }
}
