using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewBlogApp.Data.Abstractt;

namespace NewBlogApp.ViewComponents
{
    public class NewPost:ViewComponent
    {

        private readonly IPostRepository postRepository;

        public NewPost(IPostRepository postRepository)
        {
            this.postRepository = postRepository;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {
            var posts = await postRepository.Posts.OrderByDescending(p=>p.PublishedOn).Take(2).ToListAsync();
            return View("Default",posts);
        }
    }
}
