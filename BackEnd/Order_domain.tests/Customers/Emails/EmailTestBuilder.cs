using Oder_infrastructure.builders;
using Order_domain.Customers.Emails;

namespace Order_domain.tests.Customers.Emails
{
    public class EmailTestBuilder : Builder<Email>
    {
        private readonly Email.EmailBuilder _emailBuilder;

        private EmailTestBuilder(Email.EmailBuilder emailBuilder)
        {
            _emailBuilder = emailBuilder;
        }

        public static EmailTestBuilder AnEmptyEmail()
        {
            return new EmailTestBuilder(Email.EmailBuilder.Email());
        }

        public static EmailTestBuilder AnEmail()
        {
            return new EmailTestBuilder(Email.EmailBuilder.Email()
                .WithLocalPart("niels")
                .WithDomain("mymail.be")
                .WithComplete("niels@mymail.be"));
        }

        public override Email Build()
        {
            return _emailBuilder.Build();
        }

        public EmailTestBuilder WithLocalPart(string localPart)
        {
            _emailBuilder.WithLocalPart(localPart);
            return this;
        }

        public EmailTestBuilder WithDomain(string domain)
        {
            _emailBuilder.WithDomain(domain);
            return this;
        }

        public EmailTestBuilder WithComplete(string complete)
        {
            _emailBuilder.WithComplete(complete);
            return this;
        }

    }
}
