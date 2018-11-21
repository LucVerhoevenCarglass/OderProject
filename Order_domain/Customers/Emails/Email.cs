using System;
using System.Collections.Generic;
using System.Text;
using Oder_infrastructure.builders;

namespace Order_domain.Customers.Emails
{
    public class Email
    {
        public string LocalPart { get; }
        public string Domain { get; }
        public string Complete { get; }

        private Email(EmailBuilder emailBuilder)
        {
            LocalPart = emailBuilder.LocalPart;
            Domain = emailBuilder.Domain;
            Complete = emailBuilder.Complete;
        }


        public override string ToString()
        {
            return "Email{" + "localPart='" + LocalPart + '\'' +
                   ", domain='" + Domain + '\'' +
                   ", complete='" + Complete + '\'' +
                   '}';
        }

        public class EmailBuilder : Builder<Email>
        {
            public string LocalPart { get; set; }
            public string Domain { get; set; }
            public string Complete { get; set; }

            public static EmailBuilder Email()
            {
                return new EmailBuilder();
            }


            public override Email Build()
            {
                return new Email(this);
            }

            public EmailBuilder WithLocalPart(String localPart)
            {
                LocalPart = localPart;
                return this;
            }

            public EmailBuilder WithDomain(String domain)
            {
                Domain = domain;
                return this;
            }

            public EmailBuilder WithComplete(String complete)
            {
                Complete = complete;
                return this;
            }
        }

    }
}
