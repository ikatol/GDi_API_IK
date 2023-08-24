namespace GDi_API_IK.Model.Entities {
    public class AssignementLog {
        public int Id { get; set; }
        public Car Car { get; set; }
        public Driver Driver { get; set; }
        public DateTime Date { get; set; }
    }
}
