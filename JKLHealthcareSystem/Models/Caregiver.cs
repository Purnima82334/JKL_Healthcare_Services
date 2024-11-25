namespace JKLHealthcareSystem.Models
{
    public class Caregiver
    {
        public int CaregiverId { get; set; }
        public string Name { get; set; }
        public bool Availability { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}