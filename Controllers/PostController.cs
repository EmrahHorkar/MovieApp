using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApp.Data;
using MovieApp.Models;

public class PostController : Controller
{
    private readonly ApplicationDbContext _context;

    public PostController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    [Authorize]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create(Post post)
    {
        if (ModelState.IsValid)
        {
            // Get the current user
            var currentUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == User.Identity.Name);

            if (currentUser != null)
            {
                post.Author = currentUser;
                _context.Posts.Add(post);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
        }

        return View(post);
    }
    public IActionResult Posts()
    {
        List<Post> posts = _context.Posts.Include(p => p.Author).ToList();
        return View(posts);
    }
    // Other actions for editing, deleting, and displaying posts
}
