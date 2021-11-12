using Core.DataAccess.AdoNet;
using TMDContactsApp.DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using TMDContactsApp.Core.Entities.Concrete;

namespace DataAccess.Concrete
{
    public class AnContactDal : AnEntityRepositoryBaseAsync<Contact>, IContactDal
    {
        public AnContactDal(IOptions<ConnectionSettings> connectionSettings) : base(connectionSettings)
        {
        }
    }
}
