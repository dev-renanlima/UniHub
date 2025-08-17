using Mapster;

namespace UniHub.API.Model.Course.CreateCourse;

public class CreateCourseBody
{
    public string UserIdentifier { get; set; } = null!;

    public string? Name { get; set; }
}
