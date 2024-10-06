
namespace GameZone.EF.Services.Implementions
{
    public class GameService : GenaricService<Game>, IGameService
    {
        private readonly DbContext _context;
        private readonly DbSet<Game> _games;    
        public GameService(AppDBContext context) : base(context) 
        {
            _context = context;
            _games = context.Set<Game>();   
        }

        public IEnumerable<Game> GetAllWithAllIncludes()
        {
            var games =  _games.
                Include(e => e.Category).
                Include(e => e.Devices).ThenInclude(e => e.Device).ToList();
            return games;
        }


        public Game? GetOneWithAllIncludes(int id)
        {
            var game = _games
                .Include(e => e.Category)
                .Include(e => e.Devices)
                .ThenInclude(e => e.Device)
                .SingleOrDefault(e => e.Id == id);
            return game;
        }
    }
}
