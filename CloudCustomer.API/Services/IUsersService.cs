using CloudCustomer.API.Model;

namespace CloudCustomer.API.Services
{
    public interface IUsersService
    {
        Task<List<User>?> GetAllUsers();
    }
}
