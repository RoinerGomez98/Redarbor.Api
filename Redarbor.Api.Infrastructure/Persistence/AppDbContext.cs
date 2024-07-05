using Microsoft.EntityFrameworkCore;
using Redarbor.Api.Application.Entities;

namespace Redarbor.Api.Infrastructure.Persistence
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { } 

        public DbSet<Redarbors> Redarbors => Set<Redarbors>();
    }
}
