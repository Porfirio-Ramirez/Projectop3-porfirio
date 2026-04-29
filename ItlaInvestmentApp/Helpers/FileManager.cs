namespace ItlaInvestmentApp.Helpers
{
    public static class FileManager
    {
        public static string? Upload(IFormFile? file, int id, string folderName, bool isEditMode = false, string? imagePath = "")
        {
            if (isEditMode && file == null)
            {
                return imagePath;
            }

            if (file == null)
            {
                return string.Empty;

            }

            string basepath = $"Images/{folderName}/{id}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/{basepath}");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            Guid guid = Guid.NewGuid();
            FileInfo fileInfo = new(file.FileName);
            string fileName = guid + fileInfo.Extension;

            string FullFileName = Path.Combine(path, fileName);

            using (var stream = new FileStream(FullFileName, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            if (isEditMode && !string.IsNullOrWhiteSpace(imagePath))
            {
                string[] oldImagePart = imagePath.Split("/");
                string oldFileName = oldImagePart[^1];
                string completeOldPath = Path.Combine(path, oldFileName);

                if (File.Exists(completeOldPath))
                {
                    File.Delete(completeOldPath);
                }

               
            }
            return $"{basepath}/{fileName}";

        }

        public static bool Delete(int id, string folderName)
        {
            string basepath = $"Images/{folderName}/{id}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/{basepath}");

            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }
            else
            {
                return false;
            }

            return true;
        }
    }
}
