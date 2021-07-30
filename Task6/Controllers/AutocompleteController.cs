using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task6.Data;

namespace Task6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutocompleteController : Controller
    {
        private readonly GameContext _context;

        public AutocompleteController(GameContext context)
        {
            _context = context;
        }
        [Produces("application/json")]
        [HttpGet("search")]
        public IActionResult Search()
        {
            string term = HttpContext.Request.Query["term"].ToString();
            var Tags = _context.Games.Where(p => p.Tags.Contains(term)).Select(p => p.Tags).ToList();
            return Ok(SplitBy(Tags, term));
        }
        public List<string> SplitBy(List<string> Tags, string term)
        {
            List<string> tags = new List<string>();
            foreach (string s in Tags)
            {
                var TagsInGame = s.Split(",").ToList();
                foreach (string tag in TagsInGame)
                {
                    if (tag.ToLower().Contains(term.ToLower()))
                    {
                        tags.Add(tag);
                    }
                }
            }
            return tags;
        }
    }
}
