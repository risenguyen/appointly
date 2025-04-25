using appointly.DAL.Entities;

namespace appointly.DAL.Repositories.IRepositories;

public interface ITreatmentRepository
{
    Task<Treatment> CreateTreatmentAsync(
        Treatment treatment,
        CancellationToken cancellationToken = default
    );
    Task<Treatment?> GetTreatmentByIdAsync(int id, CancellationToken cancellationToken = default);
}
