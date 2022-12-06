using PoriectSD.Models;

namespace PoriectSD.Services
{
    public interface IEnergyServices
    {
        Task<List<Energy>> GetEnergyForDeviceId(int id, DateTime date);
    }
}