using Mapster;

namespace UniHub.API.Model.Course.AddMemberByCode;

public class AddMemberByCodeBody
{
    public string UserIdentifier { get; set; } = null!;

    public string? CourseCode { get; set; }
}
