using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities.Services
{
    public interface IEmailSendHelper
    {
        bool SendEmailWithContext(string email, string htmlBody, string textBody);
    }
}
