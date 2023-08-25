using GDi_API_IK.Model.DTOs;
using GDi_API_IK.Model.DTOs.Drivers;

namespace GDi_API_IK.Model.Services.Drivers {
    public interface IDriverService {
        Task<LayerResponse<List<GetDriverResponseDTO>>> GetAllDriversAsync();
        Task<LayerResponse> UpdateDriverAsync(PutDriverRequestDTO driver);
        Task<LayerResponse> DeleteDriverAsync(int id);
        Task<LayerResponse> AddDriverAsync(PostDriverRequestDTO newDriver);
    }
}
