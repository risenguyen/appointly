using appointly.BLL.DTOs.Staff;
using Ardalis.Result;

namespace appointly.BLL.Services.IServices;

public interface IStaffService
{
    Task<Result<StaffResponse>> CreateStaffAsync(
        CreateStaffRequest createStaffRequest,
        CancellationToken cancellationToken
    );
}
