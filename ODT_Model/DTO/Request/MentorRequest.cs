using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODT_Model.DTO.Request
{
    public class MentorRequest
    {
        [Required(ErrorMessage = "AcademicLevel is required")]
        public string? AcademicLevel { get; set; }

        [Required(ErrorMessage = "WorkPlace is required")]
        public string? WorkPlace { get; set; }

        [Required(ErrorMessage = "Skill is required")]
        public string? Skill { get; set; }

        public IFormFile? File { get; set; }
    }
}
