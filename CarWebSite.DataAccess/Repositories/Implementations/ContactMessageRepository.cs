using CarWebSite.DataAccess.Context;
using CarWebSite.DataAccess.Repositories.Interfaces;
using CarWebSite.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarWebSite.DataAccess.Repositories.Implementations
{
    public class ContactMessageRepository : GenericRepository<ContactMessageData>, IContactMessageRepository
    {
        public ContactMessageRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<ContactMessageData>> GetByEmailAsync(string email)
        {
            return await _context.Set<ContactMessageData>()
                .Where(cm => cm.Email == email)
                .ToListAsync();
        }
    }
}