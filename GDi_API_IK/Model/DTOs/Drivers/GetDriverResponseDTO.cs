using GDi_API_IK.Model.Entities;

namespace GDi_API_IK.Model.DTOs.Drivers {
    public class GetDriverResponseDTO {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public GetDriverResponseDTO(Driver driver) {
            Id = driver.Id;
            Name = driver.Name;
        }
    }
}
