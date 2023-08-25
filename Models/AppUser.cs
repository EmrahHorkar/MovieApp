    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Authentication;
    using MovieApp.Data;
    using Microsoft.EntityFrameworkCore;
    using System.ComponentModel.DataAnnotations.Schema;

    namespace MovieApp.Models
    {
        public class AppUser
        {
            public int Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public List<UserFavoriteMovie> FavoriteMovies { get; set; } = new List<UserFavoriteMovie>();
            public List<UserWatchedMovie> WatchedMovies { get; set; } = new List<UserWatchedMovie>();
            [NotMapped]
            public int PostCount { get; set; }
            public List<AppUser> Followers { get; set; } = new List<AppUser>();
            public List<AppUser> FollowedUsers { get; set; } = new List<AppUser>();
            public List<Comment> Comments { get; set; } = new List<Comment>();
            public List<UserMessage> ReceivedMessages { get; set; } = new List<UserMessage>();
    }
    }
