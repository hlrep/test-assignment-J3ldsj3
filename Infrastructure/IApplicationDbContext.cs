using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public interface IApplicationDbContext
{
    public DbSet<User> Users { get; set; }

    public int SaveChanges();
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
