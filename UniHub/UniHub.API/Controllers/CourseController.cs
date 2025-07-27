using Asp.Versioning;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using UniHub.API.Model.Course.AddMemberByCode;
using UniHub.API.Model.Course.CreateCourse;
using UniHub.Domain.DTOs;
using UniHub.Domain.DTOs.Responses.Course;
using UniHub.Domain.Interfaces.Services;

namespace UniHub.API.Controllers;

[ApiVersion(1.0)]
[ApiController]
[Route("unihub/api/v{version:apiVersion}/course")]
public class CourseController : ControllerBase
{
    private readonly ICourseService _courseService;

    public CourseController(ICourseService courseService)
    {
        _courseService = courseService;
    }

    [HttpPost("/createCourse")]
    public async Task<IActionResult> CreateCourse(CreateCourseModel createCourseModel)
    {
        CourseDTO courseDTO = createCourseModel.Adapt<CourseDTO>();

        CreateCourseResponseDTO response = await _courseService.CreateAsync(courseDTO);

        return StatusCode(StatusCodes.Status201Created, response);
    }

    [HttpPost("/addMemberByCode")]
    public async Task<IActionResult> AddMemberByCode(AddMemberByCodeModel addMemberByCodeModel)
    {
        CourseMemberDTO courseMemberDTO = addMemberByCodeModel.Adapt<CourseMemberDTO>();

        AddCourseMemberResponseDTO response = await _courseService.AddMemberByCode(courseMemberDTO);

        return StatusCode(StatusCodes.Status200OK, response);
    }
}