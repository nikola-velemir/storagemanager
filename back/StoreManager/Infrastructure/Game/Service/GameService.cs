using StoreManager.Infrastructure.Game.DTO;

namespace StoreManager.Infrastructure.Game.Service
{
    public class GameService
    {
        List<GameResponseDTO> games = [
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
        public List<GameResponseDTO> getAll()
        {
            return games;
        }

        public GameResponseDTO? getById(int id)
        {
            return games.FirstOrDefault(game => game.Id == id);
        }

    }
}
