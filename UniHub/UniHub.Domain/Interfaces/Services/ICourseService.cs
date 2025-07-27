using UniHub.Domain.DTOs;
using UniHub.Domain.DTOs.Responses.Course;

namespace UniHub.Domain.Interfaces.Services
{
    public interface ICourseService
    {
        Task<CreateCourseResponseDTO> CreateAsync(CourseDTO courseDTO);
        Task<AddCourseMemberResponseDTO> AddMemberByCode(CourseMemberDTO courseMemberDTO);
    }
}
