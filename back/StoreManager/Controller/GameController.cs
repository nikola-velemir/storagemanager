using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using StoreManager.DTO;
using StoreManager.Service;

namespace StoreManager.Controller
{
    [ApiController]
    [Route("games")]
    public class GameController : ControllerBase
    {
        private GameService _gameService;

        public GameController()
        {
            _gameService = new GameService();
        }

        [HttpGet]
        [Route("")]
        public ActionResult<List<GameDTO>> getGames()
        {
            return Ok(_gameService.getAll());
        }
        [HttpGet]
        [Route("{id}")]
        public ActionResult<GameDTO> getGame(int id)
        {
            return Ok(_gameService.getById(id));
        }
    }
}
