using GDi_API_IK.Model.Entities;

namespace GDi_API_IK.Model.DTOs.Cars
{
    public class GetFullCarResponseDTO : GetCarWithCoordinatesResponseDTO {
        public Driver? Driver { get; set; }

        public GetFullCarResponseDTO(Car car) : base(car) {
            Driver = car.Driver;
        }
    }
}
