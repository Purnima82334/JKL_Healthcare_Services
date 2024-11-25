namespace JKLHealthcareSystem.Models
{
    public class Appointment
    {
        public int AppointmentId { get; set; }
        public int CaregiverId { get; set; }
        public int PatientId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Status { get; set; }
        public virtual Caregiver Caregiver { get; set; }
        public virtual Patient Patient { get; set; }
    }
}
