namespace appointly.DAL.Entities;

public class Appointment
{
    public int Id { get; set; }
    public required int ClientId { get; set; }
    public Client? Client { get; set; }
    public required int EmployeeId { get; set; }
    public Employee? Employee { get; set; }
    public required int TreatmentId { get; set; }
    public Treatment? Treatment { get; set; }
    public required DateTime StartTime { get; set; }
    public DateTime EndTime =>
        Treatment != null ? StartTime.AddMinutes(Treatment.DurationInMinutes) : StartTime;
}
