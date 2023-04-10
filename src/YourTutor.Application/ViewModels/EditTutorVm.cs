using System.ComponentModel.DataAnnotations;

namespace YourTutor.Application.ViewModels
{
    public class EditTutorVm
    {
        [Required(ErrorMessage = "Description is required")]
        [StringLength(350, MinimumLength = 2, ErrorMessage = "Must be between 2 and 350 characters")]
        public string Description { get; set; }


        [Required(ErrorMessage = "Country is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Must be between 2 and 100 characters")]
        public string Country { get; set; }


        [Required(ErrorMessage = "Language is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Must be between 2 and 100 characters")]
        public string Language { get; set; }
    }
}


