using CinemaSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace CinemaSystem.Controllers
{
    public class CinemaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CinemaController()
        {
            _context = new();
        }

        public IActionResult Index()
        {
            var cinemas = _context.Cinemas.ToList();
            return View(cinemas);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Cinema cinema)
        {
            if (ModelState.IsValid)
            {
                _context.Cinemas.Add(cinema);
                _context.SaveChanges();
                TempData["success_notification"] = "Cinema Added Successfully";
                return RedirectToAction("Index");
            }
            return View(cinema);
        }

        public IActionResult Edit(int id)
        {
            var cinema = _context.Cinemas.Find(id);
            if (cinema == null) return NotFound();
            return View(cinema);
        }

        [HttpPost]
        public IActionResult Edit(Cinema cinema)
        {
            if (ModelState.IsValid)
            {
                _context.Cinemas.Update(cinema);
                _context.SaveChanges();
                TempData["success_notification"] = "Cinema Updated Successfully";
                return RedirectToAction("Index");
            }
            return View(cinema);
        }

        public IActionResult Delete(int id)
        {
            var cinema = _context.Cinemas.Find(id);
            if (cinema == null) return NotFound();
            return View(cinema);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var cinema = _context.Cinemas.Find(id);
            _context.Cinemas.Remove(cinema);
            _context.SaveChanges();
            TempData["success_notification"] = "Cinema Deleted Successfully";
            return RedirectToAction("Index");
        }
    }
}