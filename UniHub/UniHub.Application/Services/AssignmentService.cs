using Mapster;
using System.Net;
using UniHub.Application.Exceptions;
using UniHub.Application.Resources;
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
        private readonly IUserService _userService;

        public AssignmentService(IUnitOfWork unitOfWork, ICourseService courseService, IUserService userService)
        {
            _unitOfWork = unitOfWork;
            _courseService = courseService;
            _userService = userService;
        }

        public async Task<CreateAssignmentResponseDTO?> CreateAsync(AssignmentDTO assignmentDTO)
        {
            try
            {
                var user = await _userService.GetUserByIdentifierAsync(assignmentDTO.UserIdentification!);

                if ((UserRole?)user.Role != UserRole.ADMIN)
                    throw new HttpRequestFailException(nameof(ApplicationMsg.USR0003), ApplicationMsg.USR0003, HttpStatusCode.BadRequest);

                var course = await _courseService.GetCourseByCodeAsync(assignmentDTO.CourseCode!);

                var assignment = (course, user, assignmentDTO).Adapt<Assignment>();

                assignment = await _unitOfWork.AssignmentRepository.CreateAsync(assignment!);

                var assignmentAttachmentsDTO = assignment.Adapt<List<AssignmentAttachmentDTO>>();

                await CreateAssignmentAttachmentsAsync(assignmentAttachmentsDTO);

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

        public async Task CreateAssignmentAttachmentsAsync(List<AssignmentAttachmentDTO> assignmentAttachmentsDTO)
        {
            var assignmentAttachments = assignmentAttachmentsDTO.Adapt<List<AssignmentAttachment>>();

            foreach (var attachment in assignmentAttachments)
                await _unitOfWork.AssignmentRepository.CreateAssignmentAttachmentAsync(attachment);
        }
    }
}
