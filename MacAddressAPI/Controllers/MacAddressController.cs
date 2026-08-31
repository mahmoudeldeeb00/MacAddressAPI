using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.NetworkInformation;

namespace MacAddressAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MacAddressController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetMacAddress()
        {
            var macAddress = NetworkInterface
                .GetAllNetworkInterfaces()
                .Where(x =>
                    x.OperationalStatus == OperationalStatus.Up &&
                    x.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                .Select(x => x.GetPhysicalAddress().ToString())
                .FirstOrDefault(x => !string.IsNullOrEmpty(x));

            return Ok(new
            {
                macAddress
            });
        }
    }
}
