using GDi_API_IK.Model.DContext;
using GDi_API_IK.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace GDi_API_IK.Model.Repositories.Drivers {
    public class DriverRepository : IDriverRespository {
        private readonly DataContext _dataContext;

        public DriverRepository(DataContext dataContext) {
            _dataContext = dataContext;
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
    }
}
