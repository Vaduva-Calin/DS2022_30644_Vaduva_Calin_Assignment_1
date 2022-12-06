using Microsoft.AspNetCore.Mvc;
using PoriectSD.Models;
using PoriectSD.Services;

namespace PoriectSD.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeviceController : ControllerBase
    {
        IDeviceServices _deviceService;

        public DeviceController(IDeviceServices deviceServices)
        {
            _deviceService = deviceServices;
        }

        [HttpGet("get")]
        public async Task<IEnumerable<Device>> GetDevices()
        {
            return await _deviceService.GetDevices();
        }

        [HttpGet("user-devices")]
        public async Task<IEnumerable<Device>> GetUserDevices(int id)
        {
            return await _deviceService.GetUserDevices(id);
        }

        [HttpPost("add")]
        public async Task AddDevice(Device device)
        {
            await _deviceService.AddDevice(device);
        }

        [HttpPut("edit")]
        public async Task EditDevice(Device device)
        {
            await _deviceService.EditDevice(device);
        }

        [HttpDelete("remove")]
        public async Task RemoveDevice(int id)
        {
            await _deviceService.RemoveDevice(id);
        }
    }
}
