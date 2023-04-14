using System.ComponentModel.DataAnnotations;

namespace YourTutor.Application.ViewModels
{
    public class CreateOfferVm
    {
        [Required(ErrorMessage = "Description is required")]
        [StringLength(350, MinimumLength = 2, ErrorMessage = "Must be between 2 and 350 characters")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Subject is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Must be between 2 and 100 characters")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Price have to be greather than 0")]
        public int Price { get; set; }

        public bool IsRemotely { get; set; }
        public string Location { get; set; }
    }
}


