using Azure;
using GDi_API_IK.Model;
using GDi_API_IK.Model.DTOs.Cars;
using GDi_API_IK.Model.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;

namespace GDi_API_IK.Controllers
{
    [ApiController]
    [Route("API/[controller]")]
    public class CarController : Controller {
        private readonly ICarService _carService;

        public CarController(ICarService carService) {
            _carService = carService;
        }
        //public IActionResult Index() {
        //    return View();
        //}

        #region GETs
        [HttpGet("GetFullCars")]
        public async Task<ActionResult<LayerResponse<List<GetFullCarResponseDTO>>>> GetFullCars() {
            var serviceResponse = await _carService.GetAllCarsAsync(includeDrivers: true, includeCoordinates: true);
            LayerResponse<List<GetFullCarResponseDTO?>> response = new() {
                Message = serviceResponse.Message,
                ResponseCode = serviceResponse.ResponseCode,
                Success = serviceResponse.Success,
                ExMessage = serviceResponse.ExMessage
            };

            if (serviceResponse.Success) {
                if(serviceResponse.Payload is not null) {
                    response.Payload = serviceResponse.Payload.Select(c => c as GetFullCarResponseDTO).ToList();
                }
            }

            return StatusCode(ResponseCodes.GetHttpCode(response.ResponseCode), response);
        }

        [HttpGet("GetCars")]
        public async Task<ActionResult<LayerResponse<List<GetCarResponseDTO>>>> GetCars() {
            var serviceResponse = await _carService.GetAllCarsAsync(includeDrivers: false, includeCoordinates: false);
            LayerResponse<List<GetCarResponseDTO>> response = new() {
                Message = serviceResponse.Message,
                ResponseCode = serviceResponse.ResponseCode,
                Success = serviceResponse.Success,
                ExMessage = serviceResponse.ExMessage
            };

            if (serviceResponse.Success) {
                if (serviceResponse.Payload is not null) {
                    response.Payload = serviceResponse.Payload;
                }
            }

            return StatusCode(ResponseCodes.GetHttpCode(response.ResponseCode), response);
        }

        [HttpGet("GetCarsWithCoordinates")]
        public async Task<ActionResult<LayerResponse<List<GetCarWithCoordinatesResponseDTO>>>> GetCarsWithCoordinates() {
            var serviceResponse = await _carService.GetAllCarsAsync(includeDrivers: false, includeCoordinates: true);
            LayerResponse<List<GetCarWithCoordinatesResponseDTO?>> response = new() {
                Message = serviceResponse.Message,
                ResponseCode = serviceResponse.ResponseCode,
                Success = serviceResponse.Success,
                ExMessage = serviceResponse.ExMessage
            };

            if (serviceResponse.Success) {
                if (serviceResponse.Payload is not null) {
                    response.Payload = serviceResponse.Payload.Select(c => c as GetCarWithCoordinatesResponseDTO).ToList();
                }
            }

            return StatusCode(ResponseCodes.GetHttpCode(response.ResponseCode), response);
        }

        [HttpGet("GetCarsWithDrivers")]
        public async Task<ActionResult<LayerResponse<List<GetCarWithDriverResponseDTO>>>> GetCarsWithDrivers() {
            var serviceResponse = await _carService.GetAllCarsAsync(includeDrivers: true, includeCoordinates: false);
            LayerResponse<List<GetCarWithDriverResponseDTO?>> response = new() {
                Message = serviceResponse.Message,
                ResponseCode = serviceResponse.ResponseCode,
                Success = serviceResponse.Success,
                ExMessage = serviceResponse.ExMessage
            };

            if (serviceResponse.Success) {
                if (serviceResponse.Payload is not null) {
                    response.Payload = serviceResponse.Payload.Select(c => c as GetCarWithDriverResponseDTO).ToList();
                }
            }

            return StatusCode(ResponseCodes.GetHttpCode(response.ResponseCode), response);
        }
        #endregion

        #region POSTs
        [HttpPost("AddCar")]
        public async Task<ActionResult<LayerResponse>> AddCar(PostCarRequestDTO newCar) {
            var validationResult = await ValidateCarData(newCar.Model, newCar.Registration, newCar.ProductionYear);


            if (validationResult.validationSuccess) {
                var serviceResponse = await _carService.AddCarAsync(newCar);
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
        [HttpDelete("DeleteCar/{id}")]
        public async Task<ActionResult<LayerResponse>> DeleteCar(int id) {
            var serviceResponse = await _carService.DeleteCarAsync(id);
            LayerResponse response = new() {
                Message = serviceResponse.Message,
                ResponseCode = serviceResponse.ResponseCode,
                Success = serviceResponse.Success,
                ExMessage = serviceResponse.ExMessage
            };
            return StatusCode(ResponseCodes.GetHttpCode(response.ResponseCode), response);
        }
        #endregion

        #region PUTs
        [HttpPut("UpdateCar")]
        public async Task<ActionResult<LayerResponse>> UpdateCar(PutCarRequestDTO updatedCar) {
            var validationResult = await ValidateCarData(updatedCar.Model, updatedCar.Registration, updatedCar.ProductionYear, updatedCar.Id);
            
            if (validationResult.validationSuccess) {
                var serviceResponse = await _carService.UpdateCarAsync(updatedCar);
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

        private async Task<(bool validationSuccess, string message)> ValidateCarData(string model, string registration, int productionYear, int id = -1) {
            bool validationSuccess = true;
            string message = "";

            if (registration == string.Empty) {
                message += "Registration is Required | ";
                validationSuccess = false;
            }
            if ((await _carService.GetAllCarsAsync(false, false)).Payload?.FirstOrDefault(c => c.Registration == registration && c.Id != id) is not null) {
                message += "Registration is already taken | ";
                validationSuccess = false;
            }
            if (model == string.Empty) {
                message += "Car model is required | ";
                validationSuccess = false;
            }
            if (productionYear < 1885 || productionYear > DateTime.Now.Year) {
                message += $"Production year must be between 1885 and {DateTime.Now.Year} | ";
                validationSuccess = false;
            }

            return (validationSuccess, message);
        }
    }
}
