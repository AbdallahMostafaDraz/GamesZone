namespace GameZone.Web.Settings
{
    public static class ConstantsFile
    {
        public const string ImagesPath = "/Assets/Images/Games";
        public const string AllowedExtensions = ".jpg,.jpeg,.png";
        public const  int MaxFileSizeInMB = 1;
        public const long MaxFileSizeInBytes = MaxFileSizeInMB * 1024 * 1024;
    }
}
