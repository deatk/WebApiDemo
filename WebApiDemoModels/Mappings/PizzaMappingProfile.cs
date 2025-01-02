using AutoMapper;
using WebApiDemoModels;
using WebApiDemoModels.Requests;

namespace WebApiDemoModels.Mappings
{
    public class PizzaMappingProfile : Profile
    {
        public PizzaMappingProfile()
        {
            CreateMap<CreatePizzaRequest, Pizza>();
            CreateMap<UpdatePizzaRequest, Pizza>();
        }
    }
}