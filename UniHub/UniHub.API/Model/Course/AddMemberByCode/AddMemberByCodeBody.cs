using Mapster;

namespace UniHub.API.Model.Course.AddMemberByCode;

public class AddMemberByCodeBody
{
    public string ExternalIdentifier { get; set; } = null!;

    public string? Code { get; set; }
}
