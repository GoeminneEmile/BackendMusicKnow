using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Project_Backend.Models.Models
{
    public class Answer
    {
        [Key]
        public Guid AnswerId { get; set; } = Guid.NewGuid();
        [Required]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Answer name is limited to 50 characters in length.")]
        public string AnswerN { get; set; }
        public virtual Questions Question { get; set; }
        [Required]
        public Guid QuestionId { get; set; }

        public Guid ScoreId { get; set; }
        [Required]
        public State Correct { get; set; }
        public enum State
        {
            incorrect = 0,
            correct = 1
        }


    }
}
