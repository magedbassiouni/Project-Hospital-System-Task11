namespace Project_Hospital_System_Task11.Models
{
    public class Appointment
    {
        public int AppointmentId { get; set; }

        public string PatientName { get; set; }

        public DateTime Date { get; set; }

        public DateTime Time { get; set; }

        public int DoctorId { get; set; }

        public Doctor Doctor { get; set; }
    }
}