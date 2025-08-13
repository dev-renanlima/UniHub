using Mapster;
using UniHub.API.Model.Course.AddMemberByCode;
using UniHub.Domain.DTOs;
using UniHub.Domain.DTOs.Responses.Course;
using UniHub.Domain.DTOs.Responses.User;
using UniHub.Domain.Entities;
using UniHub.Domain.VOs;

namespace UniHub.API.Mapper;

public static class CourseMemberMappingConfigurations
{
    public static void RegisterCourseMemberMaps(this IServiceCollection services)
    {
        #region Course - Member
        TypeAdapterConfig<AddMemberByCodeModel, CourseMemberDTO>
            .NewConfig()
                .Map(dest => dest.UserIdentifier, src => src.Body!.UserIdentifier)
                .Map(dest => dest.CourseCode, src => src.Body!.CourseCode);

        TypeAdapterConfig<(GetCourseResponseDTO Course, GetUserResponseDTO User), CourseMember>
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

        #region Course - GetMembersByCourseCode
        TypeAdapterConfig<(Course Course, GetUserResponseDTO User, List<CourseMemberVO> CourseMembers), GetCourseResponseDTO>
            .NewConfig()
                .Map(dest => dest.CourseId, src => src.Course.Id!)
                .Map(dest => dest.CourseName, src => src.Course.Name!)
                .Map(dest => dest.CourseCode, src => src.Course.Code!)
                .Map(dest => dest.UserIdentifier, src => src.User.ExternalIdentifier!)
                .Map(dest => dest.UserName, src => src.User.Name)
                .Map(dest => dest.CreationDate, src => src.Course.CreationDate!)
                .Map(dest => dest.UpdateDate, src => src.Course.UpdateDate!)
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
