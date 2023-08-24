namespace GDi_API_IK.Model.Entities {
    public class Car {
        public int Id { get; set; }
        public string Model { get; set; } = null!;
        public string Registration { get; set; } = null!;
        public int ProductionYear { get; set; }
        public int LoadCapacityKg { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }

        public int? DriverId { get; set; }
        public virtual Driver? Driver { get; set; }
    }
}
