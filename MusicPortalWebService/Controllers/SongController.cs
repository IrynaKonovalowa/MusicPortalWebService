using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicPortalWebAPIClient.Models;
using MusicPortalWebAPIClient.Repositories;

namespace MusicPortalWebAPI.Controllers
{
    [ApiController]
    [Route("api/Songs")]
    public class SongController : ControllerBase
    {
        IRepository<Song> repo;
        IRepository<User> u_repo;
        IRepository<Genre> g_repo;

        public SongController(IRepository<Song> r, IRepository<User> u_r, IRepository<Genre> g_r)
        {
            repo = r;
            u_repo = u_r;
            g_repo = g_r;
        }
        // GET: api/Songs
        [HttpGet]
        public async Task<ActionResult<List<SongViewModel>>> GetSongs()
        {
            List<SongViewModel> songs = new List<SongViewModel>();

            foreach (Song song in await repo.GetAll())
            {
                SongViewModel song_vm = new SongViewModel();
                song_vm.Id = song.Id;
                song_vm.Title = song.Title;
                song_vm.Singer = song.Singer;
                song_vm.Genre = song.Genre.Name;
                song_vm.User = song.User.Login;
                songs.Add(song_vm);
            }
            return songs;
        }

        // GET: api/Songs/id
        [HttpGet("{id}")]
        public async Task<ActionResult<Song>> GetSong(int id)
        {
            var song = await repo.Get(id);
            if (song == null)
            {
                return NotFound();
            }
            SongViewModel song_vm = new SongViewModel();
            song_vm.Id = song.Id;
            song_vm.Title = song.Title;
            song_vm.Singer = song.Singer;
            song_vm.Genre = song.Genre.Name;
            song_vm.User = song.User.Login;

            return new ObjectResult(song_vm);
        }

        // PUT: api/Songs
        [HttpPut]
        public async Task<ActionResult<Song>> PutSong(SongViewModel s)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (await repo.Get(s.Id) == null)
            {
                return NotFound();
            }

            Song song = await repo.Get(s.Id);
            User user = await u_repo.Get(s.User);
            Genre genre = await g_repo.Get(s.Genre);
            song.Id = s.Id;
            song.Title = s.Title;
            song.Singer = s.Singer;
            song.User = user;
            song.Genre = genre;            

            repo.Update(song);
            await repo.Save();
            return Ok(song);
        }
        // POST: api/Songs
        [HttpPost]
        public async Task<ActionResult<Song>> PostSong(SongViewModel s)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Song song = new Song();
            User user = await u_repo.Get(s.User);
            Genre genre = await g_repo.Get(s.Genre);
            song.Title = s.Title;
            song.Singer = s.Singer;
            song.User = user;
            song.Genre = genre;

            await repo.Create(song);
            await repo.Save();

            return Ok(song);
        }

        // DELETE: api/Songs/id
        [HttpDelete("{id}")]
        public async Task<ActionResult<Song>> DeleteSong(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var song = await repo.Get(id);
            if (song != null)
            {
                await repo.Delete(id);
            }
            await repo.Save();

            return Ok(song);
        }
    }
}
