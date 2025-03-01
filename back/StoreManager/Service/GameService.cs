using StoreManager.DTO;

namespace StoreManager.Service
{
    public class GameService
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
        public List<GameDTO> getAll()
        {
            return games;
        }

        public GameDTO? getById(int id)
        {
            return games.FirstOrDefault(game => game.Id == id);
        }

    }
}
