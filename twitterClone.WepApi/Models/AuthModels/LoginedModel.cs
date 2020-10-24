using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace twitterClone.WepApi.Models.AuthModels
{
    public class LoginedModel
    {
        public int ID { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Token { get; set; }
    }
}
