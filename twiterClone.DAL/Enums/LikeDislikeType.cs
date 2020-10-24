using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace twiterClone.DAL.Enums
{
    public enum LikeDislikeType
    {
        [Display(Name ="Beğen")]
        Like=1,

        [Display(Name = "Beğenme")]
        Dislike =0,
    }
}
