using GameService.Entities;
using GameService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Numerics;

namespace GameService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : Controller
    {
        private readonly GameServiceContext _context;

        public GameController(GameServiceContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets all the game collection from DB
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> GetAllGames()
        {
            List<GameDTO> gameClientList = new List<GameDTO>();

            foreach (Game game in _context.Games)
            {
                GameDTO gameClient = new GameDTO()
                {
                    ID = game.gameID,
                    Title = game.Title,
                    Description = game.Description,
                    Price = game.Price,
                    Publisher = game.Publisher,
                    ReleaseYear = game.ReleaseYear,
                    GameGenre = _context.Genres.FirstOrDefault(g => g.GenreID == game.GenreID)?.GenreName,
                    GamePlatform = _context.Platforms.FirstOrDefault(p => p.PlatformID == game.PlatformID)?.Name
                };

                gameClientList.Add(gameClient);
            }

            return Ok(gameClientList);

            //return Ok(await _context.Games.ToListAsync());
        }

        /// <summary>
        /// Gets all genre from DB for the client select option
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GenresPlatforms")]
        public async Task<ActionResult> GetAllGenresPlatforms()
        {
            GenrePlatformDTO clientList = new()
            {
                GenreList = _context.Genres.ToList(),
                PlatformList = _context.Platforms.ToList()
            };

            return Ok(clientList);
        }

        /// <summary>
        /// Gets specific game by Id from DB
        /// </summary>
        /// <param name="id">refers to gameID</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult> GetGameById(int id)
        {
            var game = await _context.Games.FindAsync(id);

            GameDTO gameById = new()
            {
                ID = game.gameID,
                Title = game.Title,
                Description = game.Description,
                Price = game.Price,
                Publisher = game.Publisher,
                ReleaseYear = game.ReleaseYear,
                GameGenre = _context.Genres.FirstOrDefault(g => g.GenreID == game.GenreID)?.GenreName,
                GamePlatform = _context.Platforms.FirstOrDefault(p => p.PlatformID == game.PlatformID)?.Name
            };

            if (gameById == null)
            {
                return NotFound();
            }

            return Ok(gameById);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="search">search query parameter from client</param>
        /// <returns>A list of game or games based on search query</returns>
        [HttpGet("search")]
        public async Task<ActionResult> GetGameBySearch(string search)
        {
            IQueryable<Game> games = _context.Games;

            if (!String.IsNullOrEmpty(search))
            {
                games = games.Where(g => g.Title.Contains(search));
            }

            List<GameDTO> gameClientList = new List<GameDTO>();

            foreach (Game game in games)
            {
                GameDTO gameClient = new GameDTO()
                {
                    ID = game.gameID,
                    Title = game.Title,
                    Description = game.Description,
                    Price = game.Price,
                    Publisher = game.Publisher,
                    ReleaseYear = game.ReleaseYear,
                    GameGenre = _context.Genres.FirstOrDefault(g => g.GenreID == game.GenreID)?.GenreName,
                    GamePlatform = _context.Platforms.FirstOrDefault(p => p.PlatformID == game.PlatformID)?.Name
                };

                gameClientList.Add(gameClient);
            }

            return Ok(gameClientList);
        }



        /*List<string> genres = new List<string> { "Action", "Adventure" };

        string url = "https://localhost:7108/api/Game/preference?";
		
		for(int i = 0; i <= genres.Count -1; i++)
		{
			url += "GamesByGenre=" + genres[i];
			
			if(genres.Count > 1)
			{
				url += "&";
			}
        }
            url = url.Remove(url.Length - 1);
            Console.WriteLine("New URL: " + url);*/



        /// <summary>
        /// 
        /// </summary>
        /// <param name="model">a list of strings as query parameter</param>
        /// <returns>A Game filtered by Genre</returns>
        [HttpGet("GameByGenre")]
        public async Task<ActionResult> GetGameByGenre([FromQuery] List<string> model)
        {
            List<GameDTO> gameClientList = new List<GameDTO>();

            foreach (Game game in _context.Games)
            {
                GameDTO gameClient = new GameDTO()
                {
                    ID = game.gameID,
                    Title = game.Title,
                    Description = game.Description,
                    Price = game.Price,
                    Publisher = game.Publisher,
                    ReleaseYear = game.ReleaseYear,
                    GameGenre = _context.Genres.FirstOrDefault(g => g.GenreID == game.GenreID)?.GenreName,
                    GamePlatform = _context.Platforms.FirstOrDefault(p => p.PlatformID == game.PlatformID)?.Name
                };

                gameClientList.Add(gameClient);
            }

            IQueryable<GameDTO> games = gameClientList.AsQueryable();
            List<GameDTO> gamesByPreference = new List<GameDTO>();

            if (model.Count != 0)
            {
                foreach(var game in gameClientList)
                {
                    if(model.Contains(game.GameGenre))
                    {
                        gamesByPreference.Add(game);
                    }
                }
            }

            return Ok(gamesByPreference);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model">a list of strings as query parameter</param>
        /// <returns>A Game filtered by Platform</returns>
        [HttpGet("GameByPlatform")]
        public async Task<ActionResult> GetGameByPlatform([FromQuery] List<string> model)
        {
            List<GameDTO> gameClientList = new List<GameDTO>();

            foreach (Game game in _context.Games)
            {
                GameDTO gameClient = new GameDTO()
                {
                    ID = game.gameID,
                    Title = game.Title,
                    Description = game.Description,
                    Price = game.Price,
                    Publisher = game.Publisher,
                    ReleaseYear = game.ReleaseYear,
                    GameGenre = _context.Genres.FirstOrDefault(g => g.GenreID == game.GenreID)?.GenreName,
                    GamePlatform = _context.Platforms.FirstOrDefault(p => p.PlatformID == game.PlatformID)?.Name
                };

                gameClientList.Add(gameClient);
            }

            IQueryable<GameDTO> games = gameClientList.AsQueryable();
            List<GameDTO> gamesByPreference = new List<GameDTO>();

            if (model.Count != 0)
            {
                foreach (var game in gameClientList)
                {
                    if (model.Contains(game.GamePlatform))
                    {
                        gamesByPreference.Add(game);
                    }
                }
            }

            return Ok(gamesByPreference);

        }


        /// <summary>
        /// Adds a new game to DB
        /// </summary>
        /// <param name="game">Game object serialized from the client</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> AddGame([FromBody] GameDTO game)
        {
            Game newGame = new()
            {
                gameID = game.ID,
                Title = game.Title,
                Description = game.Description,
                Price = game.Price.Value,
                Publisher = game.Publisher,
                ReleaseYear = game.ReleaseYear.Value,
                GenreID = _context.Genres.FirstOrDefault(g => g.GenreName == game.GameGenre)?.GenreID ?? 0,
                PlatformID = _context.Platforms.FirstOrDefault(p => p.Name == game.GamePlatform)?.PlatformID ?? 0

            };

            _context.Games.Add(newGame);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGameById", new { id = newGame.gameID }, newGame);
        }

        /// <summary>
        /// Updates game content in DB
        /// </summary>
        /// <param name="id">Calls game by Id</param>
        /// <param name="game">Game object is altered and then updated to DB</param>
        /// <returns>An updated game object</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<GameDTO>> UpdateGame([FromRoute]int id, [FromBody]GameDTO game)
        {
            Game updateGame = new()
            {
                gameID = game.ID,
                Title = game.Title,
                Description = game.Description,
                Price = game.Price.Value,
                Publisher = game.Publisher,
                ReleaseYear = game.ReleaseYear.Value,
                GenreID = _context.Genres.FirstOrDefault(g => g.GenreName == game.GameGenre)?.GenreID ?? 0,
                PlatformID = _context.Platforms.FirstOrDefault(p => p.Name == game.GamePlatform)?.PlatformID ?? 0

            };

            if (id != updateGame.gameID)
            {
                return BadRequest();
            }



            _context.Games.Update(updateGame);
            //_context.Entry(game).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }

            catch(DbUpdateConcurrencyException)
            {
                if(!_context.Games.Any(g => g.gameID == id))
                {
                    return NotFound();
                }

                else
                {
                    throw;
                }
            }

            return game;
        }

        /// <summary>
        /// Deletes game from DB
        /// </summary>
        /// <param name="id">search game by Id</param>
        /// <returns>game object is removed and DB is updated.</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Game>> DeleteGame([FromRoute] int id)
        {
            var game = await _context.Games.FindAsync(id);

            if(game == null)
            {
                return NotFound();
            }

            _context.Games.Remove(game);
            await _context.SaveChangesAsync();

            return game;
        }


    }
}
