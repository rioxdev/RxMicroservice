using PlateformeService.Dtos;

namespace PlateformeService.AsyncDataServices
{
    public interface IMessageBusClient
    {
        void PublishNewPlatform(PlateformPubDto dto);
    }
}
