using PlateformeService.Models;
using System.Collections.Generic;

namespace PlateformeService.Data
{
    public interface IPlateformRepository
    {
        IEnumerable<Plateform> GetAll();
        Plateform Get(int id);
        void Create(Plateform entity);
        void Update(Plateform entity);
        void Delete(int id);

    }
}
