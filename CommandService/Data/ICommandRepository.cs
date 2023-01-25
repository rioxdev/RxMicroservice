using CommandService.Models;
using System.Collections.Generic;

namespace CommandService.Data
{
    public interface ICommandRepository
    {
        Command GetCommand(int plateformId,int id);
        IEnumerable<Command> GetCommands(int plateformId);
        IEnumerable<Plateform> GetPlateforms();
        Command Create(int plateformId, Command command);
        void CreatePlateform(Plateform plateform); 
        Plateform GetPlateform(int plateformId);

        bool ExternalPlateformExists(int plateformId);
    }
}
