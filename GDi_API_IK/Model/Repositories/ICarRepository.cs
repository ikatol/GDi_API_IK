using GDi_API_IK.Model.Entities;

namespace GDi_API_IK.Model.Repositories {
    public interface ICarRepository {
        Task<LayerResponse<List<Car>>> GetAllAsync(bool includeDrivers = false);
        Task<LayerResponse> AddAsync(Car car);

        Task<LayerResponse> DeleteAsync(int id);

        Task<LayerResponse> UpdateAsync(Car car);
        Task<LayerResponse<Car>> GetByRegistrationAsync(string registration);

        Task<LayerResponse<Car>> GetCarAsync(int id, bool includeDriver = false);

        Task<LayerResponse> AssignDriver(int CarId, int? DriverId);
    }
}
