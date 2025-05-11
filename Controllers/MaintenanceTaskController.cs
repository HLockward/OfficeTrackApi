using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OfficeTrackApi.DTOs;
using OfficeTrackApi.Entities;
using OfficeTrackApi.Repositories;

namespace OfficeTrackApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaintenanceTaskController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MaintenanceTaskController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<MaintenanceTaskDto>>> GetAll()
        {
            var maintenanceTasks = await _unitOfWork.MaintenanceTasks.GetAllAsync();
            var maintenanceTasksDtoList = _mapper.Map<IEnumerable<MaintenanceTaskDto>>(maintenanceTasks);
            return Ok(maintenanceTasksDtoList);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MaintenanceTaskDto>> GetById(int id)
        {
            var maintenanceTask = await _unitOfWork.MaintenanceTasks.GetByIdAsync(id);
            if (maintenanceTask == null)
                return NotFound();

            var maintenanceTaskDto = _mapper.Map<MaintenanceTaskDto>(maintenanceTask);
            return Ok(maintenanceTaskDto);
        }

        [HttpPost]
        public async Task<ActionResult<MaintenanceTaskDto>> Create([FromBody] CreateMaintenanceTaskDto dto)
        {
            var maintenanceTask = _mapper.Map<MaintenanceTask>(dto);
            await _unitOfWork.MaintenanceTasks.AddAsync(maintenanceTask);
            await _unitOfWork.SaveAsync();

            var maintenanceTaskDtoResult = _mapper.Map<MaintenanceTaskDto>(maintenanceTask);
            return CreatedAtAction(nameof(GetById), new { id = maintenanceTaskDtoResult.Id }, maintenanceTaskDtoResult);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateMaintenanceTaskDto dto)
        {
            var maintenanceTask = await _unitOfWork.MaintenanceTasks.GetByIdAsync(id);
            if (maintenanceTask == null)
                return NotFound();

            _mapper.Map(dto, maintenanceTask);
            _unitOfWork.MaintenanceTasks.Update(maintenanceTask);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var maintenanceTask = await _unitOfWork.MaintenanceTasks.GetByIdAsync(id);
            if (maintenanceTask == null)
                return NotFound();

            _unitOfWork.MaintenanceTasks.Delete(maintenanceTask);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }
    }
}
