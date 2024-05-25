using Microsoft.EntityFrameworkCore;

namespace LoginApp.Data;

public class LoginContext : DbContext
{
    public LoginContext(DbContextOptions<LoginContext> options) : base(options)
    {

    }

    public DbSet<User> Users => Set<User>();
}