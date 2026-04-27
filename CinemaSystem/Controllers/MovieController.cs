using CinemaSystem.DataAccess;
using CinemaSystem.Models;
using CinemaSystem.Services;
using CinemaSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CinemaSystem.Controllers
{
    public class MovieController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly MovieService _movieService;

        public MovieController(ApplicationDbContext context, MovieService movieService)
        {
            _context = context;
            _movieService = movieService;
        }

        public IActionResult Index()
        {
            var categories = _context.Categories
                .Include(c => c.Movies)
                .ToList();
            return View(categories);
        }

        public IActionResult Details(int id)
        {
            var movie = _context.Movies
                .Include(m => m.Category)
                .Include(m => m.Cinema)
                .Include(m => m.MovieSubImages)
                .Include(m => m.MovieActors)
                    .ThenInclude(ma => ma.Actor)
                .FirstOrDefault(m => m.Id == id);

            if (movie is null) return NotFound();
            return View(movie);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Categories = _context.Categories.ToList();
            ViewBag.Cinemas = _context.Cinemas.ToList();
            ViewBag.Actors = _context.Actors.ToList();
            return View(new CreateMovieVM());
        }

        [HttpPost]
        public IActionResult Create(CreateMovieVM vm)
        {
            ModelState.Remove("MainImg");
            ModelState.Remove("SubImages");
            ModelState.Remove("ActorIds");

            if (!ModelState.IsValid)
            {
                ViewBag.Categories = _context.Categories.ToList();
                ViewBag.Cinemas = _context.Cinemas.ToList();
                ViewBag.Actors = _context.Actors.ToList();
                return View(vm);
            }

            var movie = new Movie
            {
                Name = vm.Name,
                Description = vm.Description,
                Price = vm.Price,
                Status = vm.Status,
                DateTime = vm.DateTime,
                CategoryId = vm.CategoryId,
                CinemaId = vm.CinemaId
            };

            if (vm.MainImg is not null && vm.MainImg.Length > 0)
            {
                var fileName = _movieService.SaveImg(vm.MainImg, "movies");
                if (fileName is not null) movie.MainImg = fileName;
            }

            if (vm.SubImages is not null && vm.SubImages.Any())
            {
                movie.MovieSubImages = new List<MovieSubImage>();
                foreach (var img in vm.SubImages)
                {
                    var fileName = _movieService.SaveImg(img, "movies\\sub");
                    if (fileName is not null)
                        movie.MovieSubImages.Add(new MovieSubImage { ImageUrl = fileName });
                }
            }

            if (vm.ActorIds is not null && vm.ActorIds.Any())
            {
                movie.MovieActors = vm.ActorIds.Select(actorId => new MovieActor
                {
                    ActorId = actorId
                }).ToList();
            }

            _context.Movies.Add(movie);
            _context.SaveChanges();

            TempData["success_notification"] = "Movie Added Successfully";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var movie = _context.Movies
                .Include(m => m.MovieActors)
                .Include(m => m.MovieSubImages)
                .FirstOrDefault(m => m.Id == id);

            if (movie is null) return NotFound();

            var vm = new EditMovieVM
            {
                Id = movie.Id,
                Name = movie.Name,
                Description = movie.Description,
                Price = movie.Price,
                Status = movie.Status,
                DateTime = movie.DateTime,
                CategoryId = movie.CategoryId,
                CinemaId = movie.CinemaId,
                ExistingMainImg = movie.MainImg,
                ExistingActorIds = movie.MovieActors?.Select(ma => ma.ActorId).ToList()
            };

            ViewBag.Categories = _context.Categories.ToList();
            ViewBag.Cinemas = _context.Cinemas.ToList();
            ViewBag.Actors = _context.Actors.ToList();
            return View(vm);
        }

        [HttpPost]
        public IActionResult Edit(EditMovieVM vm)
        {
            ModelState.Remove("MainImg");
            ModelState.Remove("SubImages");
            ModelState.Remove("ActorIds");
            ModelState.Remove("ExistingMainImg");
            ModelState.Remove("ExistingActorIds");

            if (!ModelState.IsValid)
            {
                ViewBag.Categories = _context.Categories.ToList();
                ViewBag.Cinemas = _context.Cinemas.ToList();
                ViewBag.Actors = _context.Actors.ToList();
                return View(vm);
            }

            var movie = _context.Movies
                .Include(m => m.MovieActors)
                .Include(m => m.MovieSubImages)
                .FirstOrDefault(m => m.Id == vm.Id);

            if (movie is null) return NotFound();

            movie.Name = vm.Name;
            movie.Description = vm.Description;
            movie.Price = vm.Price;
            movie.Status = vm.Status;
            movie.DateTime = vm.DateTime;
            movie.CategoryId = vm.CategoryId;
            movie.CinemaId = vm.CinemaId;

            if (vm.MainImg is not null && vm.MainImg.Length > 0)
            {
                if (movie.MainImg is not null)
                    _movieService.RemoveImg(movie.MainImg, "movies");

                var fileName = _movieService.SaveImg(vm.MainImg, "movies");
                if (fileName is not null) movie.MainImg = fileName;
            }

            if (vm.SubImages is not null && vm.SubImages.Any())
            {
                movie.MovieSubImages ??= new List<MovieSubImage>();
                foreach (var img in vm.SubImages)
                {
                    var fileName = _movieService.SaveImg(img, "movies\\sub");
                    if (fileName is not null)
                        movie.MovieSubImages.Add(new MovieSubImage { ImageUrl = fileName });
                }
            }

            var existingActors = _context.MovieActors.Where(ma => ma.MovieId == movie.Id).ToList();
            _context.MovieActors.RemoveRange(existingActors);

            if (vm.ActorIds is not null && vm.ActorIds.Any())
            {
                foreach (var actorId in vm.ActorIds)
                    _context.MovieActors.Add(new MovieActor { MovieId = movie.Id, ActorId = actorId });
            }

            _context.Movies.Update(movie);
            _context.SaveChanges();

            TempData["success_notification"] = "Movie Updated Successfully";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var movie = _context.Movies
                .Include(m => m.Category)
                .Include(m => m.Cinema)
                .FirstOrDefault(m => m.Id == id);

            if (movie is null) return NotFound();
            return View(movie);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var movie = _context.Movies
                .Include(m => m.MovieActors)
                .Include(m => m.MovieSubImages)
                .FirstOrDefault(m => m.Id == id);

            if (movie is null) return NotFound();

            if (movie.MainImg is not null)
                _movieService.RemoveImg(movie.MainImg, "movies");

            if (movie.MovieSubImages is not null)
                foreach (var img in movie.MovieSubImages)
                    _movieService.RemoveImg(img.ImageUrl, "movies\\sub");

            _context.Movies.Remove(movie);
            _context.SaveChanges();

            TempData["success_notification"] = "Movie Deleted Successfully";
            return RedirectToAction(nameof(Index));
        }
    }
}