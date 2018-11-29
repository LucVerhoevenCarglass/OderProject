namespace Oder_infrastructure.dto
{
    public abstract class Mapper<DTO, DOMAIN>
    {
        public abstract DTO ToDto(DOMAIN domainObject);
        public abstract DOMAIN ToDomain(DTO dtoObject);

    }
}
