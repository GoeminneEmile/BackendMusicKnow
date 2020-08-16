using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project_Backend.Models.Models
{
    public class BonusPoints
    {
        [Key]
        public Guid BonusId { get; set; } = Guid.NewGuid();
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public int Points { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateCreated { get; set; } = DateTime.Now;

    }
}
