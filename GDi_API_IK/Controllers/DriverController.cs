using GDi_API_IK.Model;
using GDi_API_IK.Model.DTOs.Cars;
using GDi_API_IK.Model.DTOs.Drivers;
using GDi_API_IK.Model.Services.Drivers;
using Microsoft.AspNetCore.Mvc;

namespace GDi_API_IK.Controllers {
    [ApiController]
    [Route("API/[controller]")]
    public class DriverController : Controller {
        //public IActionResult Index() {
        //    return View();
        //}

        private readonly IDriverService _driverService;

        public DriverController(IDriverService driverService) {
            _driverService = driverService;
        }

        #region GETs
        [HttpGet("GetDrivers")]
        public async Task<ActionResult<LayerResponse<List<GetDriverResponseDTO>>>> GetDrivers() {
            var serviceResponse = await _driverService.GetAllDriversAsync();
            LayerResponse<List<GetDriverResponseDTO?>> response = new() {
                Message = serviceResponse.Message,
                ResponseCode = serviceResponse.ResponseCode,
                Success = serviceResponse.Success,
                ExMessage = serviceResponse.ExMessage
            };

            if (serviceResponse.Success) {
                if (serviceResponse.Payload is not null) {
                    response.Payload = serviceResponse.Payload.Select(c => c as GetDriverResponseDTO).ToList();
                }
            }

            return StatusCode(ResponseCodes.GetHttpCode(response.ResponseCode), response);
        }
        #endregion
    }
}
