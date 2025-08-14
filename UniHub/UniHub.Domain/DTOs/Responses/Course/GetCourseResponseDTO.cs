using System.Text.Json.Serialization;

namespace UniHub.Domain.DTOs.Responses.Course;

public record GetCourseResponseDTO
{
    [JsonIgnore]
    public Guid Id { get; set; }

    public string? CourseIdentifier { get; set; }
    public string? CourseName { get; set; }
    public string? CourseCode { get; set; }
    public string? UserName { get; set; }
    public string? UserIdentifier { get; set; }
    public DateTime? CreationDate { get; set; }
    public DateTime? UpdateDate { get; set; }
    public int? NumberOfMembers { get; set; }
    public List<CourseMemberResponseDTO>? CourseMembers { get; set; }
}
