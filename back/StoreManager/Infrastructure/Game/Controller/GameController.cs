using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreManager.Infrastructure.Game.DTO;
using StoreManager.Infrastructure.Game.Service;

namespace StoreManager.Infrastructure.Game.Controller
{
    [Authorize(Roles ="ADMIN")]
    [Route("api/games")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private GameService _gameService;

        public GameController()
        {
            _gameService = new GameService();
        }

        [HttpGet]
        [Route("")]
        public ActionResult<List<GameResponseDTO>> getGames()
        {
            return Ok(_gameService.getAll());
        }
        [HttpGet]
        [Route("{id}")]
        public ActionResult<GameResponseDTO> getGame(int id)
        {
            return Ok(_gameService.getById(id));
        }
    }
}
