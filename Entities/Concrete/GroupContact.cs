using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class GroupContact: EntityBase, IEntity
    {
        public int GroupId { get; set; }
        public int ContactId { get; set; }
    }
}
