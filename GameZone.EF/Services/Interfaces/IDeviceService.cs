
namespace GameZone.EF.Services.Interfaces
{
    public interface IDeviceService : IGenaricService<Device>
    {
        Task<IEnumerable<SelectListItem>> GetDevicesSelectedList();
    }
}
