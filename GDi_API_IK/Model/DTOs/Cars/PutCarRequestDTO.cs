namespace GDi_API_IK.Model.DTOs.Cars {
    public class PutCarRequestDTO {
        public int Id { get; set; }
        public string Model { get; set; } = string.Empty;
        public string Registration { get; set; } = string.Empty;
        public int ProductionYear { get; set; }
        public int LoadCapacityKg { get; set; }
    }
}
