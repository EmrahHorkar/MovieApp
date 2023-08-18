using System.Collections.Generic;
using System.Threading.Tasks;
using MovieApp.Models;

namespace MovieApp.Services
{
    public interface IUserService
    {
        Task<List<AppUser>> SearchUsersAsync(string searchTerm);
    }
}
