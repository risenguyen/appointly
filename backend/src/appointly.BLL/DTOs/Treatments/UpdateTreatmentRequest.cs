using appointly.DAL.Enums;

namespace appointly.BLL.DTOs.Treatments;

public class UpdateTreatmentRequest
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required decimal Price { get; set; }
    public required int DurationInMinutes { get; set; }
    public required TreatmentType TreatmentType { get; set; }
}
