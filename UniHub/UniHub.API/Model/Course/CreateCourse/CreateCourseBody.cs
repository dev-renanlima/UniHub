using Mapster;

namespace UniHub.API.Model.Course.CreateCourse;

public class AddMemberByCodeBody
{
    public string AdminId { get; set; } = null!;

    public string? Name { get; set; }
}
