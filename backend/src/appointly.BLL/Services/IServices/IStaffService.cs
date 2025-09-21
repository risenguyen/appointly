using appointly.BLL.DTOs.Staff;
using Ardalis.Result;

namespace appointly.BLL.Services.IServices;

public interface IStaffService
{
    Task<Result<StaffResponse>> CreateStaffMemberAsync(
        CreateStaffRequest createStaffRequest,
        CancellationToken cancellationToken
    );
    Task<Result<List<StaffResponse>>> GetStaffAsync(CancellationToken cancellationToken);
    Task<Result<StaffResponse>> GetStaffMemberAsync(int id, CancellationToken cancellationToken);

    Task<Result<StaffResponse>> UpdateStaffMemberAsync(
        int id,
        UpdateStaffRequest updateStaffRequest,
        CancellationToken cancellationToken
    );
    Task<Result> DeleteStaffMemberAsync(int id, CancellationToken cancellationToken);
}
