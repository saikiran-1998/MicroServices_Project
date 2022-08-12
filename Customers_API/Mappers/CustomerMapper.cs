namespace Customers_API.Mappers
{
    public class CustomerMapper: AutoMapper.Profile
    {
        public CustomerMapper()
        {
            CreateMap<Data.Customer, Models.Customer>();
        }
    }
}
