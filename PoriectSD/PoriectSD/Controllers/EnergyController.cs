using Microsoft.AspNetCore.Mvc;
using PoriectSD.Models;
using PoriectSD.Services;


namespace PoriectSD.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnergyController : ControllerBase
    {
        IEnergyServices _energyServices;

        public EnergyController(IEnergyServices energyServices)
        {
            _energyServices = energyServices;
        }

        [HttpGet("get-for-deviceId")]
        public async Task<List<Energy>> GetEnergyForDeviceId(int id, DateTime date)
        {
            return await _energyServices.GetEnergyForDeviceId(id, date);

        }

        
    }
}

