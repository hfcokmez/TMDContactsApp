using Core.DataAccess.AdoNet;
using TMDContactsApp.DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using TMDContactsApp.Core.Entities.Concrete;

namespace DataAccess.Concrete
{
    public class AnGroupDal : AnEntityRepositoryBaseAsync<Group>, IGroupDal
    {
        public AnGroupDal(IOptions<ConnectionSettings> connectionSettings) : base(connectionSettings)
        {
        }
    }
}
