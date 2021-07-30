using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Task6.Data;
using Task6.Hubs;

namespace Task6.Controllers
{
    public class GameController : Controller
    {
        private readonly GameContext _context;
        private readonly IHubContext<GameHub> _hubContext;

        public GameController(GameContext context, IHubContext<GameHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        public IActionResult Field(string name, string move, string join)
        {
            ViewBag.Join = join;
            ViewBag.Name = name;
            ViewBag.TypeOfMove = move;
            return View();
        }
    }
}
