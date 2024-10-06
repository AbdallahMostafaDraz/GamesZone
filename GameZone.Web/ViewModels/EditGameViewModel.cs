
namespace GameZone.Web.ViewModels
{
    public class EditGameViewModel : BaseGameViewModel
    {
        public int Id { get; set; }
        [AllowedExtensions(ConstantsFile.AllowedExtensions)]
        [MaxFileSize(ConstantsFile.MaxFileSizeInBytes)]
        public IFormFile? Cover { get; set; }

        public string? OldCoverName { get; set; }
    }
}
