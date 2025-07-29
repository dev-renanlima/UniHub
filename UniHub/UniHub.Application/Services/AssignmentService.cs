using Mapster;
using UniHub.Domain.DTOs;
using UniHub.Domain.DTOs.Responses.Assignment;
using UniHub.Domain.Entities;
using UniHub.Domain.Enums;
using UniHub.Domain.Interfaces.Repositories;
using UniHub.Domain.Interfaces.Services;

namespace UniHub.Application.Services
{
    public class AssignmentService : IAssignmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICourseService _courseService;

        public AssignmentService(IUnitOfWork unitOfWork, ICourseService courseService)
        {
            _unitOfWork = unitOfWork;
            _courseService = courseService;
        }

        public async Task<CreateAssignmentResponseDTO?> CreateAsync(AssignmentDTO assignmentDTO)
        {
            try
            {
                var course = await _courseService.GetCourseByCodeAsync(assignmentDTO.CourseCode!);

                var assignment = (course, assignmentDTO).Adapt<Assignment>();

                await _unitOfWork.AssignmentRepository.CreateAsync(assignment!);

                if (assignmentDTO.AssignmentAttachments is not null)
                {
                    foreach (var attachment in assignmentDTO.AssignmentAttachments)
                    {
                        attachment.AssignmentId = assignment.Id;
                        await CreateAssignmentAttachmentAsync(attachment, AssignmentAttachmentType.CreatedByTeacher);
                    }
                }

                // Create Notification

                _unitOfWork.Commit();

                CreateAssignmentResponseDTO? createAssignmentResponseDTO = assignment.Adapt<CreateAssignmentResponseDTO>();

                return createAssignmentResponseDTO;
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();

                throw;
            }
        }

        public async Task CreateAssignmentAttachmentAsync(AssignmentAttachmentDTO assignmentAttachmentDTO, AssignmentAttachmentType attachmentType)
        {
            var assignmentAttachment = assignmentAttachmentDTO.Adapt<AssignmentAttachment>();

            assignmentAttachment.Type = attachmentType; // TEMP

            await _unitOfWork.AssignmentRepository.CreateAssignmentAttachmentAsync(assignmentAttachment!);
        }
    }
}
