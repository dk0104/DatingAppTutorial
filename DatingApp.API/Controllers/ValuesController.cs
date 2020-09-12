using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DatingApp.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ValuesController : ControllerBase
    {
        private readonly DataContext _context;
        public ValuesController(DataContext dataContext){
            _context = dataContext;
        }


        [HttpGet]
        public async Task<IActionResult> GetValues()
        {
            var values = await _context.ValueItems.ToListAsync();
            return Ok(values);
        }
    }
}
