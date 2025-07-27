using Mapster;
using UniHub.API.Extensions;
using UniHub.API.Model.Course.CreateCourse;
using UniHub.Domain.DTOs;
using UniHub.Domain.DTOs.Responses.Course;
using UniHub.Domain.Entities;

namespace UniHub.API.Mapper
{
    public static class CourseMappingConfigurations
    {
        public static void RegisterCourseMaps(this IServiceCollection services)
        {
            TypeAdapterConfig<CreateCourseModel, CourseDTO>
                    .NewConfig()
                    .Map(dest => dest.AdminId, src => src.Body!.AdminId)
                    .Map(dest => dest.Name, src => src.Body!.Name)
                    .Map(dest => dest.Code, src => NumberExtensions.GenerateRandomCodeByDate());

            TypeAdapterConfig<CourseDTO, Course>
                    .NewConfig()
                    .Map(dest => dest.AdminId, src => src.AdminId)
                    .Map(dest => dest.Name, src => src.Name)
                    .Map(dest => dest.Code, src => src.Code);

            TypeAdapterConfig<Course, CreateCourseResponseDTO>
                    .NewConfig()
                    .Map(dest => dest.AdminId, src => src.AdminId)
                    .Map(dest => dest.Name, src => src.Name)
                    .Map(dest => dest.Code, src => src.Code)
                    .Map(dest => dest.Id, src => src.Id);
        }
    }
}
