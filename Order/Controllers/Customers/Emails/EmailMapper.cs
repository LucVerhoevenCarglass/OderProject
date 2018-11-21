using Oder_infrastructure.dto;
using Order_domain.Customers.Emails;

namespace Order_api.Controllers.Customers.Emails
{
    public class EmailMapper : Mapper<EmailDto, Email>
    {
        public override EmailDto ToDto(Email email)
        {
            return new EmailDto()
                .WithLocalPart(email.LocalPart)
                .WithDomain(email.Domain)
                .WithComplete(email.Complete);
        }

        public override Email ToDomain(EmailDto emailDto)
        {
            return Email.EmailBuilder.Email()
                .WithLocalPart(emailDto.LocalPart)
                .WithDomain(emailDto.Domain)
                .WithComplete(emailDto.Complete)
                .Build();
        }
    }
}
