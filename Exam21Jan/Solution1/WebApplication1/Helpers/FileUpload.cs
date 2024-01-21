namespace WebApplication1.Helpers
{
    public static class FileUpload
    {
        public static async Task<bool> IsValidSize(this IFormFile file, int size = 5000000)
            => file.Length <= size;
        public static async Task<bool> IsValidType(this IFormFile file, string contenttype="image")
            =>file.ContentType.Contains(contenttype);

        public static async Task<string> ImageSaveAsync(this IFormFile file,string path)
        {
            string extension=Path.GetExtension(file.FileName);
            string filename =Path.GetFileNameWithoutExtension(file.FileName);
            if (filename.Length>32)
            {
                filename = filename.Substring(filename.Length - 32);
            }
            filename=Path.Combine(path, filename+Path.GetRandomFileName()+extension);
            using(FileStream fs=File.Create(Path.Combine(PathConstants.RoothPath, filename))) 
            {
                await file.CopyToAsync(fs);
            }
            return filename;
        }
    }
}
