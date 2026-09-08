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
            //var macAddress = NetworkInterface
            //    .GetAllNetworkInterfaces()
            //    .Where(x =>
            //        x.OperationalStatus == OperationalStatus.Up &&
            //        x.NetworkInterfaceType != NetworkInterfaceType.Loopback)
            //    .Select(x => x.GetPhysicalAddress().ToString())
            //    .FirstOrDefault(x => !string.IsNullOrEmpty(x));


            var macAddress = NetworkInterface
   .GetAllNetworkInterfaces()
   .Where(n =>
       n.NetworkInterfaceType != NetworkInterfaceType.Loopback &&
       n.NetworkInterfaceType != NetworkInterfaceType.Tunnel &&
       !n.Description.ToLower().Contains("virtual"))
   .OrderByDescending(n => n.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
   .ThenByDescending(n => n.NetworkInterfaceType == NetworkInterfaceType.Wireless80211)
   .Select(n => n.GetPhysicalAddress().ToString())
   .FirstOrDefault();

            //return mac ?? "";
            return Ok(new
            {
                macAddress
            });
        }
    }
}
