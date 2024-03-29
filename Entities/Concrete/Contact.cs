﻿using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Contact: EntityBase, IEntity
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Tel { get; set; }
        public string TelBusiness { get; set; }
        public string TelHome { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Photo { get; set; }
        public string Company { get; set; }
        public string Title { get; set; }
        public string BirthDate { get; set; }
        public string Note { get; set; }
    }
}
