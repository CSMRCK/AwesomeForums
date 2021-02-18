﻿using System.Linq;
using System.Threading.Tasks;
using AwesomeForums.Data;
using AwesomeForums.Models;
using AwesomeForums.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;

namespace Forum.Tests
{

    [TestFixture]
    [Category("Services")]
    public class PostServiceTests
    {
        [Test]
        public async Task Create_Post_Creates_New_Post_Via_Context()
        {
            var mockSet = new Mock<DbSet<Post>>();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Add_Post_Writes_Post_To_Database").Options;


            using (var ctx = new ApplicationDbContext(options))
            {
                var postService = new PostService(ctx);

                var post = new Post
                {
                    Title = "writing functional javascript",
                    Content = "some post content"
                };

                await postService.Add(post);
            }


            using (var ctx = new ApplicationDbContext(options))
            {
                Assert.AreEqual(1, ctx.Posts.CountAsync().Result);
                Assert.AreEqual("writing functional javascript", ctx.Posts.SingleAsync().Result.Title);
            }
        }

        [Test]
        public void Get_Post_By_Id_Returns_Correct_Post()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Get_Post_By_Id_Db").Options;

            using (var ctx = new ApplicationDbContext(options))
            {
                ctx.Posts.Add(new Post { Id = 1986, Title = "first post" });
                ctx.Posts.Add(new Post { Id = 223, Title = "second post" });
                ctx.Posts.Add(new Post { Id = 12, Title = "third post" });
                ctx.SaveChanges();
            }

            using (var ctx = new ApplicationDbContext(options))
            {
                var postService = new PostService(ctx);
                var result = postService.GetById(223);
                Assert.AreEqual(result.Title, "second post");
            }
        }

        [Test]
        public void Get_All_Posts_Returns_All_Posts()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Get_Post_By_Id_Db").Options;

            using (var ctx = new ApplicationDbContext(options))
            {
                ctx.Posts.Add(new Post { Id = 21341, Title = "first post" });
                ctx.Posts.Add(new Post { Id = 8144, Title = "second post" });
                ctx.Posts.Add(new Post { Id = 1245, Title = "third post" });
                ctx.SaveChanges();
            }

            using (var ctx = new ApplicationDbContext(options))
            {
                var postService = new PostService(ctx);
                var result = postService.GetAll();
                Assert.AreEqual(3, result.Count());
            }
        }
    }
}