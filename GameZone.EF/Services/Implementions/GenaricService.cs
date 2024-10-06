
namespace GameZone.EF.Services.Implementions
{
    public class GenaricService<T> : IGenaricService<T> where T : class
    {
        private readonly AppDBContext _context;
        private DbSet<T> _dbSet;
        public GenaricService(AppDBContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();  
        }


        public async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>>? filter, string[]? includeWords)
        {

            IQueryable<T> query = _dbSet.AsNoTracking();

            if (filter != null)
                query = query.Where(filter);

            if (includeWords != null)
                foreach (var word in includeWords)
                    query = query.Include(word);
            
            return await query.ToListAsync();
        }
        public async Task<T> GetOne(Expression<Func<T, bool>>? filter, string[]? includeWords)
        {
            IQueryable<T> query = _dbSet;

            if (filter != null)
                query = query.Where(filter);

            if (includeWords != null)
                foreach (var word in includeWords)
                    query = query.Include(word);

            return await query.FirstOrDefaultAsync();
        }

        public async Task Add(T item)
        {
            await _dbSet.AddAsync(item);
        }

        public void Update(T item)
        {
            _dbSet.Update(item);
        }

        public void Delete(T item)
        {
            _dbSet.Remove(item);
        }


        
    }
}
