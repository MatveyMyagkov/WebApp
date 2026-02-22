namespace DataAccess.Interfaces;

public interface IUserRepository
{
    List<User> GetUsers();

    void AddUser(User user);

}