using AutoMapper;
using OfficeTrackApi.DTOs;
using OfficeTrackApi.Entities;

namespace OfficeTrackApi.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Equipment, EquipmentDto>().ForMember(dest => dest.EquipmentTypeDescription,
                opt => opt.MapFrom(src => src.EquipmentType.Description));
        CreateMap<EquipmentDto, Equipment>();
        CreateMap<CreateEquipmentDto, Equipment>();
        CreateMap<UpdateEquipmentDto, Equipment>();
        CreateMap<MaintenanceTask, MaintenanceTaskDto>().ReverseMap();
        CreateMap<CreateMaintenanceTaskDto, MaintenanceTask>();
        CreateMap<UpdateMaintenanceTaskDto, MaintenanceTask>();
    }
}
