using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace twitterClone.WepApi.Models.AuthModels
{
    public class RegisterViewModel
    {
        public string UserName { get; set; }

        public List<IFormFile> postFiles { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
      
    }
}
