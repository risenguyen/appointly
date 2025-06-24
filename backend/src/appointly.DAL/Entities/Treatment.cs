using appointly.DAL.Enums;

namespace appointly.DAL.Entities;

public class Treatment
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required decimal Price { get; set; }
    public required int DurationInMinutes { get; set; }
    public required TreatmentType TreatmentType { get; set; }
}
