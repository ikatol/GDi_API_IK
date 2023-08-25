using GDi_API_IK.Model.DTOs.Drivers;
using GDi_API_IK.Model.Entities;

namespace GDi_API_IK.Model.Repositories.Drivers {
    public interface IDriverRespository {
        Task<LayerResponse<List<Driver>>> GetAllAsync();
        Task<LayerResponse> UpdateAsync(Driver driver);
        Task<LayerResponse> DeleteAsync(int id);
        Task<LayerResponse> AddAsync(Driver driver);
        Task<LayerResponse<Driver>> GetDriverAsync(int id);
    }
}
