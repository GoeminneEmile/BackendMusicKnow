using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Project_Backend.Models.Models
{
    public class Comment
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [StringLength(450, MinimumLength = 5, ErrorMessage = "Comment is limited to 450 characters in length and a minnimum of 5 characters")]
        public string Comments { get; set; }
        public string AppUserId { get; set; }
        public Guid QuizId { get; set; }

        public virtual AppUser AppUser { get; set; }
        public virtual Quizzes Quiz { get; set; }
    }
}
