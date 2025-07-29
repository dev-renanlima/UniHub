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

        AddCourseMemberResponseDTO response = await _courseService.AddMemberByCodeAsync(courseMemberDTO);

        return StatusCode(StatusCodes.Status200OK, response);
    }

    [HttpGet("/getCourseByCode/{code}")]
    public async Task<IActionResult> GetCourseByCode([FromRoute] string code)
    {
        GetCourseByCodeResponseDTO? response = await _courseService.GetCourseByCodeAsync(code);

        return StatusCode(StatusCodes.Status200OK, response);
    }

    [HttpGet("/getCoursesByUser/{externalIdentifier}")]
    public async Task<IActionResult> GetCoursesByUser([FromRoute] string externalIdentifier)
    {
        GetCoursesByUserResponseDTO? response = await _courseService.GetCoursesByUserAsync(externalIdentifier);

        return StatusCode(StatusCodes.Status200OK, response);
    }
}