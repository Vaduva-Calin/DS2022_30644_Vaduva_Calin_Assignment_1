using PoriectSD.Models;

namespace PoriectSD.Services
{
    public interface IDeviceServices
    {
        Task AddDevice(Device device);
        Task<IEnumerable<Device>> GetUserDevices(int id);
        Task EditDevice(Device device);
        Task<IEnumerable<Device>> GetDevices();
        Task RemoveDevice(int id);

    }
}