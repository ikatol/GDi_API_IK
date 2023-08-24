using GDi_API_IK.Model.Entities;

namespace GDi_API_IK.Model.Repositories {
    public interface ICarRepository {
        Task<LayerResponse<List<Car>>> GetAllAsync(bool includeDrivers = false);
        Task<LayerResponse> AddAsync(Car car);

        Task<LayerResponse> DeleteAsync(int id);

        Task<LayerResponse> UpdateAsync(Car car);
    }
}
