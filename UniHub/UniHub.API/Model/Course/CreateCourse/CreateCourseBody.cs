using Mapster;

namespace UniHub.API.Model.Course.CreateCourse;

public class CreateCourseBody
{
    public string AdminId { get; set; } = null!;

    public string? Name { get; set; }
}
