namespace Project_Hospital_System_Task11.Models
{
    public class Specialization
    {
        public int SpecializationId { get; set; }

        public string Name { get; set; }

        public ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();
    }
}