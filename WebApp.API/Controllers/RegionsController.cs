using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebApp.API.Data;
using WebApp.API.Models.Domain;
using WebApp.API.Models.DTO;
using WebApp.API.Repositories;

namespace WebApp.API.Controllers
{
	// https://localhost:1234/api/regions
	[Route("api/[controller]")]
	[ApiController]
	public class RegionsController : ControllerBase
	{
		private readonly IRegionRepository regionRepository;
		private readonly IMapper mapper;

		public RegionsController(IRegionRepository regionRepository,IMapper mapper)
		{
			this.regionRepository = regionRepository;
			this.mapper = mapper;
		}

		// GET ALL REGIONS
		// GET: https://localhost:portnumber/api/regions
		[HttpGet]
		[Authorize(Roles = "Reader,Writer")]
		public async Task<IActionResult> GetAll()
		{
			// Lấy dữ liệu từ Database - (Domain models)
			var regionsDomain = await regionRepository.GetAllAsync();

			// Ánh xạ các phần tử trong Domain Models sang DTOs
			var regionsDto = mapper.Map<List<RegionDto>>(regionsDomain);

			// Return DTOs
			return Ok(regionsDto);
		}

		// GET SINGLE REGION (Get Region By ID)
		// GET: https://localhost:portnumber/api/regions/{id}
		[HttpGet]
		[Route("{id:Guid}")]
		//[Authorize(Roles = "Reader,Writer")]

		public async Task<IActionResult> GetById([FromRoute] Guid id)
		{
			// Get Region Domain Model From Database
			var regionDomain = await regionRepository.GetByIdAsync(id);

			if (regionDomain == null)
			{
				return NotFound();
			}

			// Ánh xạ phần tử trong Domain Models sang DTOs
			var regionDto = mapper.Map<RegionDto>(regionDomain);

			// Return DTO back to client
			return Ok(regionDto);
		}

		// POST To Create New Region
		// POST: https://localhost:portnumber/api/regions
		//[Authorize(Roles = "Writer")]

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] AddNewRegionRequestDto addRegionRequestDto)
		{
			// Map or Convert DTO to Domain Model
			var regionDomainModel = mapper.Map<Region>(addRegionRequestDto);

			// Sử dụng Domain Model để tạo mới Region
			await regionRepository.CreateAsync(regionDomainModel);

			// Map Domain model về DTO
			var regionDto = mapper.Map<RegionDto>(regionDomainModel);

			return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
		}


		// Update region
		// PUT: https://localhost:portnumber/api/regions/{id}
		[HttpPut]
		[Route("{id:Guid}")]
		//[Authorize(Roles = "Writer")]

		public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
		{
			// Map or Convert DTO to Domain Model
			var regionDomainModel = mapper.Map<Region>(updateRegionRequestDto);
			
			// Kiểm tra Region có tồn tại
			if (regionDomainModel==null)
			{
				return NotFound();
			}
			regionDomainModel = await regionRepository.UpdateAsync(id, updateRegionRequestDto);

			// Return DTO back to client
			return Ok(mapper.Map<RegionDto>(regionDomainModel));
		}

		// Delete Region
		// DELETE: https://localhost:portnumber/api/regions/{id}
		[HttpDelete]
		[Route("{id:Guid}")]
		//[Authorize(Roles = "Writer")]

		public async Task<IActionResult> Delete([FromRoute] Guid id)
		{
			var regionDomainModel = await regionRepository.DeleteAsync(id);
			return Ok(mapper.Map<RegionDto>(regionDomainModel));
		}
	}
}
