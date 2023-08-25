using GDi_API_IK.Model.DContext;
using GDi_API_IK.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace GDi_API_IK.Model.Repositories.Drivers {
    public class DriverRepository : IDriverRespository {
        private readonly DataContext _dataContext;

        public DriverRepository(DataContext dataContext) {
            _dataContext = dataContext;
        }

        public async Task<LayerResponse> AddAsync(Driver driver) {
            var response = new LayerResponse();

            var validationResult = ValidateDriverData(driver);
            response.Success = validationResult.validationSuccess;
            response.Message = validationResult.message;

            if (response.Success) {
                try {
                    _dataContext.Add(driver);
                    await _dataContext.SaveChangesAsync();

                    if (_dataContext.Entry(driver).State == EntityState.Unchanged) {
                        response.Message = "Driver has been added!";
                    } else {
                        response.ResponseCode = ResponseCodes.Code.INTERNAL_ERROR;
                        response.Success = false;
                        response.Message = "Driver has not been added!";
                    }
                } catch (Exception ex) {
                    response.ResponseCode = ResponseCodes.Code.INTERNAL_ERROR;
                    response.ExMessage = ex.Message;
                    response.Success = false;
                }
            } else {
                response.ResponseCode = ResponseCodes.Code.INTERNAL_ERROR;
            }
            return response;
        }

        public async Task<LayerResponse> DeleteAsync(int id) {
            var getResponse = await GetDriverAsync(id);
            var response = new LayerResponse();

            if (getResponse.Success) {
                if (getResponse.Payload is not null) {
                    try {
                        _dataContext.Remove(getResponse.Payload);
                        await _dataContext.SaveChangesAsync();
                        if (_dataContext.Entry(getResponse.Payload).State == EntityState.Detached) {
                            response.Message = "Driver has been deleted";
                        } else {
                            response.ResponseCode = ResponseCodes.Code.INTERNAL_ERROR;
                            response.Success = false;
                            response.Message = "Driver was NOT deleted";
                        }
                    } catch (Exception ex) {
                        response.ResponseCode = ResponseCodes.Code.INTERNAL_ERROR;
                        response.ExMessage = ex.Message;
                        response.Success = false;
                    }
                } else {
                    if (getResponse.ResponseCode == ResponseCodes.Code.NOT_FOUND) {
                        response.Message = "Driver not found";
                    } else {
                        response.Message = getResponse.Message;
                    }
                    response.ResponseCode = getResponse.ResponseCode;
                    response.ExMessage = getResponse.ExMessage;
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

        public async Task<LayerResponse<List<Driver>>> GetAllAsync() {
            var response = new LayerResponse<List<Driver>>();

            try {
                response.Payload = await _dataContext.Drivers.ToListAsync();
                response.Success = true;
            } catch (Exception ex) {
                response.ResponseCode = ResponseCodes.Code.INTERNAL_ERROR;
                response.ExMessage = ex.Message;
                response.Success = false;
            }
            return response;
        }

        public async Task<LayerResponse<Driver>> GetDriverAsync(int id) {
            var response = new LayerResponse<Driver>();
            try {
                response.Payload = await _dataContext.Drivers.FirstOrDefaultAsync(d => d.Id == id);
                if (response.Payload is null) {
                    response.ResponseCode = ResponseCodes.Code.NOT_FOUND;
                }
            } catch (Exception ex) {
                response.ResponseCode = ResponseCodes.Code.INTERNAL_ERROR;
                response.ExMessage = ex.Message;
                response.Success = false;
            }
            return response;
        }

        public async Task<LayerResponse> UpdateAsync(Driver driver) {
            var response = new LayerResponse();
            var validationResult = ValidateDriverData(driver);

            response.Success = validationResult.validationSuccess;
            response.Message = validationResult.message;

            if (response.Success) {
                try {
                    var getDriverResponse = await GetDriverAsync(driver.Id);
                    response.Message = getDriverResponse.Message;
                    response.Success = getDriverResponse.Success;
                    response.ExMessage = getDriverResponse.ExMessage;
                    response.ResponseCode = getDriverResponse.ResponseCode;

                    if (getDriverResponse.ResponseCode == ResponseCodes.Code.NOT_FOUND) {
                        response.Success = false;
                        response.Message = "Driver that was trying to be updated not found in the database";
                    } else if (getDriverResponse.Success == true && getDriverResponse.Payload is not null) {
                        getDriverResponse.Payload.Name = driver.Name;
                        _dataContext.Update(getDriverResponse.Payload);
                        await _dataContext.SaveChangesAsync();

                        if (_dataContext.Entry(driver).State == EntityState.Unchanged) {
                            response.ResponseCode = ResponseCodes.Code.INTERNAL_ERROR;
                            response.Success = false;
                            response.Message = "Driver was not updated";
                        } else {
                            response.Message = "Driver was updated";
                        }
                    }      
                } catch (Exception ex) {
                    response.ResponseCode = ResponseCodes.Code.INTERNAL_ERROR;
                    response.ExMessage = ex.Message;
                    response.Success = false;
                }
            } else {
                response.ResponseCode = ResponseCodes.Code.INTERNAL_ERROR;
            }
            return response;
        }

        private (bool validationSuccess, string message) ValidateDriverData(Driver driver) {
            bool validationSuccess = true;
            string message = "";

            if (driver.Name == string.Empty) {
                message += "Driver name is required | ";
                validationSuccess = false;
            }
            return (validationSuccess, message);
        }
    }
}
