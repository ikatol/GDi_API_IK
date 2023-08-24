using GDi_API_IK.Model.Entities;

namespace GDi_API_IK.Model.DTOs.Cars {
    public class GetCarWithCoordinatesResponseDTO : GetCarResponseDTO {
        public double Longitude { get; set; }
        public double Latitude { get; set; }

        public GetCarWithCoordinatesResponseDTO(Car car) : base(car) {
            Longitude = car.Longitude;
            Latitude = car.Latitude;
        }
    }
}
