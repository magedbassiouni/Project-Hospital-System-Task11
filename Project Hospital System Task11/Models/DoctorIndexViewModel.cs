namespace Project_Hospital_System_Task11.Models
{
    public class DoctorIndexViewModel
    {
        public List<Doctor> Doctors { get; set; } = new();

        public string Search { get; set; }

        public int? SpecializationId { get; set; }

        public List<Specialization> Specializations { get; set; }
            = new();

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }
    }
}