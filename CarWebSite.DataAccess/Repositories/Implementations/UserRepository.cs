using CarWebSite.DataAccess.Context;
using CarWebSite.DataAccess.Repositories.Interfaces;
using CarWebSite.Domain.Entities;

namespace CarWebSite.DataAccess.Repositories.Implementations
{
    public class UserRepository : GenericRepository<UserData>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context) { }
    }
}
