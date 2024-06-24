using AutoMapper;
using monoProjekt.Models;
using Projekt.Sevice.DatabaseModels;


namespace monoProjekt.Mapping
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<VehicleMake, VehicleMakeDto>().ReverseMap();
            CreateMap<VehicleModel, VehicleModelDto>().ReverseMap();
        }
    }
}
