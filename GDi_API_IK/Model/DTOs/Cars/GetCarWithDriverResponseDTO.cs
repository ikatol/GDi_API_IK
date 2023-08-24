using GDi_API_IK.Model.Entities;

namespace GDi_API_IK.Model.DTOs.Cars {
    public class GetCarWithDriverResponseDTO : GetCarResponseDTO {
        public Driver? Driver { get; set; }

        public GetCarWithDriverResponseDTO(Car car) : base(car) {
            Driver = car.Driver;
        }
    }
}
