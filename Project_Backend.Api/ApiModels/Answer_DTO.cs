using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project_Backend.Api.ApiModels
{
    public class Answer_DTO
    {
        [Required]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Answer name is limited to 50 characters in length.")]
        public string AnswerName { get; set; }

        [Required]
        public string Correct { get; set; }
    }
}
