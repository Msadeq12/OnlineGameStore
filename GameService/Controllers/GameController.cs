using GameService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            return Ok(await _context.Games.ToListAsync());
        }

        /// <summary>
        /// Gets all genre from DB for the client select option
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Genres")]
        public async Task<ActionResult> GetAllGenres()
        {
            return Ok( await _context.Genres.ToListAsync());
        }

        /// <summary>
        /// Gets specific game by Id from DB
        /// </summary>
        /// <param name="id">refers to gameID</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult> GetGame(int id)
        {
            var game = await _context.Games.FindAsync(id);

            if (game == null)
            {
                return NotFound();
            }

            return Ok(game);
        }

        /// <summary>
        /// Adds a new game to DB
        /// </summary>
        /// <param name="game">Game object serialized from the client</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> AddGame(Game game)
        {
            _context.Games.Add(game);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGame", new { id = game.gameID }, game);
        }

        /// <summary>
        /// Updates game content in DB
        /// </summary>
        /// <param name="id">Calls game by Id</param>
        /// <param name="game">Game object is altered and then updated to DB</param>
        /// <returns>An updated game object</returns>
        [HttpPut("id")]
        public async Task<ActionResult> UpdateGame(int id, Game game)
        {
            if(id != game.gameID)
            {
                return BadRequest();
            }

            _context.Entry(game).State = EntityState.Modified;

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

            return NoContent();
        }
        /// <summary>
        /// Deletes game from DB
        /// </summary>
        /// <param name="id">search game by Id</param>
        /// <returns>game object is removed and DB is updated.</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Game>> DeleteGame(int id)
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
