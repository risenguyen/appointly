namespace appointly.DAL.Entities;

public class SalonService
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required int DurationInMinutes { get; set; }
    public required decimal Price { get; set; }
    public List<EmployeeSalonService> EmployeeSalonServices { get; set; } = [];
}
