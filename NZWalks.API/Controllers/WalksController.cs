using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Models.Domains;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repository;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IWalksRepository _walksRepository;
        private readonly ILogger<WalksController> logger;

        public WalksController(IMapper mapper, IWalksRepository walksRepository, ILogger<WalksController> logger)
        {
            _mapper = mapper;
            _walksRepository = walksRepository;
            this.logger = logger;
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody]WalksCreateDTO walksCreateDTO)
        {
            try
            {
                //if (!ModelState.IsValid)
                    //{
                    //    return BadRequest(ModelState);
                    //}

                // Map DTO to entity
                var walkDomainModel = _mapper.Map<Walk>(walksCreateDTO);

                walkDomainModel = await _walksRepository.CreateAsync(walkDomainModel);

                return Ok(_mapper.Map<WalksDTO>(walkDomainModel));
            }
            catch (Exception ex)
            {
                logger.LogInformation("Something went wrong", ex.Message);

                return Problem("Something went wrong", null, (int)HttpStatusCode.InternalServerError);
            }
        }

        // api/Walks?filterOn=Name&filterQuery=searchItem&sortBy=Name&isAscending=true&pageNumber=1&pageSize=10
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery]string? filterOn, [FromQuery]string? filterQuery
                                                ,[FromQuery]string? sortBy, bool? isAscending,
                                                  [FromQuery]int pageNumber = 1, [FromQuery] int pageSize = 1000)
        {
            var walksDomainModel = await _walksRepository.GetAllAsync(filterOn: filterOn, filterQuery: filterQuery,
                                                                      sortBy: sortBy, isAscending: true,
                                                                      pageNumber: pageNumber, pageSize: pageSize);

            return Ok(_mapper.Map<List<WalksDTO>>(walksDomainModel));
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute]Guid id)
        {
            var walksDomainModel = await _walksRepository.GetByIdAsync(x => x.Id == id);

            if(walksDomainModel == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<WalksDTO>(walksDomainModel));
        }


        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateById([FromRoute]Guid id, [FromBody]WalksUpdateDTO walksUpdateDTO)
        {

            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}
            //Map to entity

            var walksDomainModel = _mapper.Map<Walk>(walksUpdateDTO);

            walksDomainModel = await _walksRepository.UpdateAsync(id, walksDomainModel);

            if(walksDomainModel == null)
            {
                return null;
            }

            return Ok(_mapper.Map<WalksDTO>(walksDomainModel));
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteById([FromRoute]Guid id)
        {
            var deletedModel = await _walksRepository.DeleteAsync(id);

            if(deletedModel == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<WalksDTO>(deletedModel));
        }

    }
}
