
namespace GameZone.EF.Services.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryService Category {  get; }
        IGameService Game { get; }
        IDeviceService Device { get; }

        Task<int> Complete();
    }
}
