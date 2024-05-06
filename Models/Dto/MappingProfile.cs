using AutoMapper;
using FuelPredictor.Models.V2;

namespace FuelPredictor.Models.Dto
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Station, StationDto>()
                .ForMember(dest => dest.IdVille, opt => opt.MapFrom(src => (int)src.IDVille))
                .ForMember(dest => dest.IdCompany, opt => opt.MapFrom(src => (int)src.IDCompany))
                .ForMember(dest => dest.Ville, opt => opt.MapFrom(src => src.Ville != null ? src.Ville.Name : null))
                .ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.Company != null ? src.Company.Nom : null))
               ;
        }
    }
}
