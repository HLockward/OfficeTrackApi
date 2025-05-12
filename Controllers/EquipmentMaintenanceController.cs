using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeTrackApi.DTOs;
using OfficeTrackApi.Entities;
using OfficeTrackApi.Repositories;

[ApiController]
[Route("api/equipment/{equipmentId}/maintenances")]
public class EquipmentMaintenanceController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public EquipmentMaintenanceController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<EquipmentMaintenanceDto>> GetMaintenanceByEquipmentId(int equipmentId)
    {
        var equipment = await _unitOfWork.Equipment.FindSingleAsync(
            e => e.Id == equipmentId,
            q => q
                .Include(e => e.EquipmentType)
                .Include(e => e.EquipmentMaintenances)
                .ThenInclude(em => em.MaintenanceTask)
        );

        if (equipment is null)
            return NotFound($"Equipment with ID {equipmentId} not found.");

        var dto = _mapper.Map<EquipmentMaintenanceDto>(equipment);
        return Ok(dto);
    }

    [HttpPost]
    public async Task<ActionResult> AssignMaintenanceToEquipment(int equipmentId, [FromBody] AssignMaintenanceDto dto)
    {
        var equipment = await _unitOfWork.Equipment.GetByIdAsync(equipmentId);
        if (equipment is null)
            return NotFound($"Equipment with ID {equipmentId} not found.");

        var task = await _unitOfWork.MaintenanceTasks.GetByIdAsync(dto.MaintenanceTaskId);
        if (task is null)
            return NotFound($"MaintenanceTask with ID {dto.MaintenanceTaskId} not found.");

        var exists = await _unitOfWork.EquipmentMaintenances.ExistsAsync(em =>
            em.EquipmentId == equipmentId && em.MaintenanceTaskId == dto.MaintenanceTaskId);

        if (exists)
            return Conflict("The maintenance task is already assigned to this equipment.");

        var equipmentMaintenance = new EquipmentMaintenance
        {
            EquipmentId = equipmentId,
            MaintenanceTaskId = dto.MaintenanceTaskId
        };

        await _unitOfWork.EquipmentMaintenances.AddAsync(equipmentMaintenance);
        await _unitOfWork.SaveAsync();

        return CreatedAtAction(nameof(GetMaintenanceByEquipmentId), new { equipmentId }, null);
    }

    [HttpDelete("{maintenanceTaskId}")]
    public async Task<IActionResult> RemoveMaintenanceFromEquipment(int equipmentId, int maintenanceTaskId)
    {
        var relation = await _unitOfWork.EquipmentMaintenances.FindSingleAsync(
            em => em.EquipmentId == equipmentId && em.MaintenanceTaskId == maintenanceTaskId
        );

        if (relation is null)
            return NotFound("The maintenance task is not assigned to this equipment.");

        _unitOfWork.EquipmentMaintenances.Delete(relation);
        await _unitOfWork.SaveAsync();

        return NoContent();
    }

}
