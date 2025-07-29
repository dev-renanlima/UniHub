using UniHub.Domain.DTOs;

namespace UniHub.API.Model.Assignment
{
    public class CreateAssignmentBody
    {
        public string? CourseCode { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public DateTime? ExpirationDate { get; set; }

        public List<AttachmentDTO>? AssignmentAttachments { get; set; }
    }
}
