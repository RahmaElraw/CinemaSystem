using CinemaSystem.Models;
using CinemaSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CinemaSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            ViewBag.MoviesCount = _context.Movies.Count();
            ViewBag.ActorsCount = _context.Actors.Count();
            ViewBag.CinemasCount = _context.Cinemas.Count();
            ViewBag.CategoriesCount = _context.Categories.Count();
            return View();
        }

        public IActionResult Details(int page = 1)
        {
            var movies = _context.Movies.AsQueryable();
            int totalPages = (int)Math.Ceiling(movies.Count() / 4.0);
            movies = movies.Skip((page - 1) * 4).Take(4);


            return View(new MovieWithRelatedVM()
            {
                Movies = movies.AsEnumerable(),
                TotalPages = totalPages,
                CurrentPage = page
            });

        }



       




        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
