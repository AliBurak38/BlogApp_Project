using NewBlogApp.Entitiy;

namespace NewBlogApp.Data.Abstractt
{
    public interface ITagRepository
    {

        IQueryable<Tag> Tags { get; }


        void Createtag(Tag tag);   

        
    }
}
