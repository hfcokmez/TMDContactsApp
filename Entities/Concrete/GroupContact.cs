using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class GroupContact: IEntity
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public int ContactId { get; set; }
    }
}
