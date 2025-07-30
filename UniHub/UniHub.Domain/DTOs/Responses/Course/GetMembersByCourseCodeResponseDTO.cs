namespace UniHub.Domain.DTOs.Responses.Course;

public class GetMembersByCourseCodeResponseDTO
{
    public long CourseId { get; set; }
    public string? CourseName { get; set; }
    public string? Code { get; set; }
    public int? NumberOfMembers { get; set; }
    public List<CourseMemberResponseDTO>? CourseMembers { get; set; }
}
