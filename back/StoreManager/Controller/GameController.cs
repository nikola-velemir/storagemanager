using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using StoreManager.DTO;

namespace StoreManager.Controller
{
    [ApiController]
    [Route("games")]
    public class GameController : ControllerBase
    {
        List<GameDTO> games = [
   new (
        1,
        "Street",
        "Fighting",
        19.99M,
        new DateOnly(1992,1,2)),
    new (
        2,
        "KITA",
        "BANANA",
        19.99M,
        new DateOnly(1992,1,2)),
    new (
        3,
        "dsadasdas",
        "BAdsadsadasdasNANA",
        19.99M,
        new DateOnly(1992,1,2)),

    ];


        [HttpGet]
        [Route("")]
        public ActionResult<List<GameDTO>> getGames()
        {
            return Ok(games);
        }
        [HttpGet]
        [Route("{id}")]
        public ActionResult<GameDTO> getGame(int id)
        {
            return Ok(games.FirstOrDefault(game => { return game.Id == id; }));
        }
    }
}
