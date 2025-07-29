using Mapster;
using UniHub.API.Model.Assignment;
using UniHub.Domain.DTOs;
using UniHub.Domain.DTOs.Responses.Assignment;
using UniHub.Domain.DTOs.Responses.Course;
using UniHub.Domain.Entities;

namespace UniHub.API.Mapper;

public static class AssignmentMappingConfigurations
{
    public static void RegisterAssignmentMaps(this IServiceCollection services)
    {
        #region Assignment - Create
        TypeAdapterConfig<AssignmentAttachmentDTO, AttachmentDTO>
            .NewConfig()
                .Map(dest => dest.Url, src => src.Url!);

        TypeAdapterConfig<CreateAssignmentModel, AssignmentDTO>
            .NewConfig()
                .Map(dest => dest.CourseCode, src => src.Body!.CourseCode)
                .Map(dest => dest.Title, src => src.Body!.Title)
                .Map(dest => dest.Description, src => src.Body!.Description)
                .Map(dest => dest.ExpirationDate, src => src.Body!.ExpirationDate)
                .Map(dest => dest.AssignmentAttachments, src => src.Body!.AssignmentAttachments ?? new());

        TypeAdapterConfig<(GetCourseByCodeResponseDTO Course, AssignmentDTO Assignment), Assignment>
            .NewConfig()
                .Map(dest => dest.CourseId, src => src.Course.CourseId)
                .Map(dest => dest.Course, src => src.Course)
                .Map(dest => dest.Title, src => src.Assignment.Title)
                .Map(dest => dest.Description, src => src.Assignment.Description)
                .Map(dest => dest.ExpirationDate, src => src.Assignment.ExpirationDate)
                .Map(dest => dest.AssignmentAttachments, src => src.Assignment.AssignmentAttachments);

        TypeAdapterConfig<AttachmentDTO, AssignmentAttachmentDTO>
            .NewConfig()
                .Map(dest => dest.Url, src => src.Url!);

        TypeAdapterConfig<Assignment, CreateAssignmentResponseDTO>
            .NewConfig()
                .Map(dest => dest.CourseName, src => src.Course.Name)
                .Map(dest => dest.Title, src => src.Title)
                .Map(dest => dest.Description, src => src.Description)
                .Map(dest => dest.ExpirationDate, src => src.ExpirationDate)
                .Map(dest => dest.AssignmentAttachments, src => src.AssignmentAttachments);

        TypeAdapterConfig<AssignmentAttachmentDTO, AssignmentAttachment>
            .NewConfig();
        #endregion
    }
}
