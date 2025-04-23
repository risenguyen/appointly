namespace appointly.DAL.Entities;

public class EmployeeTreatment
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public Employee Employee { get; set; } = null!;
    public int SalonServiceId { get; set; }
    public Treatment Treatment { get; set; } = null!;
}
