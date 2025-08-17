using Microsoft.AspNetCore.Mvc;

namespace UniHub.API.Model.Assignment
{
    public class CreateAssignmentModel
    {
        [FromBody]
        public CreateAssignmentBody? Body { get; set; }
    }
}
