using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project_Backend.Api.ApiModels
{
    public class Question_DTO
    {
        [Required]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Question name is limited to 250 characters in length.")]
        public string QuestionName { get; set; }

        public List<Answer_DTO> Answers { get; set; } = new List<Answer_DTO>();
    }
}
