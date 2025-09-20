using appointly.BLL.DTOs.Staff;
using appointly.BLL.Services.IServices;
using appointly.DAL.Entities;
using appointly.DAL.Repositories.IRepositories;
using Ardalis.Result;

namespace appointly.BLL.Services;

public class StaffService(IStaffRepository staffRepository) : IStaffService
{
    private readonly IStaffRepository _staffRepository = staffRepository;

    public async Task<Result<StaffResponse>> CreateStaffAsync(
        CreateStaffRequest createStaffRequest,
        CancellationToken cancellationToken
    )
    {
        var staff = new Staff()
        {
            FirstName = createStaffRequest.FirstName,
            LastName = createStaffRequest.LastName,
            Email = createStaffRequest.Email,
            Phone = createStaffRequest.Phone,
        };
        var createdStaff = await _staffRepository.CreateStaffAsync(staff, cancellationToken);
        var response = new StaffResponse()
        {
            Id = createdStaff.Id,
            FirstName = createdStaff.FirstName,
            LastName = createdStaff.LastName,
            Email = createdStaff.Email,
            Phone = createdStaff.Phone,
        };
        return Result.Created(response, $"/api/Staff/{response.Id}");
    }

    public async Task<Result<List<StaffResponse>>> GetStaffAsync(
        CancellationToken cancellationToken
    )
    {
        var staff = await _staffRepository.GetStaffAsync(cancellationToken);
        var response = staff
            .Select(s => new StaffResponse()
            {
                Id = s.Id,
                FirstName = s.FirstName,
                LastName = s.LastName,
                Email = s.Email,
                Phone = s.Phone,
            })
            .ToList();
        return Result.Success(response);
    }
}
