using appointly.DAL.Entities;

namespace appointly.DAL.Repositories.IRepositories;

public interface IStaffRepository
{
    Task<Staff> CreateStaffAsync(Staff staff, CancellationToken cancellationToken);
}
