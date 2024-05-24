using DTO;
using SecurityMarket.Model;

namespace SecurityMarket.Abstraction
{
    public interface IUserRepository
    {
        public void AddUser(string email, string password, UserRoleType userRoleType);

        public UserRoleType CheckUser(string email, string password);
    }
}
