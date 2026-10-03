namespace Project_Hospital_System_Task11.Models
{
    public class BookAppointmentViewModel
    {
        public int DoctorId { get; set; }

        public string DoctorName { get; set; }

        public string SpecializationName { get; set; }

        public string PatientName { get; set; }

        public DateTime Date { get; set; }

        public DateTime Time { get; set; }
    }
}