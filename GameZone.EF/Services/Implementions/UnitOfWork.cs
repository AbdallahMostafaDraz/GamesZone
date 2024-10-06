

namespace GameZone.EF.Services.Implementions
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDBContext _context;
        public ICategoryService Category { get; private set; }

        public IGameService Game {  get; private set; }

        public IDeviceService Device { get; private set; }

        public UnitOfWork(AppDBContext context)
        {
            _context = context;
            Category = new CategoryService(context);
            Game = new GameService(context);
            Device = new DeviceService(context);
        }
        public async  Task<int> Complete()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

    }
}
