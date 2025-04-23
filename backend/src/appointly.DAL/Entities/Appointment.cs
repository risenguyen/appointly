namespace appointly.DAL.Entities;

public class Appointment
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public Employee Employee { get; set; } = null!;
    public int SalonServiceId { get; set; }
    public SalonService SalonService { get; set; } = null!;
    public required string ClientName { get; set; }
    public string? ClientPhone { get; set; }
    public required DateTime StartTime { get; set; }
    public DateTime EndTime => StartTime.AddMinutes(SalonService.DurationInMinutes);
}
