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
    public class ApiTitlesUsersController : Controller
    {
        private readonly ComputerdbContext _context;

        public ApiTitlesUsersController(ComputerdbContext context, IWebHostEnvironment environment)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Title>>> GetUser()
        {
            return await _context.Title.ToListAsync();
        }
    }
}
