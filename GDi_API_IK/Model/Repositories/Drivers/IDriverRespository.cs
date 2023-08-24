using GDi_API_IK.Model.Entities;

namespace GDi_API_IK.Model.Repositories.Drivers {
    public interface IDriverRespository {
        Task<LayerResponse<List<Driver>>> GetAllAsync();
    }
}
