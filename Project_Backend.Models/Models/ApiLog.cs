using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project_Backend.Models.Models
{
    public class ApiLog
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        [StringLength(450, ErrorMessage = "Error message is limited to 450 characters in length.")]
        public string Error { get; set; }
        [Required]
        [StringLength(450, ErrorMessage = "Name is limited to 450 characters in length.")]
        public string Name { get; set; }
        [Required]
        [StringLength(450, ErrorMessage = "Questions is limited to 450 characters in length.")]
        public string Questions { get; set; }
        [Required]
        [StringLength(450, ErrorMessage = "Description is limited to 450 characters in length.")]
        public string Description { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateCreated { get; set; } = DateTime.Now;
    }
}
