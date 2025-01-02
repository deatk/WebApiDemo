using AutoMapper;
using WebApiDemoModels;
using WebApiDemoModels.Requests;

namespace WebApiDemoModels.Mappings
{
    public class ContactMappingProfile : Profile
    {
        public ContactMappingProfile()
        {
            CreateMap<CreateContactRequest, Contact>();
            CreateMap<UpdateContactRequest, Contact>();
        }
    }
}