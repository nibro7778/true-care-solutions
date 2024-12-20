using System.Data;

namespace Staffs.Application;

public interface IStaffRepository
{
    public Task Add(Staff staff);
    Task<Staff?> Get(Guid id);
}