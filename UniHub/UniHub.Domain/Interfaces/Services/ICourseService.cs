using UniHub.Domain.DTOs;
using UniHub.Domain.DTOs.Responses.Course;

namespace UniHub.Domain.Interfaces.Services
{
    public interface ICourseService
    {
        Task<CreateCourseResponseDTO> Create(CourseDTO courseDTO);
    }
}
