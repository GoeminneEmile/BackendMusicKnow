using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project_Backend.Models.Models
{
    public class Quizzes
    {
        [Key]
        public Guid QuizId { get; set; } = Guid.NewGuid();
        [Required]
        [StringLength(50, ErrorMessage = "Quiz name is limited to 250 characters in length.")]
        public string Name { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "Quiz name is limited to 250 characters in length.")]
        public string Description { get; set; }

        public Difficulties Difficulty { get; set; }

        [Required]
        [Range(0, 10)]
        public int QuestionCount { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateCreated { get; set; } = DateTime.Now;

        public string AppUserId { get; set; }

        public virtual AppUser AppUser { get; set; }
        public virtual IList<Questions> Questions { get; set; }
        public virtual IList<Score> ScoreTables { get; set; }
        public virtual IList<Comment> CommentTables { get; set; }

        public enum Difficulties
        {
            starter = 0,
            medium = 1,
            Beethoven = 2
        }

    }
}
