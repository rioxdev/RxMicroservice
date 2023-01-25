using PlateformeService.Dtos;
using System.Threading.Tasks;

namespace PlateformeService.SyncDataServices.Http
{
    public interface ICommandDataClient
    {
        Task SendPlateformToCommand(PlateformReadDto dto);
    }
}
