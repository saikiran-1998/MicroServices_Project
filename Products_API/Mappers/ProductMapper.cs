namespace Products_API.Mappers
{
    public class ProductMapper : AutoMapper.Profile
    {
        public ProductMapper()
        {
            CreateMap<Data.Product, Models.Product>();
        }
    }
}
