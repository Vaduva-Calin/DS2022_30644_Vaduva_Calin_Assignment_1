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
            var allEnergy = _dbContext.Energies.Where(e => (e.DeviceId == id) && (e.Timestamp.Date == date.Date)).ToList();
            List<Energy> dailyEnergy = new List<Energy>();
            for(int i = 0; i<24; i++)
            {
                var energy = allEnergy.Where(a => a.Timestamp.Hour == i).ToList();

                Energy finalEnergy = new Energy() {};
                foreach (var j in energy)
                {
                    finalEnergy.DeviceId = j.DeviceId;
                    finalEnergy.Timestamp = j.Timestamp;
                    finalEnergy.EnergyConsumption += j.EnergyConsumption;
                }
                dailyEnergy.Add(finalEnergy);
                
            }
            return dailyEnergy;
        }
    }
}
