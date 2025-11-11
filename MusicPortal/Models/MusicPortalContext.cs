using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Music_Portal.Models
{
    public class MusicPortalContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Song> Songs { get; set; }

        public MusicPortalContext(DbContextOptions<MusicPortalContext> options)
            : base(options)
        {
            if (Database.EnsureCreated())
            {
                Genres.Add(new Genre { Name = "Рок", Description = "Рок-музика" });
                Genres.Add(new Genre { Name = "Поп", Description = "Популярна музика" });
                Genres.Add(new Genre { Name = "Джаз", Description = "Джазова музика" });
                Genres.Add(new Genre { Name = "Класика", Description = "Класична музика" });

                Users.Add(new User { Username = "admin", Email = "admin@musicportal.com", Password = "admin123", Role = "Admin" });
                Users.Add(new User { Username = "user1", Email = "user1@musicportal.com", Password = "user123", Role = "User" });

                SaveChanges();

                Songs.Add(new Song { Title = "Bohemian Rhapsody", Artist = "Queen", GenreId = 1, Duration = 354 });
                Songs.Add(new Song { Title = "Billie Jean", Artist = "Michael Jackson", GenreId = 2, Duration = 294 });
                Songs.Add(new Song { Title = "Take Five", Artist = "Dave Brubeck", GenreId = 3, Duration = 324 });

                SaveChanges();
            }
        }
    }
}