using appointly.DAL.Entities;

namespace appointly.DAL.Repositories.IRepositories;

public interface ITreatmentRepository
{
    Task<Treatment> CreateTreatmentAsync(Treatment treatment, CancellationToken cancellationToken);
    Task<Treatment> UpdateTreatmentAsync(Treatment treatment, CancellationToken cancellationToken);
    Task DeleteTreatmentAsync(Treatment treatment, CancellationToken cancellationToken);
    Task<Treatment?> GetTreatmentAsync(int id, CancellationToken cancellationToken);
    Task<List<Treatment>> GetTreatmentsAsync(CancellationToken cancellationToken);
}
