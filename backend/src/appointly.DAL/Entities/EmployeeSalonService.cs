namespace appointly.DAL.Entities;

public class EmployeeSalonService
{
    public int EmployeeId { get; set; }
    public Employee Employee { get; set; } = null!;
    public int SalonServiceId { get; set; }
    public SalonService SalonService { get; set; } = null!;
}
