using NewBlogApp.Entitiy;

namespace NewBlogApp.Data.Abstractt
{
    public interface ICommentRepository
    {

        IQueryable<Comment> Comments { get; }   

       void createComment(Comment comment); 
    }
}
