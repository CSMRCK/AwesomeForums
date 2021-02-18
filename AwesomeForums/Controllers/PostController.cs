using AwesomeForums.Models;
using AwesomeForums.Models.PostData;
using AwesomeForums.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AwesomeForums.Controllers
{
    public class PostController : Controller
    {
        private readonly IPost _postService;
        private readonly IForum _forumService;
        private static UserManager<IdentityUser> _userManager;
        public PostController(IPost postService, IForum forumService, UserManager<IdentityUser> userManager)
        {
            _postService = postService;
            _forumService = forumService;
            _userManager = userManager;
        }
        public IActionResult Create(int id)
        {
            var forum = _forumService.GetById(id);
            var model = new NewPostModel
            {
                ForumName = forum.Title,
                ForumId = forum.Id,
                ForumImageUrl = forum.Image,
                AuthorName = User.Identity.Name
            };
            String UserName = User.FindFirstValue(ClaimTypes.Name);

            if (UserName != null)
            {
                return View(model);
            }
            else
            {
                ///Identity/Account/Register
                return Redirect("~/Identity/Account/Register");
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddPost(NewPostModel model)
        {
            var userId = _userManager.GetUserId(User);
            var user = await _userManager.FindByIdAsync(userId);
            var post = BuildPost(model, user);
            await _postService.Add(post);

            return RedirectToAction("Index", "Post", new { id = post.Id });
        }

        private Post BuildPost(NewPostModel model, IdentityUser user)
        {
            var forum = _forumService.GetById(model.ForumId);
            return new Post
            {
                Title = model.Title,
                Content = model.Content,
                Created = DateTime.Now,
                User = user,
                Forum = forum
            };
        }

        public async Task<IActionResult> Edit(int id)
        {
            var post = _postService.GetById(id);
            var model = new NewPostModel
            {
                Title = post.Title,
                Content = post.Content,
                Created = post.Created,
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id,NewPostModel model)
        {
            var newContent = model.Content;
            var post = _postService.GetById(id);
            int iz = post.Id;

            await _postService.EditPostContent(iz, newContent);

            return RedirectToAction("Index", "Post", new { id = post.Id });
        }

        public IActionResult Index(int id)
        {
            var post = _postService.GetById(id);
            var model = new PostIndexModel
            {
                Id = post.Id,
                Title = post.Title,
                AuthorId = post.User.Id,
                AuthorName = post.User.UserName,
                Created = post.Created,
                PostContent = post.Content
            };
            return View(model);
        }
    }
}
