using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.DataBind
{
    public interface ISPDataBindHelper<TEntity>
        where TEntity: class, IEntity, new()
    {
        TEntity ReadDataToEntity(int Id, string sProcedure);
        IList<TEntity> ReadDataToEntityList();

    }
}
