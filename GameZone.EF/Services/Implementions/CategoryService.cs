

namespace GameZone.EF.Services.Implementions
{
    public class CategoryService : GenaricService<Category>, ICategoryService
    {
        public CategoryService(AppDBContext context) : base(context) { }

        public async Task<IEnumerable<SelectListItem>> GetCategorySelectedList()
        {
            var categories = await GetAll(null, null);
            var selecteList = categories.
               Select(e => new SelectListItem { Value = e.Id.ToString(), Text = e.Name })
               .OrderBy(e => e.Text).ToList();
            return selecteList;
        }

    }
}
