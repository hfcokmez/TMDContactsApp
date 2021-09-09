using Core.DataAccess.AdoNet;
using TMDContactsApp.DataAccess.Abstract;
using Entities.Concrete;

namespace DataAccess.Concrete
{
    public class AnGroupDal : AnEntityRepositoryBaseAsync<Group>, IGroupDal
    {
    }
}
