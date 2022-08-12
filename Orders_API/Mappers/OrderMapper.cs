namespace Orders_API.Mappers
{
    public class OrderMapper : AutoMapper.Profile
    {
        public OrderMapper()
        {
            CreateMap<Data.Order, Models.Order>();
            CreateMap<Data.OrderItem, Models.OrderItem>();
        }
    }
}
