using BackendComputer.Models.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendComputer.Controllers
{


    [Route("[controller]")]
    [ApiController]
    public class ApiUsersController : ControllerBase
    {
        private readonly ComputerdbContext _context;
        private readonly IWebHostEnvironment _environment;

        public ApiUsersController(ComputerdbContext context, IWebHostEnvironment environment)

        {
            _context = context;
            _environment = environment;
        }

        // GET: api/ApiUsers
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<User>>> GetUser()
        //{
        //    return await _context.User.ToListAsync();
        //}


        // GET: api/ApiTitleUsersProducts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUser()
        {
            //return await _context.Products.ToListAsync();
            return await _context.User.Include(e => e.TitleNavigation).ToListAsync();
        }

        // GET: api/ApiUsers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.User.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }


        //Post ApiUsers/Register
        [Route("Register")]
        [HttpPost]
        public async Task<ActionResult> Register([FromForm] User data)
        {
            var result = await _context.User.FirstOrDefaultAsync(p => p.UserEmail.Equals(data.UserEmail));
            //if (result != null) return Conflict();
            if (result != null) return CreatedAtAction(nameof(Register), new { msg = "อีเมล์ซ้ำ" });

            await _context.User.AddAsync(data);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Register), new { msg = "OK", data });
        }

        // ApiUsers/Login
        [Route("Login")]
        [HttpPost]
        public async Task<ActionResult> Login([FromForm] User data)
        {
            var result = await _context.User.FirstOrDefaultAsync(p => p.UserEmail.Equals(data.UserEmail)
            && p.UserPassword.Equals(data.UserPassword));

            //if (result == null) return NotFound();
            if (result == null) return CreatedAtAction(nameof(Login), new { msg = "ไม่พบผู้ใช้" });

            return CreatedAtAction(nameof(Login), new { msg = "OK", data = result.Id });
        }
        


 

        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUsers(int id)
        {
            var users = await _context.User.FindAsync(id);
            if (users == null)
            {
                return NotFound();
            }

            _context.User.Remove(users);
            await _context.SaveChangesAsync();

            return users;
        }

        private bool UsersExists(int id)
        {
            return _context.User.Any(e => e.Id == id);
        }
    }
}
