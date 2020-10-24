using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using twiterClone.DAL.Enums;

namespace twitterClone.WepApi.Models.AppModel
{
    public class ResuldModel
    {
        public ResuldStatus ResuldStatus { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }
    }
}
