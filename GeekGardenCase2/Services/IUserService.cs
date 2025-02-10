using GeekGardenCase2.Models;

namespace GeekGardenCase2.Services
{
    public interface IUserService
    {
        Task<User> Authenticate(string username, string password);
        Task<IEnumerable<User>> GetAll();
        Task<User> GetById(int id);
        Task<User> GetByUsername(string username);
        Task<IEnumerable<User>> GetAllByDepartment(int idUser);
    }
}
