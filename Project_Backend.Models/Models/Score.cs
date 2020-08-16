using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Project_Backend.Models.Models
{
    public class Score
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public int FinalScore { get; set; }
        public int MaxScore { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateFinished { get; set; } = DateTime.Now;
        public string AppUserId { get; set; }
        public Guid QuizId { get; set; }

        public virtual AppUser AppUser { get; set; }
        public virtual Quizzes Quiz { get; set; }
    }
}
