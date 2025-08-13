using Mapster;
using UniHub.API.Model.Course.CreateCourse;
using UniHub.Domain.DTOs;
using UniHub.Domain.DTOs.Responses.Course;
using UniHub.Domain.DTOs.Responses.User;
using UniHub.Domain.Entities;
using UniHub.Domain.Extensions;
using UniHub.Domain.VOs;

namespace UniHub.API.Mapper;

public static class CourseMappingConfigurations
{
    public static void RegisterCourseMaps(this IServiceCollection services)
    {
        #region Course - Create
        TypeAdapterConfig<CreateCourseModel, CourseDTO>
            .NewConfig()
                .Map(dest => dest.UserIdentifier, src => src.Body!.UserIdentifier)
                .Map(dest => dest.Name, src => src.Body!.Name)
                .Map(dest => dest.Code, src => NumberExtensions.GenerateRandomCodeByDate());

        TypeAdapterConfig<(CourseDTO Course, GetUserResponseDTO User), Course>
            .NewConfig()
                .Map(dest => dest.UserId, src => src.User.Id)
                .Map(dest => dest.User, src => src.User)
                .Map(dest => dest.Code, src => src.Course.Code)
                .Map(dest => dest.Name, src => src.Course.Name);

        TypeAdapterConfig<Course, CreateCourseResponseDTO>
            .NewConfig()
                .Map(dest => dest.UserIdentifier, src => src.User.ExternalIdentifier);
        #endregion

        #region Course - GetCoursesByUser
        TypeAdapterConfig<CourseVO, CourseByUserDTO>
            .NewConfig();

        TypeAdapterConfig<(GetUserResponseDTO User, List<CourseVO> Courses), GetCoursesByUserResponseDTO>
            .NewConfig()
                .Map(dest => dest.UserIdentifier, src => src.User.ExternalIdentifier!)
                .Map(dest => dest.UserName, src => src.User.Name)
                .Map(dest => dest.Courses, src =>
                    src.Courses!.Select(course => new CourseVO
                    {
                        CourseId = course.CourseId,
                        CourseIdentifier = course.CourseIdentifier,
                        CourseName = course.CourseName,
                        CourseCode = course.CourseCode,
                        UserName = course.UserName,
                        NumberOfMembers = course.NumberOfMembers,
                        CreationDate = course.CreationDate,
                        UpdateDate = course.UpdateDate
                    }).ToList())
                .Map(dest => dest.NumberOfCourses, src => src.Courses.Count);
        #endregion

        #region Course - GetCoursesByCode
        TypeAdapterConfig<Course, GetCourseResponseDTO>
            .NewConfig()
                .Map(dest => dest.CourseId, src => src.Id!);
        #endregion        
    }
}
