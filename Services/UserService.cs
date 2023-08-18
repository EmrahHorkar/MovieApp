using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieApp.Data;
using MovieApp.Models;

namespace MovieApp.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<AppUser>> SearchUsersAsync(string searchTerm)
        {
            // Implement the user search logic here
            // For example:
            return await _context.Users
                .Where(u => u.FirstName.Contains(searchTerm) || u.LastName.Contains(searchTerm))
                .ToListAsync();
        }
    }
}
