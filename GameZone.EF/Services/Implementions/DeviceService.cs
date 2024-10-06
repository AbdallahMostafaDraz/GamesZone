
namespace GameZone.EF.Services.Implementions
{
    public class DeviceService : GenaricService<Device>, IDeviceService
    {
        public DeviceService(AppDBContext context) : base(context) { }
        public async Task<IEnumerable<SelectListItem>> GetDevicesSelectedList()
        {
            var devices = await GetAll(null, null);
            var selectList = devices.Select(e => new SelectListItem { Value = e.Id.ToString(), Text = e.Name }).
                OrderBy(e => e.Text).ToList();
            return selectList;
        }
    }
}
