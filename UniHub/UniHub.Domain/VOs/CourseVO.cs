namespace UniHub.Domain.VOs;

public class CourseVO
{
    public long? CourseId { get; set; }
    public string? CourseName { get; set; }
    public string? CourseCode{ get; set; }
    public long? UserId { get; set; }
    public string? UserIdentifier { get; set; }
    public string? UserName { get; set; }
    public long? NumberOfMembers { get; set; }
    public DateTime? CreationDate { get; set; }
    public DateTime? UpdateDate { get; set; }
}
