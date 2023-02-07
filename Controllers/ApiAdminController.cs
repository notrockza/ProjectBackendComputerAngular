using BackendComputer.Models.Data;
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
    public class ApiAdminController : Controller
    {
        private readonly ComputerdbContext _context;

        public ApiAdminController(ComputerdbContext context)
        {
            _context = context;
        }

        // GET: api/ApiAdmin
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Admin>>> GetAdmin()
        {
            return await _context.Admin.ToListAsync();
        }

        // GET: api/ApiAdmin/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Admin>> GetAdmin(int id)
        {
            var admin = await _context.Admin.FindAsync(id);

            if (admin == null)
            {
                return NotFound();
            }

            return admin;
        }




        // ApiAdmin/Login
        [Route("AdminLogin")]
        [HttpPost]
        public async Task<ActionResult> AdminLogin([FromForm] Admin data)
        {
            var result = await _context.Admin.FirstOrDefaultAsync(p => p.AdminNme.Equals(data.AdminNme)
            && p.AdminPassword.Equals(data.AdminPassword));

            //if (result == null) return NotFound();
            if (result == null) return CreatedAtAction(nameof(AdminLogin), new { msg = "ไม่พบผู้ใช้" });

            return CreatedAtAction(nameof(AdminLogin), new { msg = "OKS", data = result });
        }
    }
}
