using UniHub.Domain.DTOs;
using UniHub.Domain.DTOs.Responses.Assignment;

namespace UniHub.Domain.Interfaces.Services;

public interface IAssignmentService
{
    Task<CreateAssignmentResponseDTO?> CreateAsync(AssignmentDTO assignmentDTO);
}
