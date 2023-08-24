using GDi_API_IK.Model.Entities;

namespace GDi_API_IK.Model.DTOs.Cars {
    public class GetCarResponseDTO {
        public int Id { get; set; }
        public string Model { get; set; } = string.Empty;
        public string Registration { get; set; } = string.Empty;
        public int ProductionYear { get; set; }
        public int LoadCapacityKg { get; set; }

        public GetCarResponseDTO(Car car) {
            Id = car.Id;
            Model = car.Model;
            Registration = car.Registration;
            ProductionYear = car.ProductionYear;
            LoadCapacityKg = car.LoadCapacityKg;
        }
    }
}
