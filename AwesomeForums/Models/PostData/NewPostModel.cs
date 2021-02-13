using System;
using System.ComponentModel.DataAnnotations;

namespace AwesomeForums.Models.PostData
{
    public class NewPostModel
    {
        public string ForumName { get; set; }
        public int ForumId { get; set; }
        public string AuthorName { get; set; }
        public string ForumImageUrl { get; set; }
        [Required(ErrorMessage = "Title no specified")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Content no specified")]
        public string Content { get; set; }
        public DateTime Created { get; set; }
    }
}
