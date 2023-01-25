using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlateformeService.AsyncDataServices;
using PlateformeService.Data;
using PlateformeService.Dtos;
using PlateformeService.Models;
using PlateformeService.SyncDataServices.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlateformeService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlateformsController : ControllerBase
    {

        private readonly IPlateformRepository _repository;
        private readonly IMapper _mapper;
        private readonly ICommandDataClient _commandDataClient;
        private readonly IMessageBusClient _messageBusClient;

        public PlateformsController(IPlateformRepository repository, IMapper mapper, ICommandDataClient commandDataClient, IMessageBusClient messageBusClient)
        {
            _repository = repository;
            _mapper = mapper;
            _commandDataClient = commandDataClient;
            _messageBusClient = messageBusClient;
        }

        [HttpGet]
        public ActionResult<List<PlateformReadDto>> GetAll()
        {
            var entities = _repository.GetAll().ToList();
            var dtos = _mapper.Map<List<PlateformReadDto>>(entities);

            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public ActionResult<PlateformReadDto> Get(int id)
        {
            var entity = _repository.Get(id);
            if (entity == null)
                return NotFound();

            return Ok(_mapper.Map<PlateformReadDto>(entity));
        }

        [HttpPost]
        public async Task<ActionResult<PlateformReadDto>> Create(PlateformCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var entity = _mapper.Map<Plateform>(dto);
            _repository.Create(entity);

            var result = _mapper.Map<PlateformReadDto>(entity);

            try
            {
                await _commandDataClient.SendPlateformToCommand(result);
                Console.WriteLine("--> sync message sent");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            try
            {
                var pubDto = _mapper.Map<PlateformPubDto>(result);
                pubDto.Event = "Plateform_Published";
                _messageBusClient.PublishNewPlatform(pubDto);

                Console.WriteLine("--> async message sent");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return CreatedAtAction(nameof(Get), new { id = entity.Id },  result);
        }

    }
}
