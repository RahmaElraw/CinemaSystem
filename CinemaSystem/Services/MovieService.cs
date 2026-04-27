namespace CinemaSystem.Services
{
    public class MovieService
    {
        public string? SaveImg(IFormFile image, string folder = "movies")
        {
            try
            {
                var fileName = $"{DateTime.Now.ToString("dd_MM_yyyy")}_{Guid.NewGuid()}{Path.GetExtension(image.FileName)}";

                var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", folder);
                Directory.CreateDirectory(folderPath);

                var filePath = Path.Combine(folderPath, fileName);

                using (var stream = System.IO.File.Create(filePath))
                {
                    image.CopyTo(stream);
                }

                return fileName;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }

        public bool RemoveImg(string imageName, string folder = "movies")
        {
            try
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", folder, imageName);

                if (System.IO.File.Exists(filePath))
                    System.IO.File.Delete(filePath);

                Console.WriteLine($"Remove old img from wwwroot");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }
    }
}