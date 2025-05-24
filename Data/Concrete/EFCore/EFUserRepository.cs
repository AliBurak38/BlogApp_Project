using NewBlogApp.Data.Abstractt;
using NewBlogApp.Entitiy;

namespace NewBlogApp.Data.Concrete.EFCore
{
    public class EFUserRepository : IUserRepository
    {

        private readonly BlogContext _context;

        public EFUserRepository(BlogContext context)
        {
            _context = context;
        }
        public IQueryable<User> Users => _context.Users;

        public void createUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }
    }
}
