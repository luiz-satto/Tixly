using Microsoft.EntityFrameworkCore;
using Tixly.Infrastructure.Data;

namespace Tixly.Infrastructure.Repositories.User
{
    public class UserRepository(AppDbContext dbContext)
        : RepositoryBase<Models.User>(dbContext), IUserRepository
    {
        private AppDbContext AppDbContext { get; init; } = dbContext;

        public async Task<IEnumerable<Models.User>> GetAsync(CancellationToken cancellationToken)
        {
            var result = await AppDbContext.Users
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return result ?? default!;
        }

        public async Task<Models.User> GetAsync(Guid id, CancellationToken cancellationToken)
        {
            var result = await AppDbContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            return result ?? default!;
        }
    }
}
