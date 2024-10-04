using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ToDo.Domain.Entity;

namespace ToDo.Domain.DataBase;

public class ApplicationDbContext : IdentityDbContext<User, Role, int>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public ApplicationDbContext()
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql("Server=127.0.0.1;Port=3306;Database=your_database_name;User Id=root;Password=admin;",
            new MySqlServerVersion(new Version(11, 5, 2)));
        base.OnConfiguring(optionsBuilder);
    }

    public DbSet<Entity.ToDo> TodoList { set; get; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Entity.ToDo>().HasQueryFilter(x => !x.IsDelete);

        base.OnModelCreating(modelBuilder);
    }
}