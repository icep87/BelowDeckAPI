using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BelowDeckAPI.Models.Persistence;
using BelowDeckAPI.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BelowDeckAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SiteController : Controller
    {
        //We use the underline in the name to distinguish class member from local member
        private readonly DatabaseContext _context;

        public SiteController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/values
        [HttpGet]
        public async Task<ActionResult<Dictionary<string, string>>> Get()
        {
            var Settings = await _context.Settings.Where(x => x.Flags.Equals("PUBLIC")).Select(x => new Setting { Key = x.Key, Value = x.Value }).ToDictionaryAsync(x => x.Key, x => x.Value); ;
            var MainNavigation = await _context.MenuItems.Where(x => x.MenuId.Equals(int.Parse(Settings["main_navigation"]))).Include(x => x.Post).Select(x => new
            { label = x.Post.Title,
              uri = x.Post.Uri
            }).ToListAsync();
            var FooterNavigation = await _context.MenuItems.Where(x => x.MenuId.Equals(int.Parse(Settings["footer_navigation"]))).Include(x => x.Post).Select(x => new
            {
                label = x.Post.Title,
                uri = x.Post.Uri
            }).ToListAsync();
            Settings["main_navigation"] = JsonSerializer.Serialize(MainNavigation);
            Settings["footer_navigation"] = JsonSerializer.Serialize(FooterNavigation);

            return Settings;
        }
    }
}