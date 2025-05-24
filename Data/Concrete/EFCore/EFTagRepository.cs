using NewBlogApp.Data.Abstractt;
using NewBlogApp.Entitiy;

namespace NewBlogApp.Data.Concrete.EFCore
{
    public class EFTagRepository : ITagRepository
    {

        private readonly BlogContext _context;


        public EFTagRepository(BlogContext context)
        {
            _context = context;
        }
        public IQueryable<Tag> Tags => _context.Tags;

        public void Createtag(Tag tag)
        {
            _context.Tags.Add(tag);
            _context.SaveChanges();
        }
    }
}
