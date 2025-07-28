using Mapster;
using UniHub.API.Extensions;
using UniHub.API.Model.Course.AddMemberByCode;
using UniHub.API.Model.Course.CreateCourse;
using UniHub.Domain.DTOs;
using UniHub.Domain.DTOs.Responses.Course;
using UniHub.Domain.DTOs.Responses.User;
using UniHub.Domain.Entities;

namespace UniHub.API.Mapper
{
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

            #region Course - Member
            TypeAdapterConfig<AddMemberByCodeModel, CourseMemberDTO>
                .NewConfig()
                    .Map(dest => dest.ExternalIdentifier, src => src.Body!.ExternalIdentifier)
                    .Map(dest => dest.Code, src => src.Body!.Code);

            TypeAdapterConfig<(Course Course, GetUserResponseDTO User), CourseMember>
                .NewConfig()
                    .Map(dest => dest.CourseId, src => src.Course.Id)
                    .Map(dest => dest.Course, src => src.Course)
                    .Map(dest => dest.UserId, src => src.User.Id)
                    .Map(dest => dest.User, src => src.User)
                    .Map(dest => dest.EnrollmentDate, src => DateTime.UtcNow);

            TypeAdapterConfig<CourseMember, AddCourseMemberResponseDTO>
                .NewConfig()
                    .Map(dest => dest.UserIdentifier, src => src.User.ExternalIdentifier)
                    .Map(dest => dest.CourseId, src => src.Course.Id)
                    .Map(dest => dest.CourseName, src => src.Course.Name)
                    .Map(dest => dest.EnrollmentDate, src => src.EnrollmentDate);
            #endregion

            #region Course - GetCoursesByUser
            TypeAdapterConfig<Course, CourseByUserDTO>
                .NewConfig()
                    .Map(dest => dest.CourseId, src => src.Id!);

            TypeAdapterConfig<(GetUserResponseDTO User, List<Course?> Courses), GetCoursesByUserResponseDTO>
                .NewConfig()
                    .Map(dest => dest.UserIdentifier, src => src.User.ExternalIdentifier)
                    .Map(dest => dest.Courses, src => src.Courses);
            #endregion
        }
    }
}
