using System.ComponentModel.DataAnnotations;

namespace GR.Web.Models.ContactViewModel
{
    public class ContactViewModel
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "E-mail")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Phone")]
        public string Phone { get; set; }
         
        [Display(Name = "Domain")]
        public string Domain { get; set; }

        [Display(Name = "Do you have hosting?")]
        public string DoYouHaveHosting { get; set; }

        [Required]
        [Display(Name = "Project Description")]
        [StringLength(255, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 75)]
        public string ProjectDescription { get; set; }
    }
}