using NewBlogApp.Data.Abstractt;
using NewBlogApp.Entitiy;

namespace NewBlogApp.Data.Concrete.EFCore
{
    public class EFCommentRepository : ICommentRepository
    {

        private readonly BlogContext _comment;

        public EFCommentRepository(BlogContext comment)
        {
            _comment = comment;
        }
        public IQueryable<Comment> Comments => _comment.Comments;
        public void createComment(Comment comment)
        {
            _comment.Comments.Add(comment);
            _comment.SaveChanges();
        }
    }
}
