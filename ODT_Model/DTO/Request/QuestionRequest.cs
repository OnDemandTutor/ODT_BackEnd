using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ODT_Model.DTO.Request
{
    public class QuestionRequest
    {
        [Required(ErrorMessage = "Category is required!")]
        public string CategoryName { get; set; }
        
        [Required(ErrorMessage = "Content is required!")]
        public string Content { get; set; }
        
        public IFormFile? Image { get; set; }

    }
}
