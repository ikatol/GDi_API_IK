using Azure;
using GDi_API_IK.Model.DContext;
using GDi_API_IK.Model.DTOs.Cars;
using GDi_API_IK.Model.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace GDi_API_IK.Model.Repositories {
    public class CarRepository : ICarRepository {
        private readonly DataContext _dataContext;

        public CarRepository(DataContext dataContext) {
            _dataContext = dataContext;
        }
        public async Task<LayerResponse<List<Car>>> GetAllAsync(bool includeDrivers = false) {
            var response = new LayerResponse<List<Car>>();

            try {
                if (includeDrivers) {
                    response.Payload = await _dataContext.Cars.Include(d => d.Driver).ToListAsync();
                } else {
                    response.Payload = await _dataContext.Cars.ToListAsync();
                }
                response.Success = true;
            } catch (Exception ex) {
                response.ResponseCode = ResponseCodes.Code.INTERNAL_ERROR;
                response.ExMessage = ex.Message;
                response.Success = false;
            }
            return response;
        }

        public async Task<LayerResponse> AddAsync(Car car) {
            var response = new LayerResponse();

            var validationResult = await ValidateCarData(car);
            response.Success = validationResult.validationSuccess;
            response.Message = validationResult.message;

            if (response.Success) {
                try {
                    _dataContext.Add(car);
                    await _dataContext.SaveChangesAsync();

                    if (_dataContext.Entry(car).State == EntityState.Unchanged) {
                        response.Message = "Car has been added!";
                    } else {
                        response.ResponseCode = ResponseCodes.Code.INTERNAL_ERROR;
                        response.Success = false;
                        response.Message = "Car has not been added!";
                    }
                } catch (Exception ex) {
                    response.ResponseCode = ResponseCodes.Code.INTERNAL_ERROR;
                    response.ExMessage = ex.Message;
                    response.Success = false;
                }
            } else {
                response.ResponseCode= ResponseCodes.Code.INTERNAL_ERROR;
            }
            return response;
        }

        public async Task<LayerResponse> DeleteAsync(int id) {
            var getResponse = await GetCarAsync(id, false);
            var response = new LayerResponse();

            if (getResponse.Success) {
                if (getResponse.Payload is not null) {
                    try {
                        _dataContext.Remove(getResponse.Payload);
                        await _dataContext.SaveChangesAsync();
                        if (_dataContext.Entry(getResponse.Payload).State == EntityState.Detached) {
                            response.Message = "Car has been deleted";
                        } else {
                            response.ResponseCode = ResponseCodes.Code.INTERNAL_ERROR;
                            response.Success = false;
                            response.Message = "Car was NOT deleted";
                        }
                    } catch (Exception ex) {
                        response.ResponseCode = ResponseCodes.Code.INTERNAL_ERROR;
                        response.ExMessage = ex.Message;
                        response.Success = false;
                    }
                } else {
                    response.ResponseCode = ResponseCodes.Code.NOT_FOUND;
                    response.Message = "Car not found";
                    response.Success = false;
                };

            } else {
                response.ResponseCode = getResponse.ResponseCode;
                response.Message = getResponse.Message;
                response.ExMessage = getResponse.ExMessage;
                response.Success = false;
            }
            return response;
        }

        public async Task<LayerResponse<Car>> GetCarAsync(int id, bool includeDriver = false) {
            var response = new LayerResponse<Car>();

            try {
                if (includeDriver) {
                    response.Payload = await _dataContext.Cars.Include(d => d.Driver).FirstOrDefaultAsync(c => c.Id == id);
                } else {
                    response.Payload = await _dataContext.Cars.FirstOrDefaultAsync(c => c.Id == id);
                }

                if (response.Payload is null) {
                    response.Success = false;
                    response.Message = "Car not found";
                    response.ResponseCode = ResponseCodes.Code.NOT_FOUND;
                }
            } catch (Exception ex) {
                response.ResponseCode = ResponseCodes.Code.INTERNAL_ERROR;
                response.ExMessage = ex.Message;
                response.Success = false;
            }

            return response;
        }

        public async Task<LayerResponse> UpdateAsync(Car car) {
            var response = new LayerResponse();
            var validationResult = await ValidateCarData(car);

            response.Success = validationResult.validationSuccess;
            response.Message = validationResult.message;

            if (response.Success) {
                try {
                    var getCarResponse = await GetCarAsync(car.Id);
                    response.Message = getCarResponse.Message;
                    response.Success = getCarResponse.Success;
                    response.ExMessage = getCarResponse.ExMessage;
                    response.ResponseCode = getCarResponse.ResponseCode;

                    if (getCarResponse.Success && getCarResponse.Payload is not null) {
                        getCarResponse.Payload.Model = car.Model;
                        getCarResponse.Payload.Registration = car.Registration;
                        getCarResponse.Payload.LoadCapacityKg = car.LoadCapacityKg;
                        getCarResponse.Payload.ProductionYear = car.ProductionYear;
                        getCarResponse.Payload.Longitude = car.Longitude;
                        getCarResponse.Payload.Latitude = car.Latitude;

                        _dataContext.Update(getCarResponse.Payload);
                        await _dataContext.SaveChangesAsync();

                        if (_dataContext.Entry(car).State == EntityState.Unchanged) {
                            response.ResponseCode = ResponseCodes.Code.INTERNAL_ERROR;
                            response.Success = false;
                            response.Message = "Car was not updated";
                        } else {
                            response.Message = "Car was updated";
                        }
                    } 
                } catch(Exception ex) {
                    response.ResponseCode = ResponseCodes.Code.INTERNAL_ERROR;
                    response.ExMessage = ex.Message;
                    response.Success = false;
                }
            } else {
                response.ResponseCode = ResponseCodes.Code.INTERNAL_ERROR;
            }
            return response;
        }

        private async Task<(bool validationSuccess, string message)> ValidateCarData(Car car) {
            bool validationSuccess = true;
            string message = "";

            if (car.Model == string.Empty) {
                message += "Car model is required | ";
                validationSuccess = false;
            }
            if (car.Registration == string.Empty) {
                message += "Car registration is required | ";
                validationSuccess = false;
            }
            if (car.Registration == string.Empty) {
                message += "Car registration is required | ";
                validationSuccess = false;
            }
            if ((await GetAllAsync()).Payload?.FirstOrDefault(c => c.Registration == car.Registration && c.Id != car.Id) is not null) {
                message += "Car registration is already taken | ";
                validationSuccess = false;
            }
            if (car.ProductionYear < 1885 || car.ProductionYear > DateTime.Now.Year) {
                message += $"Production year must be between 1885 and {DateTime.Now.Year} | ";
                validationSuccess = false;
            }

            return (validationSuccess, message);
        }

        public async Task<LayerResponse<Car>> GetByRegistrationAsync(string registration) {
            var response = new LayerResponse<Car>();

            try {
                response.Payload = await _dataContext.Cars.FirstOrDefaultAsync(c => c.Registration == registration);
                if (response.Payload is null) {
                    response.Success = false;
                    response.Message = "Car not found";
                    response.ResponseCode = ResponseCodes.Code.NOT_FOUND;
                }
            } catch (Exception ex) {
                response.ResponseCode = ResponseCodes.Code.INTERNAL_ERROR;
                response.ExMessage = ex.Message;
                response.Success = false;
            }
            return response;
        }

        public async Task<LayerResponse> AssignDriver(int CarId, int? DriverId) {
            var carGetResponse = await GetCarAsync(CarId);
            var response = new LayerResponse() {
                ResponseCode = carGetResponse.ResponseCode,
                Message = carGetResponse.Message,
                ExMessage = carGetResponse.ExMessage,
                Success = carGetResponse.Success
            };

            if (carGetResponse.Payload is not null) {
                if (carGetResponse.Payload.DriverId == DriverId) {
                    return new LayerResponse() {
                        Message = "This car is already assigned to this car"
                    };
                }

                try {
                    carGetResponse.Payload.DriverId = DriverId;
                    _dataContext.Update(carGetResponse.Payload);
                    await _dataContext.SaveChangesAsync();

                } catch(Exception ex) {
                    var innerException = ex.InnerException as SqlException;
                    if (innerException is not null && innerException.Number == 547) {
                        response.ResponseCode = ResponseCodes.Code.NOT_FOUND;
                        response.ExMessage = ex.Message;
                        response.Success = false;
                        response.Message = "This driver does not exist";
                    } else {
                        response.ResponseCode = ResponseCodes.Code.INTERNAL_ERROR;
                        response.ExMessage = ex.Message;
                        response.Success = false;
                    }
                }
             
            }
            return response;
        }
    }
}
