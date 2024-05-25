using LoginApp.Data.Abstract;

namespace LoginApp.Data.Concreate;


public class EfUserRepository : IUserRepository
{
    private LoginContext _context;

    public EfUserRepository(LoginContext context)
    {
        _context = context;
    }

    public IQueryable<User> Users => _context.Users;

    public void CreateUser(User user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();
    }
}