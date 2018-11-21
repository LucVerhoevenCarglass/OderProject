using Order_api.Controllers.Customers.Emails;
using Order_domain.Customers.Emails;
using Xunit;

namespace Order_api.tests.Controllers.Customers.Emails
{
    public class EmailMapperTests
    {
        [Fact]
        public void ToDto()
        {
            string localPart = "mail";
            string domain = "domain.be";
            string complete = "mail@domain.be";

            EmailDto emailDto = new EmailMapper().ToDto(Email.EmailBuilder.Email()
                .WithLocalPart(localPart)
                .WithDomain(domain)
                .WithComplete(complete)
                .Build());
            
            Assert.Equal(localPart, emailDto.LocalPart);
            Assert.Equal(domain, emailDto.Domain);
            Assert.Equal(complete, emailDto.Complete);
        }

        [Fact]
        public void ToDomain()
        {
            string localPart = "mail";
            string domain = "domain.be";
            string complete = "mail@domain.be";

            Email email = new EmailMapper().ToDomain(new EmailDto()
                .WithLocalPart("mail")
                .WithDomain("domain.be")
                .WithComplete("mail@domain.be"));
            
            Assert.Equal(localPart, email.LocalPart);
            Assert.Equal(domain, email.Domain);
            Assert.Equal(complete, email.Complete);
        }
    }
}
