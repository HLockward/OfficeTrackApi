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

        CreateMap<Equipment, EquipmentMaintenanceDto>()
            .ForMember(dest => dest.EquipmentId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.EquipmentType, opt => opt.MapFrom(src => src.EquipmentType.Description))
            .ForMember(dest => dest.MaintenanceTasks, opt => opt.MapFrom(src => src.EquipmentMaintenances
                .Select(em => new MaintenanceTaskDto
                {
                    Id = em.MaintenanceTask.Id,
                    Description = em.MaintenanceTask.Description,
                })));
    }
}
