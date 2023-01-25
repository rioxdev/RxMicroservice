using AutoMapper;
using PlateformeService.Dtos;
using PlateformeService.Models;

namespace PlateformeService.Profiles
{
    public class PlateformsProfile : Profile
    {
        public PlateformsProfile()
        {
            CreateMap<Plateform, PlateformReadDto>();
            CreateMap<PlateformCreateDto, Plateform>();
            CreateMap<PlateformReadDto, PlateformPubDto>();
        }
    }
}
