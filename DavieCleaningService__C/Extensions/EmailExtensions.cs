using DavieCleaningService__C.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DavieCleaningService__C.Extensions
{
    public static class EmailExtensions
    {
        public static ContactMessage FormatBody(this ContactMessage message)
        {
            string body = $@"You have received an email from {message.FromEmail}.

                             The following message was sent:

                             {message.Message}";
            message.Message = body;
            return message;
        }
    }
}