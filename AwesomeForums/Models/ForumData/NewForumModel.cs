using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace AwesomeForums.Models.ForumData
{
    public class NewForumModel
    {
        [Required(ErrorMessage = "Title no specified")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Description no specified")]
        public string Description { get; set; }
    }
}
