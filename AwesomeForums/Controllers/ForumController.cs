using AwesomeForums.Models;
using AwesomeForums.Models.ForumData;
using AwesomeForums.Models.PostData;
using AwesomeForums.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AwesomeForums.Controllers
{
    public class ForumController : Controller
    {
        private readonly IForum _forumServise;
        private readonly IPost _postService;
        public ForumController(IForum forumService, IPost postService)
        {
            _forumServise = forumService;
            _postService = postService;
        }

        public IActionResult Index()
        {
            var forums = _forumServise.GetAll()
                .Select(forum => new ForumListingModel
                {
                    Id = forum.Id,
                    Name = forum.Title,
                    Description = forum.Description
                });

            var model = new ForumIndexModel
            {
                ForumList = forums
            };

            return View(model);
        }

        public IActionResult Topic(int id)
        {
            var forum = _forumServise.GetById(id);
            var posts = forum.Posts;
            var postListings = posts.Select(post => new PostListingModel
            {
                Id = post.Id,
                AuthorId = post.User.Id,
                AuthorName = post.User.UserName,
                Title = post.Title,
                DatePosted = post.Created.ToString(""),
                Forum = BuildForumListing(post)
            });
            var model = new ForumTopicModel
            {
                Posts = postListings,
                Forum = BuildForumListing(forum)
            };

            return View(model);
        }
        public IActionResult Create()
        {

            var model = new NewForumModel();
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
        public async Task<IActionResult> Create(NewForumModel model)
        {
            var forum = new Forum
            {
                Title = model.Title,
                Description = model.Description,
                Created = DateTime.Now,
                Image = "@/images/forum/default.png"
            };
            if (string.IsNullOrEmpty(forum.Title))
            {
                ModelState.AddModelError("Title", "Please enter the title");
            }
            if (string.IsNullOrEmpty(forum.Description))
            {
                ModelState.AddModelError("Description", "Description is incorrect");
            }

            if (ModelState.IsValid)
            {
                await _forumServise.Create(forum);
                return RedirectToAction("Index", "Forum");
            }
            return View(model);
        }
       
        private ForumListingModel BuildForumListing(Post post)
        {
            var forum = post.Forum;
            return BuildForumListing(forum);
        }
        private ForumListingModel BuildForumListing(Forum forum)
        {
            return new ForumListingModel
            {
                Id = forum.Id,
                Name = forum.Title,
                Description = forum.Description,
                ImageUrl = forum.Image
            };
        }
    }

}
