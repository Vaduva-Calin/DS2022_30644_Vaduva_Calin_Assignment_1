using PoriectSD.Database;
using PoriectSD.Models;

namespace PoriectSD.Services
{
    public class EnergyServices : IEnergyServices
    {
        SDDbContext _dbContext;
        public EnergyServices(SDDbContext SDDbContext)
        {
            _dbContext = SDDbContext;
        }

        public async Task<List<Energy>> GetEnergyForDeviceId(int id, DateTime date)
        {
            return _dbContext.Energies.Where(e => (e.DeviceId == id) && (e.Timestamp.Date == date.Date)).ToList();
        }
    }
}
