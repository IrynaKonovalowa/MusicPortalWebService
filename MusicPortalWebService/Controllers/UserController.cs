using Castle.Components.DictionaryAdapter.Xml;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicPortalWebAPIClient.Models;
using MusicPortalWebAPIClient.Repositories;

namespace MusicPortalWebAPI.Controllers
{
    [ApiController]
    [Route("api/Users")]
    public class UserController : ControllerBase
    {
        IRepository<User> repo;

        public UserController(IRepository<User> r)
        {
            repo = r;
        }
        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<List<UserViewModel>>> GetUsers()
        {
            List<UserViewModel> users = new List<UserViewModel>();

            foreach (User user in await repo.GetAll())
            {
                UserViewModel user_vm = new UserViewModel();
                user_vm.Id = user.Id;
                user_vm.FirstName = user.FirstName;
                user_vm.LastName = user.LastName;
                user_vm.Login = user.Login;
                user_vm.Email = user.Email;
                users.Add(user_vm);
            }
            return users;
        }

        // GET: api/Users/id
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await repo.Get(id);
            if (user == null)
            {
                return NotFound();
            }
            UserViewModel user_vm = new UserViewModel();
            user_vm.Id = user.Id;
            user_vm.FirstName = user.FirstName;
            user_vm.LastName = user.LastName;
            user_vm.Login = user.Login;
            user_vm.Email = user.Email;

            return new ObjectResult(user_vm);
        }

        // PUT: api/Users
        [HttpPut]
        public async Task<ActionResult<User>> PutUser(UserViewModel u)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (await repo.Get(u.Id) == null)
            {
                return NotFound();
            }

            User user = await repo.Get(u.Id);
            user.Id = user.Id;
            user.FirstName = u.FirstName;
            user.LastName = u.LastName;
            user.Login = u.Login;
            user.Email = u.Email;

            repo.Update(user);
            await repo.Save();
            return Ok(user);
        }
        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(UserViewModel u)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            foreach (var us in await repo.GetAll())
            {
                if (us.Email == u.Email)
                {                    
                    return Ok("no");
                }
            }
            User user = new User();
            user.FirstName = u.FirstName;
            user.LastName = u.LastName;
            user.Login = u.Login;
            user.Email = u.Email;

            await repo.Create(user);
            await repo.Save();

            return Ok(user);
        }

        // DELETE: api/Users/id
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await repo.Get(id);
            if (user != null)
            {
                await repo.Delete(id);
            }
            await repo.Save();

            return Ok(user);
        }
    }
}
