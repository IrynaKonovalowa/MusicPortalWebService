using Microsoft.EntityFrameworkCore;
using MusicPortalWebAPIClient.Models;



namespace MusicPortalWebAPIClient.Repositories
{
    public class SongRepository : IRepository<Song>
    {
        private ClassContext db;

        public SongRepository(ClassContext context)
        {
            this.db = context;
        }

        public async Task<List<Song>> GetAll()
        {
            return await db.Songs.ToListAsync();
        }

        public async Task<Song> Get(int id)
        {
            return await db.Songs.FindAsync(id);
        }

        public async Task<Song> Get(string name)
        {
            return await db.Songs.Where(m => m.Title == name).FirstOrDefaultAsync();
        }

        public async Task Create(Song song)
        {
            await db.Songs.AddAsync(song);
        }

        public void Update(Song song)
        {
            db.Entry(song).State = EntityState.Modified;
        }

        public async Task Delete(int id)
        {
            Song? s = await db.Songs.FindAsync(id);
            if (s != null)
                db.Songs.Remove(s);
        }
        public async Task Save()
        {
            await db.SaveChangesAsync();
        }
    }
}
