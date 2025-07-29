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
public class NewsController : ControllerBase
{
    private readonly ICourseService _courseService;

    public NewsController(ICourseService courseService)
    {
        _courseService = courseService;
    }
}