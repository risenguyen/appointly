namespace appointly.DAL.Entities;

public class Employee
{
    public int Id { get; set; }
    public required string FullName { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public List<EmployeeSalonService> EmployeeSalonServices { get; set; } = [];
}
