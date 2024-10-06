
namespace GameZone.EF.Services.Interfaces
{
    public interface IGameService : IGenaricService<Game> 
    {
        IEnumerable<Game> GetAllWithAllIncludes();

        Game? GetOneWithAllIncludes(int id);
    }
}
