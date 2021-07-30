using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Task6.Data;
using Task6.Models;
using Task6.ViewModels;

namespace Task6.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        static List<Game> games = new List<Game>();
        private readonly ILogger<HomeController> _logger;
        private readonly GameContext _context;

        public HomeController(ILogger<HomeController> logger, GameContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.Games = _context.Games;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewGame(NewGameViewModel model)
        {
            if (ModelState.IsValid)
            {
                Game game = new Game { Name = model.Name, Tags = model.Tags };
                _context.Games.Add(game);
                games.Add(game);
                await _context.SaveChangesAsync();
                return AddPlayer(game, true);
            }
            return RedirectToAction("Index", "Home", model);
        }

        public IActionResult AddPlayer(Game game, bool flag)
        {
            string TypeOfMove;
            AddPlayerToGame(game, User.Identity.Name, out TypeOfMove);
            if (flag)
            {
                return RedirectToAction("Field", "Game", new { name = game.Name, move = TypeOfMove, join = "new" });
            }
            else
            {
                return RedirectToAction("Field", "Game", new { name = game.Name, move = TypeOfMove, join = "join" });
            }

        }

        public IActionResult JoinTheGame(string name)
        {
            Game game = _context.Games.FirstOrDefault(u => u.Name == name);
            if (game.Player1 == null || game.Player2 == null)
            {
                return AddPlayer(game, false);
            }
            else
            {
                return RedirectToAction("index");
            }
        }

        public void AddPlayerToGame(Game game, string name, out string TypeOfMove)
        {
            if (game.Player1 == null)
            {
                game.Player1 = name;
                TypeOfMove = "x";
            }
            else if (game.Player2 == null)
            {
                game.Player2 = name;
                TypeOfMove = "o";
            }
            else
            {
                TypeOfMove = "";
            }
            _context.SaveChanges();
        }

        public IActionResult RemovePlayerFromGame(string name)
        {
            Game CurrentGame = _context.Games.FirstOrDefault(g => g.Name == name);
            if (CurrentGame.Player1 == User.Identity.Name)
            {
                CurrentGame.Player1 = null;
            }
            else if (CurrentGame.Player2 == User.Identity.Name)
            {
                CurrentGame.Player2 = null;
            }
            _context.SaveChanges();
            CheckOnEmptyGame(CurrentGame);
            return RedirectToAction("Index", "Home");
        }

        public void CheckOnEmptyGame(Game game)
        {
            if (game.Player1 == null && game.Player2 == null)
            {
                _context.Games.Remove(game);
                games.Remove(game);
                _context.SaveChanges();
            }
        }
    }
}
