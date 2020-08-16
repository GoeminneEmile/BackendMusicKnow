using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project_Backend.Models.Models
{
    public class ApiDelete
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        [StringLength(450, ErrorMessage = "Error message is limited to 450 characters in length.")]
        public string DeletedItem { get; set; }
        [Required]
        [StringLength(450, ErrorMessage = "Error message is limited to 450 characters in length.")]
        public string DeletedName { get; set; }
        [Required]
        [StringLength(450, ErrorMessage = "Error message is limited to 450 characters in length.")]
        public string DeletedDescription { get; set; }
        [Required]
        [StringLength(450, ErrorMessage = "Error message is limited to 450 characters in length.")]
        public string DeletedQuestionCount { get; set; }
        [Required]
        [StringLength(450, ErrorMessage = "Error message is limited to 450 characters in length.")]
        public string DeletedDifficulty { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateCreated { get; set; } = DateTime.Now;
    }
}
