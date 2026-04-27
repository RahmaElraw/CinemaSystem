
using CinemaSystem.DataAccess;
using CinemaSystem.Models;
using CinemaSystem.Services;
using CinemaSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace CinemaSystem.Controllers
{
    public class ActorController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ActorService _actorService;

        public ActorController(ApplicationDbContext context, ActorService actorService)
        {
            _context = context;
            _actorService = actorService;
        }

        public IActionResult Index()
        {
            var actors = _context.Actors.ToList();
            return View(actors);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new Actor());
        }

        [HttpPost]
        public IActionResult Create(Actor actor, IFormFile? ImageFile)
        {
            ModelState.Remove("ImageFile");
            ModelState.Remove("Image");
            ModelState.Remove("MovieActors");

            if (!ModelState.IsValid)
                return View(actor);

            if (ImageFile is not null && ImageFile.Length > 0)
            {
                var fileName = _actorService.SaveImg(ImageFile);
                if (fileName is not null) actor.Image = fileName;
            }

            _context.Actors.Add(actor);
            _context.SaveChanges();

            TempData["success_notification"] = "Actor Added Successfully";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var actor = _context.Actors.Find(id);
            if (actor is null) return NotFound();
            return View(actor);
        }

        [HttpPost]
        public IActionResult Edit(Actor actor, IFormFile? ImageFile)
        {
            ModelState.Remove("ImageFile");
            ModelState.Remove("Image");
            ModelState.Remove("MovieActors");

            if (!ModelState.IsValid)
                return View(actor);

            if (ImageFile is not null && ImageFile.Length > 0)
            {
                var oldActor = _context.Actors.AsNoTracking().FirstOrDefault(a => a.Id == actor.Id);
                if (oldActor?.Image is not null)
                    _actorService.RemoveImg(oldActor.Image);

                var fileName = _actorService.SaveImg(ImageFile);
                if (fileName is not null) actor.Image = fileName;
            }
            else
            {
                var oldActor = _context.Actors.AsNoTracking().FirstOrDefault(a => a.Id == actor.Id);
                actor.Image = oldActor?.Image;
            }

            _context.Actors.Update(actor);
            _context.SaveChanges();

            TempData["success_notification"] = "Actor Updated Successfully";
            return RedirectToAction(nameof(Index));
        }

       
        public IActionResult Delete(int id)
        {
            var actor = _context.Actors.Find(id);
            if (actor is null) return NotFound();

            return View(actor); 
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmation(int id)
        {
            var actor = _context.Actors.FirstOrDefault(a => a.Id == id);
            if (actor is null) return NotFound();

            if (actor.Image is not null)
                _actorService.RemoveImg(actor.Image);

            _context.Actors.Remove(actor);
            _context.SaveChanges();

            TempData["success_notification"] = "Actor Deleted Successfully";
            return RedirectToAction(nameof(Index));
        }
    }
}