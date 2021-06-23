using Core.DataAccess.AdoNet;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.AdoNet
{
    public class AnGroupDal<Group> : IGroupDal
    {
        public void Add(Entities.Concrete.Group entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Entities.Concrete.Group entity)
        {
            throw new NotImplementedException();
        }

        public Entities.Concrete.Group Get(Expression<Func<Entities.Concrete.Group, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public IList<Entities.Concrete.Group> GetList(Expression<Func<Entities.Concrete.Group, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public IList<Entities.Concrete.Group> GetList(int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public void Update(Entities.Concrete.Group entity)
        {
            throw new NotImplementedException();
        }
    }
}
