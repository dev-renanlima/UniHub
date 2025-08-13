namespace UniHub.Domain.DTOs.Responses.Course;

public class CourseByUserDTO
{
    public string? CourseIdentifier { get; set; }
    public string? CourseName { get; set; }
    public string? CourseCode { get; set; }
    public string? UserName { get; set; }
    public int? NumberOfMembers { get; set; }
    public DateTime? CreationDate { get; set; }
    public DateTime? UpdateDate { get; set; }
}
