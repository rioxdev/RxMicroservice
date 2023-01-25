using AutoMapper;
using CommandService.Data;
using CommandService.Dtos;
using CommandService.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text.Json;

namespace CommandService.EventProcessing
{
    public class EventProcessor : IEventProcessor
    {
        private readonly IMapper _mapper;
        private readonly IServiceScopeFactory _scopeFactory;

        public EventProcessor(IMapper mapper, IServiceScopeFactory scopeFactory)
        {
            _mapper = mapper;
            _scopeFactory = scopeFactory;
        }

        public void ProcessEvent(string message)
        {
            var eventType = DetermineEvent(message);

            switch(eventType)
            {
                case EventType.PlateformPublished:
                    AddPlateform(message);
                    break;
                default:
                    break;
            }
        }

        private EventType DetermineEvent(string notificationMessage)
        {
            Console.WriteLine("---> Determining event");

            var genericEventDto = JsonSerializer.Deserialize<GenericEventDto>(notificationMessage);

            switch (genericEventDto.Event)
            {
                case "Plateform_Published":
                    return EventType.PlateformPublished;

                default:
                    return EventType.Undetermined;
            }
        }

        private void AddPlateform(string message)
        {
            using(var scope = _scopeFactory.CreateScope())
            {
                var repository = scope.ServiceProvider.GetRequiredService<ICommandRepository>();
                var dto = JsonSerializer.Deserialize<PlateformPubDto>(message);

                var entity = _mapper.Map<Plateform>(dto);

                if (repository.ExternalPlateformExists(entity.PlateformId))
                    Console.WriteLine("-->Plateform already exists");
                else
                {
                    repository.CreatePlateform(entity);
                    Console.WriteLine($"-->new plateform added::{message}");

                }

            }
        }

    }

    public enum EventType
    {
        PlateformPublished,
        Undetermined
    }
}
