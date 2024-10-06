using AutoMapper;
using GameZone.Enities.Models;
using GameZone.Web.ViewModels;

namespace GameZone.Web.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<CreateGameFormViewModel, Game>().ForMember(dest => dest.Cover, opt => opt.Ignore());

            CreateMap<Game, EditGameViewModel>().ForMember(dest => dest.Cover, opt => opt.Ignore()).
                ForMember(dest => dest.Categories, opt => opt.Ignore()).
                ForMember(dest => dest.SelectedDevices, opt => opt.Ignore()).
                ForMember(dest => dest.Devices, opt => opt.Ignore());

            CreateMap<EditGameViewModel, Game>().ForMember(dest => dest.Cover, opt => opt.Ignore()).
                ForMember(dest => dest.Devices, opt => opt.Ignore());

        }
    }
}
