namespace UniHub.Domain.VOs;

public class CourseVO
{
    public Guid? CourseId { get; set; }
    public string? CourseIdentifier { get; set; }
    public string? CourseName { get; set; }
    public string? CourseCode{ get; set; }
    public Guid? UserId { get; set; }
    public string? UserIdentifier { get; set; }
    public string? UserName { get; set; }
    public long? NumberOfMembers { get; set; }
    public DateTime? CreationDate { get; set; }
    public DateTime? UpdateDate { get; set; }
}
