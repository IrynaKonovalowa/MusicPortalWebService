using Microsoft.EntityFrameworkCore;


namespace MusicPortalWebAPIClient.Models
{
    public class ClassContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Song> Songs { get; set; }

        public ClassContext(DbContextOptions<ClassContext> options)
            : base(options)
        {
            if (Database.EnsureCreated())
            {
                Genre genre = new Genre();
                genre.Name = "Classic";
                Genre genre1 = new Genre();
                genre1.Name = "Rock";
                User user = new User();
                user.FirstName = "Ivan";
                user.LastName = "Ivanenko";
                user.Login = "Vanya";
                user.Email = "Vanya@gmail.com";
                User user1 = new User();
                user1.FirstName = "Vasylii";
                user1.LastName = "Vasylenko";
                user1.Login = "Vasya";
                user1.Email = "Vasya@gmail.com";

                Songs?.Add(new Song { Title="Turkish march", Genre = genre, Singer = "Mozart", User = user });
                Songs?.Add(new Song { Title="Melody of rain", Genre = genre, Singer = "Mozart", User = user });
                Songs?.Add(new Song { Title="Без бою", Genre = genre1, Singer = "Okean Elzy", User = user });
                Songs?.Add(new Song { Title="Відчуваю", Genre = genre1, Singer = "Okean Elzy", User = user });
                Songs?.Add(new Song { Title="Man who sold the world", Genre = genre1, Singer = "Nirvana", User = user1 });
                Songs?.Add(new Song { Title="Smells like teen spirit", Genre = genre1, Singer = "Nirvana", User = user1 });
                Songs?.Add(new Song { Title="It's my life", Genre = genre1, Singer = "Bon Jovi", User = user1 });
                Songs?.Add(new Song { Title="Never say goodbye", Genre = genre1, Singer = "Bon Jovi", User = user1 });

                SaveChanges();
            }
        }
    }
}
