﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Herotale.Models
{
    public class TicketAuth
    {
        public HttpCookie Encrypt(string id)
        {
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, id, DateTime.Now, DateTime.Now.AddHours(3), false, "", FormsAuthentication.FormsCookiePath);
            HttpCookie c = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));
            return c;
        }

        public int Decrypt()
        {
            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(System.Web.HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName].Value);
            int key = Convert.ToInt32(ticket.Name);
            return key;
        }
    }
}