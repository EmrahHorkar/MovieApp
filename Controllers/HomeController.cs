using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MovieApp.Data;
using MovieApp.Models;
using System.Diagnostics;
using System.Security.Claims;
using MovieApp.Services;
using Microsoft.Extensions.DependencyInjection;

namespace MovieApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;
        private readonly MessageProducerService _messageProducer;
        private readonly IServiceScopeFactory _serviceScopeFactory; // Add this line

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, IUserService userService, MessageProducerService messageProducer, IServiceScopeFactory serviceScopeFactory) // Add IServiceScopeFactory parameter
        {
            _logger = logger;
            _context = context;
            _userService = userService;
            _messageProducer = messageProducer;
            _serviceScopeFactory = serviceScopeFactory; // Assign the serviceScopeFactory
        }

        public IActionResult SendMessage(int receiverId, string content)
        {
            if (ModelState.IsValid)
            {
                var senderId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

                var message = new UserMessage
                {
                    SenderId = senderId,
                    ReceiverId = receiverId,
                    Content = content
                };

                _messageProducer.SendMessage(message); // Send message to RabbitMQ queue

                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                    context.UserMessages.Add(message); // Save the message to the database
                    context.SaveChanges();
                }
            }
            else
            {
                // Handle invalid ModelState, e.g., return a View with error messages
                return View();
            }

            return RedirectToAction("Profile", new { id = receiverId });
        }
        public IActionResult UserMessages()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

                var userMessages = _context.UserMessages
                    .Where(um => um.ReceiverId == userId)
                    .ToList();

                return View(userMessages);
            }

            return RedirectToAction("Login"); // Redirect unauthenticated users
        }
        //public IActionResult SentMessages(int userId)
        //{
        //    var sentMessages = _context.UserMessage
        //        .Where(message => message.SenderId == userId)
        //        .ToList();

        //    return View(sentMessages);
        //}

        public async Task<IActionResult> AddCategory(Category category)
        {
            await _context.AddAsync(category);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Category));
        }
        public IActionResult Category()
        {
            List<Category> list = _context.Categories.ToList();
            return View(list);
        }
        public async Task<IActionResult> DeleteCategory(int? Id)
        {
            Category category = await _context.Categories.FindAsync(Id);
            _context.Remove(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Category));
        }
        public IActionResult EditCategory(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Category category = _context.Categories.Find(id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCategory(Category category)
        {
            _context.Update(category);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Category));
        }
        public IActionResult CreateMovie()
        {
            
            var movie = new Movie { CategoryMovies = _context.CategoryMovies.Include(cm => cm.Category).ToList() };
            ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name");
            return View(movie);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMovie(Movie movie, int[] categoryIds)
        {
            
            foreach (var categoryId in categoryIds)
            {
                var categoryMovie = new CategoryMovie
                {
                    CategoryId = categoryId,
                    Movie = movie
                };
                _context.CategoryMovies.Add(categoryMovie);
            }

            _context.Add(movie);
            await _context.SaveChangesAsync();
            return RedirectToAction("Movies");
        }
        public IActionResult EditMovie(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = _context.Movies
                .Include(m => m.CategoryMovies)
                .FirstOrDefault(m => m.Id == id);

            if (movie == null)
            {
                return NotFound();
            }

           
            movie.CategoryIds = movie.CategoryMovies.Select(cm => cm.CategoryId).ToList();

            var categories = _context.Categories.ToList();
            var categoryItems = categories.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name }).ToList();
            ViewBag.Categories = categoryItems;

            return View(movie);
        }

        public IActionResult DetailsMovie(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            
            var movie = _context.Movies
                .Include(m => m.CategoryMovies)
                    .ThenInclude(cm => cm.Category) 
                .Include(m => m.Comments)
                    .ThenInclude(c => c.User)
                .FirstOrDefault(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }
        public async Task<IActionResult> DeleteMovie(int? id)
        {
            var movie = await _context.Movies.FindAsync(id);

            if (movie == null)
            {
                return NotFound();
            }

            _context.Remove(movie);
            await _context.SaveChangesAsync();
            return RedirectToAction("Movies");
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(int movieId, string commentText)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var user = await _context.Users.FindAsync(userId);
            var movie = await _context.Movies.FindAsync(movieId);

            if (user != null && movie != null)
            {
                var comment = new Comment
                {
                    Description = commentText,
                    MovieId = movieId,
                    UserId = userId
                };

                _context.Comments.Add(comment);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("DetailsMovie", new { id = movieId });
        }



        [HttpPost]
        public async Task<IActionResult> UpdateMovie(Movie movie, int[] categoryIds)
        {
            
            var movieToUpdate = _context.Movies.Include(m => m.CategoryMovies).FirstOrDefault(m => m.Id == movie.Id);

            
            foreach (var cm in movieToUpdate.CategoryMovies)
            {
                _context.CategoryMovies.Remove(cm);
            }

           
            foreach (var categoryId in categoryIds)
            {
                var categoryMovie = new CategoryMovie
                {
                    CategoryId = categoryId,
                    MovieId = movie.Id
                };
                _context.CategoryMovies.Add(categoryMovie);
            }

            
            movieToUpdate.Title = movie.Title;
            movieToUpdate.Description = movie.Description;
            movieToUpdate.Director = movie.Director;
            movieToUpdate.ImageUrl = movie.ImageUrl;

            await _context.SaveChangesAsync();
            return RedirectToAction("Movies");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);

            if (user == null)
            {
                ModelState.AddModelError("", "Invalid login credentials");
                return View();
            }

            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, user.FirstName + " " + user.LastName),
        new Claim(ClaimTypes.Email, user.Email),
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),

    };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {

            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

            return RedirectToAction("Movies", "Home");
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(AppUser user)
        {
            if (ModelState.IsValid)
            {

                _context.Users.Add(user);
                await _context.SaveChangesAsync();



                return RedirectToAction("Index", "Home");
            }

            return View(user);
        }
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public async Task<IActionResult> FollowUser(int userId)
        {
            var followerId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var follower = await _context.Users.FindAsync(followerId);
            var userToFollow = await _context.Users.FindAsync(userId);

            if (follower != null && userToFollow != null)
            {
                follower.FollowedUsers.Add(userToFollow);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Profile", new { id = userId });
        }

        [HttpPost]
        public async Task<IActionResult> UnfollowUser(int userId)
        {
            var followerId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var follower = await _context.Users.FindAsync(followerId);
            var userToUnfollow = await _context.Users.FindAsync(userId);

            if (follower != null && userToUnfollow != null)
            {

                follower = _context.Users.Include(u => u.FollowedUsers).FirstOrDefault(u => u.Id == followerId);

                follower.FollowedUsers.Remove(userToUnfollow);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Profile", new { id = userId });
        }


        public IActionResult Profile(int? id)
        {
            if (id == null)
            {
                var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                id = userId;
            }

            var user = _context.Users
                .Include(u => u.FavoriteMovies)
                .Include(u => u.WatchedMovies)
                .Include(u => u.Followers)
                .Include(u => u.FollowedUsers)
                .Include(u => u.ReceivedMessages) // Include received messages here
                .FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            var postCount = _context.Posts
                .Count(p => p.AuthorId == user.Id);

            var allMovies = _context.Movies.ToList();

            ViewBag.AllMovies = allMovies;

            user.PostCount = postCount;

            return View(user);
        }


        public IActionResult UserPosts(int userId)
        {
            var userPosts = _context.Posts.Where(p => p.AuthorId == userId).ToList();
            return View(userPosts);
        }


        [HttpPost]
        public async Task<IActionResult> AddFavoriteMovie(int movieId)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login");
            }

            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var user = await _context.Users
                .Include(u => u.FavoriteMovies)
                .FirstOrDefaultAsync(u => u.Id == userId);
            var movie = await _context.Movies.FindAsync(movieId);

            if (user != null && movie != null)
            {
                user.FavoriteMovies.Add(new UserFavoriteMovie { User = user, Movie = movie });
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Profile");
        }

        [HttpPost]
        public async Task<IActionResult> AddWatchedMovie(int movieId)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login");
            }

            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var user = await _context.Users
                .Include(u => u.WatchedMovies)
                .FirstOrDefaultAsync(u => u.Id == userId);
            var movie = await _context.Movies.FindAsync(movieId);

            if (user != null && movie != null)
            {
                user.WatchedMovies.Add(new UserWatchedMovie { User = user, Movie = movie });
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Profile");
        }

        public IActionResult CreatePost()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost(Post post)
        {
            if (User.Identity.IsAuthenticated)
            {
                int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));


                var user = await _context.Users.FindAsync(userId);


                post.Author = user;

                post.CreatedAt = DateTime.Now;
                _context.Posts.Add(post);
                await _context.SaveChangesAsync();
                return RedirectToAction("Posts");
            }
            else
            {
                ModelState.AddModelError("", "You must be logged in to create a post.");
                return View(post);
            }
        }


        public IActionResult Posts()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

                var followedUsers = _context.Users.Include(u => u.FollowedUsers).FirstOrDefault(u => u.Id == userId)?.FollowedUsers;

                var followedUserIds = followedUsers?.Select(u => u.Id) ?? new List<int>();

                var userPosts = _context.Posts.Include(p => p.Author)
                                             .Where(p => p.AuthorId == userId)
                                             .ToList();

                var followedUsersPosts = _context.Posts.Include(p => p.Author)
                                                       .Where(p => followedUserIds.Contains(p.AuthorId))
                                                       .ToList();

                var allPosts = userPosts.Concat(followedUsersPosts).ToList();

                return View(allPosts);
            }

            return View(new List<Post>());
        }


        [HttpPost]
        public async Task<IActionResult> DeletePost(int postId)
        {
            var post = await _context.Posts.FindAsync(postId);

            if (post == null)
            {
                return NotFound();
            }

            _context.Remove(post);
            await _context.SaveChangesAsync();
            return RedirectToAction("Posts");
        }
        [HttpGet]
        public async Task<IActionResult> SearchUsers(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return Json(null);
            }

            var foundUsers = await _userService.SearchUsersAsync(searchTerm);

            var suggestions = foundUsers.Select(user => new
            {
                id = user.Id,
                fullName = $"{user.FirstName} {user.LastName}"
            }).ToList();

            return Json(suggestions);
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Movies(string searchQuery, List<int> categoryIds)
        {
            var movies = _context.Movies.Include(m => m.CategoryMovies).ThenInclude(cm => cm.Category).AsQueryable();

            if (categoryIds != null && categoryIds.Any())
            {
                movies = movies.Where(m => m.CategoryMovies.Any(cm => categoryIds.Contains(cm.CategoryId)));
            }

            if (!string.IsNullOrEmpty(searchQuery))
            {
                movies = movies.Where(m => m.Title.Contains(searchQuery) || m.Description.Contains(searchQuery) || m.Director.Contains(searchQuery));
            }

            var categories = _context.Categories.ToList();
            ViewData["Categories"] = categories;

            return View(movies.ToList());
        }

        public IActionResult SeedCategoryMovies()
        {
            
            var movies = _context.Movies.ToList();
            
            var random = new Random();
            
            foreach (var movie in movies)
            {
               
                var numberOfCategories = random.Next(1, 4);
                
                var categories = _context.Categories.OrderBy(c => Guid.NewGuid()).Take(numberOfCategories).ToList();
                
                foreach (var category in categories)
                {
                    
                    var categoryMovie = new CategoryMovie { MovieId = movie.Id, CategoryId = category.Id };
                    
                    _context.CategoryMovies.Add(categoryMovie);
                }
            }
            // Save the changes to the database
            _context.SaveChanges();
            // Return a message indicating success
            return Content("CategoryMovies seeded successfully.");
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