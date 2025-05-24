using NewBlogApp.Entitiy;

namespace NewBlogApp.Data.Abstractt
{
    public interface IUserRepository
    {
 
        IQueryable<User> Users { get; }   

       void createUser(User user); 
    }
}
