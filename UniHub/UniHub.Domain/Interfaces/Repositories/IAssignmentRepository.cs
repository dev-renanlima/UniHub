using UniHub.Domain.Entities;

namespace UniHub.Domain.Interfaces.Repositories;

public interface IAssignmentRepository
{
    Task<Assignment?> CreateAsync(Assignment assignment);
    Task<AssignmentAttachment?> CreateAssignmentAttachmentAsync(AssignmentAttachment assignmentAttachment);
}
