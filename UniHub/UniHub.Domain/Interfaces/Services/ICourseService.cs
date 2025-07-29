using UniHub.Domain.DTOs;
using UniHub.Domain.DTOs.Responses.Course;

namespace UniHub.Domain.Interfaces.Services
{
    public interface ICourseService
    {
        Task<CreateCourseResponseDTO> CreateAsync(CourseDTO courseDTO);
        Task<GetCourseByCodeResponseDTO?> GetCourseByCodeAsync(string code);
        Task<AddCourseMemberResponseDTO> AddMemberByCodeAsync(CourseMemberDTO courseMemberDTO);
        Task<GetCoursesByUserResponseDTO?> GetCoursesByUserAsync(string externalIdentifier);
    }
}
