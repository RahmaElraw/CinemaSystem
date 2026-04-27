namespace CinemaSystem.Services
{
    public class ActorService
    {
        public string? SaveImg(IFormFile image)
        {
            try
            {
                var fileName = $"{DateTime.Now.ToString("dd_MM_yyyy")}_{Guid.NewGuid()}{Path.GetExtension(image.FileName)}";

                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\actors", fileName);

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

        public bool RemoveImg(string imageName)
        {
            try
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\actors", imageName);

                if (System.IO.File.Exists(filePath))
                    System.IO.File.Delete(filePath);

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