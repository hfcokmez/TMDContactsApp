using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.JsonWebToken
{
    public interface ITokenHelper
    {
        AccessToken CreateToken(User user);
    }
}
