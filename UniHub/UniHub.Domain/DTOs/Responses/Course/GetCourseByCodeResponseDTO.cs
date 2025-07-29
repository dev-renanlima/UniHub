namespace UniHub.Domain.DTOs.Responses.Course;

public class GetCourseByCodeResponseDTO
{
    public long CourseId { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
}
