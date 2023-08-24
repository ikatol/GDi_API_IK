using GDi_API_IK.Model.DTOs.Cars;

namespace GDi_API_IK.Model.Services
{
    public interface ICarService {
        Task<LayerResponse<List<GetCarResponseDTO>>> GetAllCarsAsync(bool includeDrivers = false, bool includeCoordinates = false);
        Task<LayerResponse> AddCarAsync(PostCarRequestDTO newCar);

        Task<LayerResponse> DeleteCarAsync(int id);

        Task<LayerResponse> UpdateCarAsync(PutCarRequestDTO updatedCar);
    }
}
