using Microsoft.AspNetCore.Mvc;

namespace UniHub.API.Model.Course.CreateCourse;

public class CreateCourseModel
{
    [FromBody]
    public CreateCourseBody? Body { get; set; }
}
