namespace appointly.DAL.Entities;

public class Treatment
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required decimal Price { get; set; }
    public required int DurationInMinutes { get; set; }
    public List<EmployeeTreatment> AssignedEmployees { get; set; } = [];
}
