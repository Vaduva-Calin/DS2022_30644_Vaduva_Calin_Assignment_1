using PoriectSD.Database;
using PoriectSD.Models;

namespace PoriectSD.Services
{
    public class DeviceServices : IDeviceServices
    {
        SDDbContext _dbContext;

        public DeviceServices(SDDbContext SDDbContext)
        {
            _dbContext = SDDbContext;
        }

        public async Task<IEnumerable<Device>> GetDevices()
        {
            return _dbContext.Devices.ToList();
        }

        public async Task<IEnumerable<Device>> GetUserDevices(int id)
        {
            return _dbContext.Devices.Where(d => d.UserId == id).ToList();
        }

        public async Task AddDevice(Device device)
        {
            try
            {
                _dbContext.Devices.Add(device);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task EditDevice(Device device)
        {
            try
            {
                _dbContext.Devices.Update(device);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task RemoveDevice(int id)
        {
            try
            {
                Device device = new Device() { Id = id};
                _dbContext.Devices.Attach(device);
                _dbContext.Devices.Remove(device);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
