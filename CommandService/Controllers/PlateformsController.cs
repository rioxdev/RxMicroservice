using AutoMapper;
using CommandService.Data;
using CommandService.Dtos;
using CommandService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CommandService.Controllers
{
    [Route("api/c/[controller]")]
    [ApiController]
    public class PlateformsController : ControllerBase
    {

        private ICommandRepository _repository;
        private IMapper _mapper;

        public PlateformsController(ICommandRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<PlateformReadDto> GetAll()
        {
            Console.WriteLine("--> PlateformsController :: GetAll");
            var entities = _repository.GetPlateforms().ToList();

            return Ok(_mapper.Map<List<PlateformReadDto>>(entities));
        }


        [HttpPost]
        public ActionResult TestInboundConnection()
        {
            Console.WriteLine(" --> inbound  Post # Commmand service");

            return Ok("Inbound test from PlateformController ");
        }

    }
}
