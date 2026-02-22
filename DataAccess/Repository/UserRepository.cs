using DataAccess.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Repositories;

internal class UserRepository : IUserRepository
{
    private readonly WebApplicationDBContext _dbContext;

    public UserRepository(WebApplicationDBContext dBContext)
    {
        _dbContext = dBContext;
    }

    public void AddUser(User user)
    {
        _dbContext.Users.Add(user);
        _dbContext.SaveChanges();
    }

    public List<User> GetUsers()
    {
        return _dbContext.Users.ToList();
    }
}