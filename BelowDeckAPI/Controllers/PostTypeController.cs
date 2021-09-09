using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BelowDeckAPI.Data;
using BelowDeckAPI.Models.Persistence;

namespace BelowDeckAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostTypeController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public PostTypeController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/PostType
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostType>>> GetPostTypes()
        {
            return await _context.PostTypes.ToListAsync();
        }
       
    }
}
