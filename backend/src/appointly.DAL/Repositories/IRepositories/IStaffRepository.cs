using appointly.DAL.Entities;

namespace appointly.DAL.Repositories.IRepositories;

public interface IStaffRepository
{
    Task<Staff> CreateStaffMemberAsync(Staff staff, CancellationToken cancellationToken);
    Task<List<Staff>> GetStaffAsync(CancellationToken cancellationToken);
    Task<Staff?> GetStaffMemberAsync(int id, CancellationToken cancellationToken);
    Task<Staff> UpdateStaffMemberAsync(Staff staff, CancellationToken cancellationToken);
    Task DeleteStaffMemberAsync(Staff staff, CancellationToken cancellationToken);
}
