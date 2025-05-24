using Microsoft.EntityFrameworkCore;
using NewBlogApp.Data.Abstractt;
using NewBlogApp.Entitiy;

namespace NewBlogApp.Data.Concrete.EFCore
{

    public class EFPostsRepository : IPostRepository

    {
        private BlogContext _context;

        public EFPostsRepository(BlogContext context)
        {
            _context = context;
        }
        public IQueryable<Post> Posts => _context.Posts;

        public void CreatePost(Post post)
        {
            _context.Posts.Add(post);
            _context.SaveChanges();
        }

        public void DeletePost(Post post)
        {
            _context.Posts.Remove(post);
            _context.SaveChanges();
        }

        public void EditPost(Post post)
        {
            var epost= _context.Posts.FirstOrDefault(x => x.PostId == post.PostId);

            if(epost != null)
            {
                epost.Title = post.Title;
                epost.Content = post.Content;
                epost.Url = post.Url;
                epost.Description = post.Description;
                epost.IsActive = post.IsActive;

                _context.SaveChanges();
            }
        }

        public void EditPost(Post post, int[] tagIds)
        {
            var epost = _context.Posts.Include(t=>t.Tags).FirstOrDefault(x => x.PostId == post.PostId);

            if (epost != null)
            {
                epost.Title = post.Title;
                epost.Content = post.Content;
                epost.Url = post.Url;
                epost.Description = post.Description;
                epost.IsActive = post.IsActive;

                epost.Tags = _context.Tags.Where(t => tagIds.Contains(t.TagId)).ToList();
                _context.SaveChanges();
            }
        }
    }
    
}
