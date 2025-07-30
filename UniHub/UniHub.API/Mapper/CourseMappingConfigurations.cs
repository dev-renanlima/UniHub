using Mapster;
using UniHub.API.Extensions;
using UniHub.API.Model.Course.AddMemberByCode;
using UniHub.API.Model.Course.CreateCourse;
using UniHub.Domain.DTOs;
using UniHub.Domain.DTOs.Responses.Course;
using UniHub.Domain.DTOs.Responses.User;
using UniHub.Domain.Entities;
using UniHub.Domain.Enums;
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

        #region Course - Member
        TypeAdapterConfig<AddMemberByCodeModel, CourseMemberDTO>
            .NewConfig()
                .Map(dest => dest.ExternalIdentifier, src => src.Body!.ExternalIdentifier)
                .Map(dest => dest.Code, src => src.Body!.Code);

        TypeAdapterConfig<(GetCourseByCodeResponseDTO Course, GetUserResponseDTO User), CourseMember>
            .NewConfig()
                .Map(dest => dest.CourseId, src => src.Course.CourseId)
                .Map(dest => dest.Course, src => src.Course)
                .Map(dest => dest.UserId, src => src.User.Id)
                .Map(dest => dest.User, src => src.User)
                .Map(dest => dest.EnrollmentDate, src => DateTime.UtcNow);

        TypeAdapterConfig<CourseMember, AddCourseMemberResponseDTO>
            .NewConfig()
                .Map(dest => dest.UserIdentifier, src => src.User.ExternalIdentifier)
                .Map(dest => dest.CourseId, src => src.CourseId)
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
                .Map(dest => dest.UserName, src => src.User.Name)
                .Map(dest => dest.Courses, src => src.Courses)
                .Map(dest => dest.NumberOfCourses, src => src.Courses.Count);
        #endregion

        #region Course - GetCoursesByCode
        TypeAdapterConfig<Course, GetCourseByCodeResponseDTO>
            .NewConfig()
                .Map(dest => dest.CourseId, src => src.Id!);
        #endregion

        #region Course - GetMembersByCourseCode
        TypeAdapterConfig<(Course Course, List<CourseMemberVO> CourseMembers), GetMembersByCourseCodeResponseDTO>
            .NewConfig()
                .Map(dest => dest.CourseId, src => src.Course.Id!)
                .Map(dest => dest.CourseName, src => src.Course.Name!)
                .Map(dest => dest.Code, src => src.Course.Code!)
                .Map(dest => dest.CourseMembers, src =>
                    src.CourseMembers!.Select(courseMember => new CourseMemberVO
                    {
                        UserIdentifier = courseMember.UserIdentifier,
                        UserName = courseMember.UserName,
                        EnrollmentDate = courseMember.EnrollmentDate
                    }).ToList())
                .Map(dest => dest.NumberOfMembers, src => src.CourseMembers.Count); 
        #endregion
    }
}
