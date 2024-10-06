
namespace GameZone.Web.ViewModels
{
    public class CreateGameFormViewModel : BaseGameViewModel
    {
        [AllowedExtensions(ConstantsFile.AllowedExtensions)]
        [MaxFileSize(ConstantsFile.MaxFileSizeInBytes)]
        public IFormFile Cover { get; set; }
    }
}
