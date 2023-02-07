using BackendComputer.Models.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Type = BackendComputer.Models.Data.Type;

namespace BackendComputer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ApiTypeProductsController : ControllerBase
    {
        private readonly ComputerdbContext _context;

        public ApiTypeProductsController(ComputerdbContext context, IWebHostEnvironment environment)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Type>>> GetUser()
        {
            return await _context.Type.ToListAsync();
        }
    }
}
