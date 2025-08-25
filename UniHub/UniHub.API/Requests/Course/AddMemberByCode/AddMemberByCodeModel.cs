using Microsoft.AspNetCore.Mvc;

namespace UniHub.API.Model.Course.AddMemberByCode;

public class AddMemberByCodeModel
{
    [FromBody]
    public AddMemberByCodeBody? Body { get; set; }
}
