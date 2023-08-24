using GDi_API_IK.Model.DTOs.Cars;
using GDi_API_IK.Model.Entities;
using GDi_API_IK.Model.Repositories;
using System.Collections.Generic;
using System.Diagnostics;

namespace GDi_API_IK.Model.Services
{
    public class CarService : ICarService {
        private readonly ICarRepository _carRepository;

        public CarService(ICarRepository carRepository) {
            _carRepository = carRepository;
        }

        public async Task<LayerResponse> AddCarAsync(PostCarRequestDTO newCar) {
            var carToAdd = new Car() {
                Model = newCar.Model,
                Registration = newCar.Registration,
                ProductionYear = newCar.ProductionYear,
                LoadCapacityKg = newCar.LoadCapacityKg
            };

            var repositoryResponse = await _carRepository.AddAsync(carToAdd);

            return repositoryResponse;
        }

        public async Task<LayerResponse> DeleteCarAsync(int id) {
            var repositoryResponse = await _carRepository.DeleteAsync(id);

            return repositoryResponse;
        }

        public async Task<LayerResponse<List<GetCarResponseDTO>>> GetAllCarsAsync(bool includeDrivers = false, bool includeCoordinates = false) {
            var response = new LayerResponse<List<GetCarResponseDTO>>();
            var repositoryResponse = await _carRepository.GetAllAsync(includeDrivers);

            if (repositoryResponse.Payload is not null && repositoryResponse.Success) {
                if (includeDrivers) {
                    if (includeCoordinates) {
                        response.Payload = repositoryResponse.Payload.Select(c =>
                            new GetFullCarResponseDTO(c) as GetCarResponseDTO).ToList();
                    } else {
                        response.Payload = repositoryResponse.Payload.Select(c => 
                            new GetCarWithDriverResponseDTO(c) as GetCarResponseDTO).ToList();
                    }
                } else {
                    if (includeCoordinates) {
                        response.Payload = repositoryResponse.Payload.Select(c => 
                            new GetCarWithCoordinatesResponseDTO(c) as GetCarResponseDTO).ToList();
                    } else {
                        response.Payload = repositoryResponse.Payload.Select(c => new GetCarResponseDTO(c)).ToList();
                    }
                }
            }

            response.Message = repositoryResponse.Message;
            response.ExMessage = repositoryResponse.ExMessage;
            response.Success = repositoryResponse.Success;
            response.ResponseCode = repositoryResponse.ResponseCode;

            return response;
        }

        public async Task<LayerResponse> UpdateCarAsync(PutCarRequestDTO updatedCar) {
            var repositoryResponse = await _carRepository.UpdateAsync(new Car() {
                Id = updatedCar.Id,
                Registration = updatedCar.Registration,
                Model = updatedCar.Model,
                ProductionYear = updatedCar.ProductionYear,
                LoadCapacityKg  = updatedCar.LoadCapacityKg
            });

            return repositoryResponse;
        }
    }
}
