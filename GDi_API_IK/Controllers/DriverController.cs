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

        #region PUTs
        [HttpPut("UpdateDriver")]
        public async Task<ActionResult<LayerResponse>> UpdateDriver(PutDriverRequestDTO driver) {
            var validationResult = ValidateDriverData(driver.Name);

            if (validationResult.validationSuccess) {
                var serviceResponse = await _driverService.UpdateDriverAsync(driver);

                LayerResponse response = new() {
                    Message = serviceResponse.Message,
                    ResponseCode = serviceResponse.ResponseCode,
                    Success = serviceResponse.Success,
                    ExMessage = serviceResponse.ExMessage
                };

                return StatusCode(ResponseCodes.GetHttpCode(response.ResponseCode), response);
            } else {
                return BadRequest(validationResult.message);
            }
        }
        #endregion

        #region DELETEs
        [HttpDelete("DeleteDriver/{id}")]
        public async Task<ActionResult<LayerResponse>> DeleteDriver(int id) {
            var serviceResponse = await _driverService.DeleteDriverAsync(id);
            LayerResponse response = new() {
                Message = serviceResponse.Message,
                ResponseCode = serviceResponse.ResponseCode,
                Success = serviceResponse.Success,
                ExMessage = serviceResponse.ExMessage
            };
            return StatusCode(ResponseCodes.GetHttpCode(response.ResponseCode), response);
        }

        #endregion

        #region POSTs
        [HttpPost("AddDriver")]
        public async Task<ActionResult<LayerResponse>> AddDriver(PostDriverRequestDTO newDriver) {
            var validationResult = ValidateDriverData(newDriver.Name);

            if (validationResult.validationSuccess) {
                var serviceResponse = await _driverService.AddDriverAsync(newDriver);
                LayerResponse response = new() {
                    Message = serviceResponse.Message,
                    ResponseCode = serviceResponse.ResponseCode,
                    Success = serviceResponse.Success,
                    ExMessage = serviceResponse.ExMessage
                };
                return StatusCode(ResponseCodes.GetHttpCode(response.ResponseCode), response);
            } else {
                return BadRequest(validationResult.message);
            }
        }
        #endregion

        private (bool validationSuccess, string message) ValidateDriverData(string name) {
            bool validationSuccess = true;
            string message = "";

            if (name == string.Empty) {
                message += "Name is Required | ";
                validationSuccess = false;
            }
            return (validationSuccess, message);
        }
    }
}
