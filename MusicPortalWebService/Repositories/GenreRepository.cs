using Microsoft.EntityFrameworkCore;
using MusicPortalWebAPIClient.Models;



namespace MusicPortalWebAPIClient.Repositories
{
    public class GenreRepository : IRepository<Genre>
    {
        private ClassContext db;

        public GenreRepository(ClassContext context)
        {
            db = context;
        }

        public async Task<List<Genre>> GetAll()
        {
            return await db.Genres.ToListAsync();
        }

        public async Task<Genre> Get(int id)
        {
            return await db.Genres.FindAsync(id);
        }

        public async Task<Genre> Get(string name)
        {
            return await db.Genres.Where(m => m.Name == name).FirstOrDefaultAsync();
        }

        public async Task Create(Genre genre)
        {
            await db.Genres.AddAsync(genre);
        }

        public void Update(Genre genre)
        {
            db.Entry(genre).State = EntityState.Modified;
        }

        public async Task Delete(int id)
        {
            Genre? g = await db.Genres.FindAsync(id);
            if (g != null)
                db.Genres.Remove(g);
        }
        public async Task Save()
        {
            await db.SaveChangesAsync();
        }
    }
}
