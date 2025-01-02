using AutoMapper;
using WebApiDemoModels;
using WebApiDemoModels.Requests;

namespace WebApiDemoModels.Mappings
{
    public class OrderMappingProfile : Profile
    {
        public OrderMappingProfile()
        {
            CreateMap<CreateOrderRequest, Order>();
            CreateMap<UpdateOrderRequest, Order>();
        }
    }
}