using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities.Abstract
{
    public abstract class EntityBase
    {
        public virtual int Id { get; set; }
        //public virtual DateTime CreatedDate { get; set; } = DateTime.Now;
        //public virtual DateTime ModifiedDate { get; set; } = DateTime.Now;
    }
}
