using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewBlogApp.Data.Abstractt;
using NewBlogApp.Data.Concrete.EFCore;
using NewBlogApp.Entitiy;
using NewBlogApp.Models;
using System.Reflection;
using System.Security.Claims;

namespace NewBlogApp.Controllers
{
    public class PostsController : Controller
    {

        private readonly IPostRepository _Postrepository;
        private readonly ICommentRepository _commentRepository;
        private readonly ITagRepository _tagRepository;


        public PostsController(IPostRepository postrepository, ICommentRepository commentRepository, ITagRepository tagRepository)
        {
            _Postrepository = postrepository;
            _commentRepository = commentRepository;
            _tagRepository = tagRepository;
        }

        public async Task<IActionResult> Index(string? tag)
        {
            var posts = _Postrepository.Posts;

            if (!string.IsNullOrEmpty(tag))
            {

                posts = posts.Where(x => x.Tags.Any(t => t.Url == tag));


            }
            return View(new PostsVithTags { Posts = await posts.ToListAsync() });
        }

        public async Task<IActionResult> Details(string Url)
        {
            var post = await _Postrepository.Posts.Include(x => x.Tags).Include(x => x.User).
                Include(x => x.Comments).ThenInclude(p => p.User).
                FirstOrDefaultAsync(p => p.Url == Url);

            return View(post);
        }


        public JsonResult AddComment(int PostId, string Text)
        {
            var userıd = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userName = User.FindFirstValue(ClaimTypes.Name);
            var avatar = User.FindFirstValue(ClaimTypes.UserData);

            var entity = new Comment()
            {
                PostId = PostId,
                Text = Text,
                PublishedOn = DateTime.Now,
                UserId = int.Parse(userıd ?? " ")


            };
            _commentRepository.createComment(entity);
            return Json(new
            {
                userName,
                Text,
                entity.PublishedOn,
                avatar
            });
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]


        public async Task<IActionResult> Create(PostsCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userıd = User.FindFirstValue(ClaimTypes.NameIdentifier);
                _Postrepository.CreatePost(new Post
                {
                    Title = model.Title,
                    Description = model.Description,
                    Content = model.Content,
                    Image = "webtasarım.jpg",
                    Url = model.Url,
                    PublishedOn = DateTime.Now,
                    IsActive = false,
                    UserId = int.Parse(userıd ?? " ")
                });

                return RedirectToAction("Index");
            }
            return View(model);
        }



        public async Task<IActionResult> List(Post model)
        {
            var userıd = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var role = User.FindFirstValue(ClaimTypes.Role);

            var post = _Postrepository.Posts;
            if (!string.IsNullOrEmpty(role))
            {

                post = _Postrepository.Posts;

            }
            else
            {
                post = _Postrepository.Posts.Where(x => x.UserId == int.Parse(userıd ?? " "));

            }
            return View(await post.ToListAsync());




        }


        public IActionResult Edit(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var post = _Postrepository.Posts.Include(z=>z.Tags).FirstOrDefault(x => x.PostId == id);

            if (post == null)
            {
                return NotFound();
            }
            ViewBag.Tags=_tagRepository.Tags.ToList();

            return View(new PostsCreateViewModel {
               Title=post.Title,
              Description= post.Description,
              Content = post.Content,
              Url = post.Url,
                PostId = post.PostId,
                IsActive = post.IsActive,
                Tags = post.Tags
            });
        }


        [HttpPost]

        public async Task<IActionResult> Edit(PostsCreateViewModel model, int[] tagIds)
        {
            if(ModelState.IsValid)
            {

                var entitypost = new Post
                {
                    PostId=model.PostId,
                    Title = model.Title,
                    Description = model.Description,
                    Content = model.Content,
                    Url = model.Url,
                   

                };

                if(User.FindFirstValue(ClaimTypes.Role) == "Admin")
                {
                    entitypost.IsActive = model.IsActive;
                }
                


                _Postrepository.EditPost(entitypost,tagIds);

                return RedirectToAction("List");
            }
            ViewBag.Tags = _tagRepository.Tags.ToList();
            return View(model); 



        }

        public IActionResult Delete(int? id)
        {
            var post = _Postrepository.Posts.FirstOrDefault(x => x.PostId == id);

            if (post == null)
            {
                return NotFound();
            }

            return View(post);

        }

        [HttpPost]

            public async Task<IActionResult> Delete(Post model)
            {
                var post = _Postrepository.Posts.FirstOrDefault(x => x.PostId == model.PostId);
                if (post == null)
                {
                    return NotFound();
                }

                _Postrepository.DeletePost(post);
                return RedirectToAction("List");

            }



        


    }
}
