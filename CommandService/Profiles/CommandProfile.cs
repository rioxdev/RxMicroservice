using AutoMapper;
using CommandService.Dtos;
using CommandService.Models;

namespace CommandService.Profiles
{
    public class CommandProfile : Profile
    {
        public CommandProfile()
        {
            CreateMap<Plateform, PlateformReadDto>();
            CreateMap<Command, CommandReadDto>();
            CreateMap<CommandCreateDto, Command>();
            CreateMap<PlateformPubDto, Plateform>()
                .ForMember(dest => dest.PlateformId, opt => opt.MapFrom(src => src.Id));
        }
    }
}
