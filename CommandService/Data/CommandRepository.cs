using CommandService.Models;
using System.Collections.Generic;
using System.Linq;

namespace CommandService.Data
{
    public class CommandRepository : ICommandRepository
    {
        private readonly AppDbContext _context;

        public CommandRepository(AppDbContext context)
        {
            _context = context;
        }
        public Command Create(int plateformId, Command command)
        {
            command.PlateformId = plateformId;
            _context.Commands.Add(command);
            _context.SaveChanges();

            return command;
        }

        public void CreatePlateform(Plateform plateform)
        {
            _context.Plateforms.Add(plateform);
            _context.SaveChanges();
        }

        public bool ExternalPlateformExists(int plateformId)
        {
            return _context.Plateforms.Any(p => p.PlateformId == plateformId);
        }

        public Command GetCommand(int plateformId, int id)
        {
            return _context.Commands.SingleOrDefault(c => c.PlateformId == plateformId && c.Id == id);
        }

        public IEnumerable<Command> GetCommands(int plateformId)
        {
            return _context.Commands.Where(c => c.PlateformId == plateformId);
        }

        public Plateform GetPlateform(int plateformId)
        {
            return _context.Plateforms.Find(plateformId);
        }

        public IEnumerable<Plateform> GetPlateforms()
        {
            return _context.Plateforms.AsEnumerable();
        }


    }
}
