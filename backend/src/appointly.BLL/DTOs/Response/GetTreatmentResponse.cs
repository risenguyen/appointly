namespace appointly.BLL.DTOs.Response;

public class GetTreatmentResponse
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required int DurationInMinutes { get; set; }
    public required decimal Price { get; set; }
}
