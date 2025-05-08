namespace appointly.BLL.DTOs.Treatments;

public class CreateTreatmentRequest
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required decimal Price { get; set; }
    public required int DurationInMinutes { get; set; }
}
