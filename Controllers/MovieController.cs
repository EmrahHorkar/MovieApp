//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;
//using MovieApp.Data;
//using MovieApp.Models; 
//using System.Linq;
//using System.Threading.Tasks;

//namespace MovieApp.Controllers
//{
//    public class MovieController : Controller
//    {
//        private readonly ApplicationDbContext _context;

//        public MovieController(ApplicationDbContext context)
//        {
//            _context = context;
//        }

//        public IActionResult CreateMovie()
//        {
//            var categories = _context.Categories.ToList();
//            var castMembers = _context.CastMembers.ToList();

//            ViewBag.Categories = categories.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name });
//            ViewBag.CastMembers = castMembers.Select(cm => new SelectListItem { Value = cm.Id.ToString(), Text = cm.Name });

//            return View();
//        }

//        [HttpPost]
//        public async Task<IActionResult> CreateMovie(Movie movie)
//        {
//            foreach (var categoryId in movie.SelectedCategoryIds)
//            {
//                var category = await _context.Categories.FindAsync(categoryId);
//                if (category != null)
//                {
//                    movie.MovieCategories.Add(new MovieCategory { Category = category });
//                }
//            }

//            foreach (var castMemberId in movie.SelectedCastMemberIds)
//            {
//                var castMember = await _context.CastMembers.FindAsync(castMemberId);
//                if (castMember != null)
//                {
//                    movie.Cast.Add(castMember);
//                }
//            }

//            _context.Add(movie);
//            await _context.SaveChangesAsync();
//            return RedirectToAction("Movies");
//        }
//        public IActionResult EditMovie(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var movie = _context.Movies.Include(m => m.MovieCategories).Include(m => m.Cast).FirstOrDefault(m => m.Id == id);

//            if (movie == null)
//            {
//                return NotFound();
//            }

//            var categories = _context.Categories.ToList();
//            var castMembers = _context.CastMembers.ToList();

//            ViewBag.Categories = categories.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name });
//            ViewBag.CastMembers = castMembers.Select(cm => new SelectListItem { Value = cm.Id.ToString(), Text = cm.Name });

//            return View(movie);
//        }

//        public IActionResult DetailsMovie(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var movie = _context.Movies
//                .Include(m => m.MovieCategories)
//                .Include(m => m.Cast)
//                .Include(m => m.Comments)
//                    .ThenInclude(c => c.User)
//                .FirstOrDefault(m => m.Id == id);

//            if (movie == null)
//            {
//                return NotFound();
//            }

//            return View(movie);
//        }

//        [HttpPost]
//        public async Task<IActionResult> UpdateMovie(Movie movie)
//        {
//            var existingMovie = await _context.Movies
//                .Include(m => m.MovieCategories)
//                .Include(m => m.Cast)
//                .FirstOrDefaultAsync(m => m.Id == movie.Id);

//            if (existingMovie != null)
//            {
//                existingMovie.Title = movie.Title;
//                existingMovie.Description = movie.Description;
//                existingMovie.Director = movie.Director;
//                existingMovie.ImageUrl = movie.ImageUrl;

//                existingMovie.MovieCategories.Clear();
//                foreach (var categoryId in movie.SelectedCategoryIds)
//                {
//                    var category = await _context.Categories.FindAsync(int.Parse(categoryId));
//                    if (category != null)
//                    {
//                        existingMovie.MovieCategories.Add(new MovieCategory { Category = category });
//                    }
//                }

//                existingMovie.Cast.Clear();
//                foreach (var castMemberId in movie.SelectedCastMemberIds)
//                {
//                    var castMember = await _context.CastMembers.FindAsync(int.Parse(castMemberId));
//                    if (castMember != null)
//                    {
//                        existingMovie.Cast.Add(castMember);
//                    }
//                }

//                _context.Update(existingMovie);
//                await _context.SaveChangesAsync();
//            }

//            return RedirectToAction("Movies");
//        }

//        public async Task<IActionResult> DeleteMovie(int? id)
//        {
//            var movie = await _context.Movies.FindAsync(id);

//            if (movie == null)
//            {
//                return NotFound();
//            }

//            _context.Remove(movie);
//            await _context.SaveChangesAsync();
//            return RedirectToAction("Movies");
//        }

//        // Rest of the methods...

//    }
//}
