using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeTrackApi.DTOs;
using OfficeTrackApi.Entities;
using OfficeTrackApi.Repositories;

namespace OfficeTrackApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipmentController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EquipmentController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<EquipmentDto>>> GetAll()
        {
            var equipments = await _unitOfWork.Equipment.GetAllAsync(q => q.Include(e => e.EquipmentType));
            var equipmentDtoList = _mapper.Map<List<EquipmentDto>>(equipments);
            return Ok(equipmentDtoList);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EquipmentDto>> GetById(int id)
        {
            var equipment = await _unitOfWork.Equipment.GetByIdAsync(id, q => q.Include(e => e.EquipmentType));
            if (equipment == null) return NotFound();

            var equipmentDto = _mapper.Map<EquipmentDto>(equipment);
            return Ok(equipmentDto);
        }

        [HttpPost]
        public async Task<ActionResult<EquipmentDto>> Create([FromBody] CreateEquipmentDto dto)
        {
            var equipmentEntity = _mapper.Map<Equipment>(dto);
            await _unitOfWork.Equipment.AddAsync(equipmentEntity);
            await _unitOfWork.SaveAsync();

            var equipmentDtoResult = _mapper.Map<EquipmentDto>(equipmentEntity);
            return CreatedAtAction(nameof(GetById), new { id = equipmentDtoResult.Id }, equipmentDtoResult);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateEquipmentDto dto)
        {
            var equipment = await _unitOfWork.Equipment.GetByIdAsync(id);
            if (equipment == null) return NotFound();

            _mapper.Map(dto, equipment);
            _unitOfWork.Equipment.Update(equipment);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var equipment = await _unitOfWork.Equipment.GetByIdAsync(id);
            if (equipment == null) return NotFound();

            _unitOfWork.Equipment.Delete(equipment);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }
    }
}
