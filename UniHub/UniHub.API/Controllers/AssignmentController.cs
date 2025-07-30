using Asp.Versioning;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using UniHub.API.Model.Assignment;
using UniHub.Domain.DTOs;
using UniHub.Domain.DTOs.Responses.Assignment;
using UniHub.Domain.Interfaces.Services;

namespace UniHub.API.Controllers;

[ApiVersion(1.0)]
[ApiController]
[Route("unihub/api/v{version:apiVersion}/assignment")]
public class AssignmentController : ControllerBase
{
    private readonly IAssignmentService _assignmentService;

    public AssignmentController(IAssignmentService assignmentService)
    {
        _assignmentService = assignmentService;
    }

    [HttpPost("createAssignment")]
    public async Task<IActionResult> CreateAssignment(CreateAssignmentModel createAssignmentModel)
    {
        AssignmentDTO assignmentDTO = createAssignmentModel.Adapt<AssignmentDTO>();

        CreateAssignmentResponseDTO? response = await _assignmentService.CreateAsync(assignmentDTO);
        
        return StatusCode(StatusCodes.Status201Created, response);
    }
}