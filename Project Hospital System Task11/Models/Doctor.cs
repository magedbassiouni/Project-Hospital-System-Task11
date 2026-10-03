namespace Project_Hospital_System_Task11.Models
{
    public class Doctor
    {
        public int DoctorId { get; set; }

        public string Name { get; set; }

        public string Image { get; set; }

        public int SpecializationId { get; set; }

        public Specialization Specialization { get; set; }

        public ICollection<Appointment> Appointments { get; set; }
            = new List<Appointment>();
    }
}