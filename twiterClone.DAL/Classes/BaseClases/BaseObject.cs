﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using twiterClone.DAL.Enums;

namespace twiterClone.DAL.Classes.BaseClases
{
    public class BaseObject
    {
        [Key]
        public int ID { get; set; }

        public ObjectStatus ObjectStatus { get; set; }

        public DateTime CreatedDate { get; set; }

        public int CreatedBy { get; set; }

        public DateTime LastUpdateDate { get; set; }

        public int LastUpdateBy { get; set; }

        public Status Status { get; set; }
    }
}
