using AutoMapper;
using Museum.DataAccess.Entities;

namespace Museum.BusinessLogic.BusinessObjects.MapperProfiles
{
    public class ArtworksBOProfile : Profile
    {
        public ArtworksBOProfile()
        {
            CreateMap<Artwork, ArtworkBO>()
                .ForMember(dest =>
                    dest.Id,
                    opt => opt.MapFrom(src => src.Id)
                )
                .ForMember(dest =>
                    dest.Name,
                    opt => opt.MapFrom(src => src.Name)
                )
                .ForMember(dest =>
                    dest.Image,
                    opt => opt.MapFrom(src => src.Image)
                )
                .ForMember(dest =>
                    dest.Price,
                    opt => opt.MapFrom(src => src.Price)
                )
                .ForMember(dest =>
                    dest.Rating,
                    opt => opt.MapFrom(src => src.Rating)
                );
        }
    }
}
