using Mapster;

namespace UniHub.API.Model.Course.AddMemberByCode;

public class AddMemberByCodeBody
{
    public string MemberId { get; set; } = null!;

    public string? Name { get; set; }
}
