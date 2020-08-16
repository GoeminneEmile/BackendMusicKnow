using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project_Backend.Models.Models
{
    public class Questions
    {
        [Key]
        public Guid QuestionId { get; set; } = Guid.NewGuid();
        [Required]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Question name is limited to 250 characters in length.")]
        public string QuestionName { get; set; }
        public Guid AnswerId { get; set; }
        public Guid QuizId { get; set; }
        public virtual Quizzes Quiz { get; set; }

        public virtual IList<Answer> Answers { get; set; }
    }
}
