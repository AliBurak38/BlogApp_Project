using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewBlogApp.Data.Abstractt;

namespace NewBlogApp.ViewComponents
{
    public class TagsMenu : ViewComponent
    {
        private readonly ITagRepository _tagRepository;
        public TagsMenu(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {
            var tags = await _tagRepository.Tags.ToListAsync();
            return View("Default",tags);
        }
    }
}
