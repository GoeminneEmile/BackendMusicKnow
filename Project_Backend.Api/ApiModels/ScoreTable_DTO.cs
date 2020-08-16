using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project_Backend.Api.ApiModels
{
    public class ScoreTable_DTO
    {
        [Required(ErrorMessage = "QuizName is required")]
        [Display(Name = "QuizName")]
        public string QuizName { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "User")]
        public string ApplicationUser { get; set; }

        [Display(Name = "achieved score")]
        [StringLength(250, MinimumLength = 2, ErrorMessage = "Only 250 characters allowed.")]
        public int FinalScore { get; set; }

        [Display(Name = "Max score")]
        [StringLength(250, MinimumLength = 2, ErrorMessage = "Only 250 characters allowed.")]
        public int MaxScore { get; set; }

        [Display(Name = "Date Finished")]
        public DateTime Datefinished { get; set; }
    }
}
