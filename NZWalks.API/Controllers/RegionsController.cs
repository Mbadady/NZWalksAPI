using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Data;
using NZWalks.API.Models.Domains;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repository;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext _dbContext;
        private readonly IRegionRepository _regionRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<RegionsController> logger;

        public RegionsController(NZWalksDbContext dbContext, IRegionRepository regionRepository
            ,IMapper mapper, ILogger<RegionsController> logger)
        {
            _dbContext = dbContext;
            _regionRepository = regionRepository;
            _mapper = mapper;
            this.logger = logger;
        }

        [HttpGet]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetAll()
        {
            logger.LogInformation("GetAllRegions Action Method was Invoked");
            //var regions = new List<Region>()
            //{
            //    new Region()
            //    {
            //        Id = Guid.NewGuid(),
            //        Name = "Auckland",
            //        Code = "AKL",
            //        RegionImageUrl = ""
            //    },
            //    new Region()
            //    {
            //        Id = Guid.NewGuid(),
            //        Name = "Wellignton",
            //        Code = "WL",
            //        RegionImageUrl = ""
            //    },
            //};
            //var regionsDomain = await _dbContext.Regions.ToListAsync();

            IEnumerable<Region> regionsDomain = await _regionRepository.GetAllAsync();

            //var regionsDTO = new List<RegionDTO>();

            //foreach (var regionDomain in regionsDomain)
            //{
            //    regionsDTO.Add(new RegionDTO()
            //    {
            //        Id = regionDomain.Id,
            //        Code = regionDomain.Code,
            //        Name = regionDomain.Name,
            //        RegionImageUrl = regionDomain.RegionImageUrl
            //    });
            //}

            logger.LogInformation($"Finished GetAllRegions with data: {JsonSerializer.Serialize(regionsDomain)}");

            var regionsDTO = _mapper.Map<List<RegionDTO>>(regionsDomain);

            return Ok(regionsDTO);
        }

        [HttpGet("{id:Guid}")]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var regionDomain = await _regionRepository.GetByIdAsync(id);

            if (regionDomain == null)
            {
                return NotFound();
            }

            //var regionDTO = new RegionDTO()
            //{
            //    Id = regionDomain.Id,
            //    Code = regionDomain.Code,
            //    Name = regionDomain.Name,
            //    RegionImageUrl = regionDomain.RegionImageUrl
            //};
            var regionDTO = _mapper.Map<RegionDTO>(regionDomain);

            return Ok(regionDTO);
        }

        [HttpPost(Name = "CreateRegion")]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> CreateRegion([FromBody] RegionCreateDTO regionCreateDTO)
        {
            // Convert the DTO to a Domain Model

            //var regionDomainModel = new Region()
            //{
            //    Code = regionCreateDTO.Code,
            //    Name = regionCreateDTO.Name,
            //    RegionImageUrl = regionCreateDTO.RegionImageUrl
            //};

            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            var regionDomainModel = _mapper.Map<Region>(regionCreateDTO);

            regionDomainModel = await _regionRepository.CreateAsync(regionDomainModel);

            // Convert back to DTO so it can be returned

            //var regionDto = new RegionDTO()
            //{
            //    RegionImageUrl = regionDomainModel.RegionImageUrl,
            //    Code = regionDomainModel.Code,
            //    Name = regionDomainModel.Name,
            //    Id = regionDomainModel.Id
            //};

            var regionDto = _mapper.Map<RegionDTO>(regionDomainModel);

            return CreatedAtAction(nameof(GetById), new { id = regionDomainModel.Id }, regionDto);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> UpdateRegion([FromRoute] Guid id, [FromBody] RegionUpdateDTO regionUpdateDTO)
        {
            //var regionDomainModel = new Region()
            //{
            //    Code = regionUpdateDTO.Code,
            //    RegionImageUrl = regionUpdateDTO.RegionImageUrl,
            //    Name = regionUpdateDTO.Name
            //};

            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            var regionDomainModel = _mapper.Map<Region>(regionUpdateDTO);

            regionDomainModel = await _regionRepository.UpdateAsync(id, regionDomainModel);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            //var regionDTO = new RegionDTO()
            //{
            //    Code = regionDomainModel.Code,
            //    Id = regionDomainModel.Id,
            //    Name = regionDomainModel.Name,
            //    RegionImageUrl = regionDomainModel.RegionImageUrl
            //};

            var regionDTO = _mapper.Map<RegionDTO>(regionDomainModel);

            return Ok(regionDTO);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> DeleteRegion([FromRoute]Guid id)
        {
            var regionDomainModel = await _regionRepository.DeleteAsync(id);

            if(regionDomainModel == null)
            {
                return NotFound();
            }

            //var regionDTO = new RegionDTO()
            //{
            //    Code = regionDomainModel.Code,
            //    Id = regionDomainModel.Id,
            //    Name = regionDomainModel.Name,
            //    RegionImageUrl = regionDomainModel.RegionImageUrl
            //};

            var regionDTO = _mapper.Map<RegionDTO>(regionDomainModel);

            return Ok(regionDTO);
        }
    }

}
