namespace appointly.DAL.Entities;

public class Appointment
{
    public int Id { get; set; }
    public required int ClientId { get; set; }
    public Client? Client { get; set; }
    public required int StaffId { get; set; }
    public Staff? Staff { get; set; }
    public required int TreatmentId { get; set; }
    public Treatment? Treatment { get; set; }
    public required DateTime StartTime { get; set; }
}
