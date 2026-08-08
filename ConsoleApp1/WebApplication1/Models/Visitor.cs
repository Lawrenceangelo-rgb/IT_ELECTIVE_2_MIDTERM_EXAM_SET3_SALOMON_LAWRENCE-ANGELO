using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Visitor
    {
        public int Id { get; set; }

        [Display(Name = "Pass Number")]
        public string PassNumber { get; set; }

        [Required(ErrorMessage = "First Name is required.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        public string Company { get; set; }

        [Required, Phone]
        [Display(Name = "Contact Number")]
        public string ContactNumber { get; set; }

        [Required]
        [Display(Name = "Person to Visit")]
        public string PersonToVisit { get; set; }

        [Required]
        public string Department { get; set; }

        [Required]
        public string Purpose { get; set; }

        [Display(Name = "Entry Time")]
        public DateTime EntryDateTime { get; set; }

        [Display(Name = "Exit Time")]
        public DateTime? ExitDateTime { get; set; }

        public string Status { get; set; }

        [Required]
        [Display(Name = "Valid ID Presented")]
        public string ValidIdPresented { get; set; }

        public string Notes { get; set; }
    }
}