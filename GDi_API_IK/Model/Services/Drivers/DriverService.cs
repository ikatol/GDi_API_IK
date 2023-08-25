using GDi_API_IK.Model.DTOs;
using GDi_API_IK.Model.DTOs.Cars;
using GDi_API_IK.Model.DTOs.Drivers;
using GDi_API_IK.Model.Entities;
using GDi_API_IK.Model.Repositories.Drivers;

namespace GDi_API_IK.Model.Services.Drivers {
    public class DriverService : IDriverService {
        private readonly IDriverRespository _driverRepository;

        public DriverService(IDriverRespository driverRepository) {
            _driverRepository = driverRepository;
        }

        public async Task<LayerResponse> AddDriverAsync(PostDriverRequestDTO newDriver) {
            var driverToAdd = new Driver() {
                Name = newDriver.Name
            };

            var repositoryResponse = await _driverRepository.AddAsync(driverToAdd);

            return repositoryResponse;
        }

        public async Task<LayerResponse> DeleteDriverAsync(int id) {
            var repositoryResponse = await _driverRepository.DeleteAsync(id);

            return repositoryResponse;
        }

        public async Task<LayerResponse<List<GetDriverResponseDTO>>> GetAllDriversAsync() {
            var response = new LayerResponse<List<GetDriverResponseDTO>>();
            var repositoryResponse = await _driverRepository.GetAllAsync();

            if (repositoryResponse.Payload is not null && repositoryResponse.Success) {
                response.Payload = repositoryResponse.Payload.Select(d => new GetDriverResponseDTO(d)).ToList();
            }

            response.Message = repositoryResponse.Message;
            response.ExMessage = repositoryResponse.ExMessage;
            response.Success = repositoryResponse.Success;
            response.ResponseCode = repositoryResponse.ResponseCode;

            return response;
        }

        public async Task<LayerResponse> UpdateDriverAsync(PutDriverRequestDTO driver) {
            var repositoryResponse = await _driverRepository.UpdateAsync(new Driver() {
                Id = driver.Id,
                Name = driver.Name
            });

            return repositoryResponse;
        }
    }
}
