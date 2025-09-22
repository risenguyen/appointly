using appointly.BLL.DTOs.Staff;
using appointly.BLL.Services.IServices;
using appointly.DAL.Entities;
using appointly.DAL.Repositories.IRepositories;
using Ardalis.Result;

namespace appointly.BLL.Services;

public class StaffService(IStaffRepository staffRepository) : IStaffService
{
    private readonly IStaffRepository _staffRepository = staffRepository;

    public async Task<Result<StaffResponse>> CreateStaffMemberAsync(
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
        var createdStaff = await _staffRepository.CreateStaffMemberAsync(staff, cancellationToken);
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

    public async Task<Result<StaffResponse>> GetStaffMemberAsync(
        int id,
        CancellationToken cancellationToken
    )
    {
        var staffMember = await _staffRepository.GetStaffMemberAsync(id, cancellationToken);
        if (staffMember == null)
        {
            return Result.NotFound($"Staff member with ID {id} cannot be found.");
        }

        var response = new StaffResponse
        {
            Id = staffMember.Id,
            FirstName = staffMember.FirstName,
            LastName = staffMember.LastName,
            Email = staffMember.Email,
            Phone = staffMember.Phone,
        };
        return Result.Success(response);
    }

    public async Task<Result<StaffResponse>> UpdateStaffMemberAsync(
        int id,
        UpdateStaffRequest updateStaffRequest,
        CancellationToken cancellationToken
    )
    {
        var staffMemberToUpdate = await _staffRepository.GetStaffMemberAsync(id, cancellationToken);
        if (staffMemberToUpdate == null)
        {
            return Result.NotFound($"Staff member with ID {id} cannot be found.");
        }

        staffMemberToUpdate.FirstName =
            updateStaffRequest.FirstName ?? staffMemberToUpdate.FirstName;
        staffMemberToUpdate.LastName = updateStaffRequest.LastName ?? staffMemberToUpdate.LastName;
        staffMemberToUpdate.Email = updateStaffRequest.Email ?? staffMemberToUpdate.Email;
        staffMemberToUpdate.Phone = updateStaffRequest.Phone ?? staffMemberToUpdate.Phone;

        await _staffRepository.UpdateStaffMemberAsync(staffMemberToUpdate, cancellationToken);

        var response = new StaffResponse
        {
            Id = staffMemberToUpdate.Id,
            FirstName = staffMemberToUpdate.FirstName,
            LastName = staffMemberToUpdate.LastName,
            Email = staffMemberToUpdate.Email,
            Phone = staffMemberToUpdate.Phone,
        };
        return Result.Success(response);
    }

    public async Task<Result> DeleteStaffMemberAsync(int id, CancellationToken cancellationToken)
    {
        var staffMember = await _staffRepository.GetStaffMemberAsync(id, cancellationToken);
        if (staffMember == null)
        {
            return Result.NotFound($"Staff member with ID {id} cannot be found.");
        }

        await _staffRepository.DeleteStaffMemberAsync(staffMember, cancellationToken);
        return Result.Success();
    }
}
