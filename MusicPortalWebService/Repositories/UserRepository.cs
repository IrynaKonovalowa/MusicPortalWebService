using Microsoft.EntityFrameworkCore;
using MusicPortalWebAPIClient.Models;



namespace MusicPortalWebAPIClient.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private ClassContext db;

        public UserRepository(ClassContext context)
        {
            this.db = context;
        }

        public async Task<List<User>> GetAll()
        {
            return await db.Users.ToListAsync();
        }

        public async Task<User> Get(int id)
        {
            return await db.Users.FindAsync(id);
        }

        public async Task<User> Get(string name)
        {
            return await db.Users.Where(m => m.Login == name).FirstOrDefaultAsync();
        }

        public async Task Create(User user)
        {
            await db.Users.AddAsync(user);
        }

        public void Update(User user)
        {
            db.Entry(user).State = EntityState.Modified;
        }

        public async Task Delete(int id)
        {
            User? u = await db.Users.FindAsync(id);
            if (u != null)
                db.Users.Remove(u);
        }
        public async Task Save()
        {
            await db.SaveChangesAsync();
        }
    }
}
