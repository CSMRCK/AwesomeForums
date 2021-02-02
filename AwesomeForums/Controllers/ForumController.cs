using AwesomeForums.Models;
using AwesomeForums.Models.ForumData;
using AwesomeForums.Models.PostData;
using AwesomeForums.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

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
