
namespace GameZone.Web.ViewModels
{
    public class BaseGameViewModel
    {
        [MaxLength(250)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(1500)]
        public string Description { get; set; } = string.Empty;
        [Required(ErrorMessage = "Category field is required")]
        [Display(Name = "Category")]
        public int CategoryId { get; set; } = default!;
        public IEnumerable<SelectListItem> Categories { get; set; } = Enumerable.Empty<SelectListItem>();
        [Display(Name = "Supported Devices")]
        public List<int> SelectedDevices { get; set; } = default!;
        public IEnumerable<SelectListItem> Devices { get; set; } = Enumerable.Empty<SelectListItem>();
    }
}
