using AutoMapper;
using CommandService.Data;
using CommandService.Dtos;
using CommandService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace CommandService.Controllers
{
    [Route("api/c/plateforms/{plateformId}/commands")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly ICommandRepository _repository;
        private readonly IMapper _mapper;

        public CommandsController(ICommandRepository repositor, IMapper mapper)
        {
            _repository = repositor;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult GetCommands(int plateformId)
        {
            Console.WriteLine($"--> Dans GetCommands, {plateformId}");

            if (_repository.GetPlateform(plateformId) == null)
                return NotFound();

            var commands = _repository.GetCommands(plateformId);
            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commands));
        }

        [HttpGet("{commandId}",Name = "GetCommand")]
        public ActionResult GetCommand(int plateformId, int commandId)
        {
            Console.WriteLine($"--> Dans GetCommand, {plateformId}/{commandId}");

            if (_repository.GetPlateform(plateformId) == null)
                return NotFound();

            var command = _repository.GetCommand(plateformId, commandId);

            return Ok(_mapper.Map<CommandReadDto>(command));
        }

        [HttpPost]
        public ActionResult Create(int plateformId, CommandCreateDto commandCreateDto)
        {
            Console.WriteLine($"--> Dans Create, {plateformId}/{commandCreateDto.HowTo}");

            if (_repository.GetPlateform(plateformId) == null)
                return NotFound();

            var command = _mapper.Map<Command>(commandCreateDto);
            _repository.Create(plateformId, command);

            return CreatedAtRoute("GetCommand", new { plateformId = plateformId, commandId = command.Id },
                _mapper.Map<CommandReadDto>(command));
        }

    }
}
