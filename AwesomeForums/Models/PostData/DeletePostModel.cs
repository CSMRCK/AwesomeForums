﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeForums.Models.PostData
{
    public class DeletePostModel
    {
        public int PostId { get; set; }
        public string PostAuthor { get; set; }
        public string PostContent { get; set; }
    }
}
