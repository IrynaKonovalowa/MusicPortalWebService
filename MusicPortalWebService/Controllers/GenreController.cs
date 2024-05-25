using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicPortalWebAPIClient.Models;
using MusicPortalWebAPIClient.Repositories;

namespace MusicPortalWebAPIClient.Controllers
{
    [ApiController]
    [Route("api/Genres")]
    public class GenreController : ControllerBase
    {
        IRepository<Genre> repo;
       
        public GenreController(IRepository<Genre> r)
        {
            repo = r;
        }
        // GET: api/Genres
        [HttpGet]
        public async Task<ActionResult<List<GenreViewModel>>> GetGenres()
        {
            List<GenreViewModel> genres = new List<GenreViewModel>();

            foreach (Genre genre in await repo.GetAll())
            {
                GenreViewModel genre_vm = new GenreViewModel();
                genre_vm.Id = genre.Id;
                genre_vm.Name = genre.Name;
                genres.Add(genre_vm);
            }
            return genres;            
        }

        // GET: api/Genres/id
        [HttpGet("{id}")]
        public async Task<ActionResult<GenreViewModel>> GetGenre(int id)
        {
            var genre = await repo.Get(id);
            if (genre == null)
            {
                return NotFound();
            }

            GenreViewModel genre_vm = new GenreViewModel();
            genre_vm.Id = genre.Id;
            genre_vm.Name = genre.Name;

            return new ObjectResult(genre_vm);
        }

        // PUT: api/Genres
        [HttpPut]
        public async Task<ActionResult<Genre>> PutGenre(GenreViewModel g)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (await repo.Get(g.Id) == null)
            {
                return NotFound();
            }

            Genre genre = await repo.Get(g.Id);
            genre.Id = g.Id;
            genre.Name = g.Name;
            repo.Update(genre);
            await repo.Save();
            return Ok(genre);
        }
        // POST: api/Genres
        [HttpPost]
        public async Task<ActionResult<Genre>> PostUser(GenreViewModel g)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            Genre genre = new Genre();
            genre.Id = g.Id;
            genre.Name = g.Name;
            await repo.Create(genre);
            await repo.Save();

            return Ok(genre);
        }

        // DELETE: api/Genres/id
        [HttpDelete("{id}")]
        public async Task<ActionResult<Genre>> DeleteGenre(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var genre = await repo.Get(id);
            if (genre != null)
            {
                await repo.Delete(id);
            }
            await repo.Save();

            return Ok(genre);
        }
    }
}
