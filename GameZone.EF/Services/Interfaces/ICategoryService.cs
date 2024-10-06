namespace GameZone.EF.Services.Interfaces
{
    public interface ICategoryService : IGenaricService<Category>
    {
        Task<IEnumerable<SelectListItem>> GetCategorySelectedList();

    }
}
