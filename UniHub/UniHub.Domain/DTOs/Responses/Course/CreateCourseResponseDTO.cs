using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniHub.Domain.DTOs.Responses.Course
{
    public class CreateCourseResponseDTO
    {
        public string? AdminId { get; set; }

        public string? Name { get; set; }

        public string? Code { get; set; }
    }
}
