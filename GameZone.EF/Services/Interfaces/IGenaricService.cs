
namespace GameZone.EF.Services.Interfaces
{
    public interface IGenaricService<T> where T : class
    {
        Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>>? filter = null,
                                     string[]? includeWords = null);
        Task<T> GetOne(Expression<Func<T, bool>>? filter = null, string[]? includeWords = null);
        Task Add(T item);       
        void Update(T item);
        void Delete(T item);
    }
}
