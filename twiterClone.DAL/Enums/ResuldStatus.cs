using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace twiterClone.DAL.Enums
{
    public enum ResuldStatus
    {
        [Display(Name = "Hata")]
        Erorr = -1,
        [Display(Name = "Uyarı")]
        Warning = 0,
        [Display(Name = "Başarılı")]
        Succes = 1
    }
}
