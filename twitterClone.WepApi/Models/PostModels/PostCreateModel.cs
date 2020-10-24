using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace twitterClone.WepApi.Models.PostModels
{
    public class PostCreateModel
    {
        public int ID { get; set; }

        public string PostTitle { get; set; }

        public List<IFormFile> postFiles { get; set; }

        public string PostContent { get; set; }

        public string PostImage { get; set; }


    }
}
