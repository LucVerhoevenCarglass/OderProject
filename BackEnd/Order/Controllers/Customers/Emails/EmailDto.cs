using System;

namespace Order_api.Controllers.Customers.Emails
{
    public class EmailDto
    {
        public string LocalPart { get; set; }
        public string Domain { get; set; }
        public string Complete { get; set; }
        
        public EmailDto WithLocalPart(string localPart)
        {
            LocalPart = localPart;
            return this;
        }

        public EmailDto WithDomain(string domain)
        {
            Domain = domain;
            return this;
        }

        public EmailDto WithComplete(string complete)
        {
            Complete = complete;
            return this;
        }
    }
}
