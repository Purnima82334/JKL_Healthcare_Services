namespace JKLHealthcareSystem.Models
{
    public class Patient
    {
        public int PatientId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string MedicalRecords { get; set; }
        public int CreatedBy { get; set; }
        public virtual User Creator { get; set; }
    }
}
