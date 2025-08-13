namespace UniHub.Domain.DTOs.Responses.Course;

public class GetCourseResponseDTO
{
    public long CourseId { get; set; }
    public string? CourseName { get; set; }
    public string? CourseCode { get; set; }
    public string? UserName { get; set; }
    public string? UserIdentifier { get; set; }
    public DateTime? CreationDate { get; set; }
    public DateTime? UpdateDate { get; set; }
    public int? NumberOfMembers { get; set; }
    public List<CourseMemberResponseDTO>? CourseMembers { get; set; }
}
