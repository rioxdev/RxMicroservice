using PlateformeService.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PlateformeService.Data
{
    public class PlateformRepository : IPlateformRepository
    {
        private readonly AppDbContext _context;

        public PlateformRepository(AppDbContext context)
        {
            _context = context;
        }
        public void Create(Plateform entity)
        {
            _context.Plateforms.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var entity = _context.Plateforms.Find(id);
            if (entity == null)
                throw new ArgumentNullException(nameof(Plateform));

            _context.Plateforms.Remove(entity);
            _context.SaveChanges();
        }

        public Plateform Get(int id)
        {
            return _context.Plateforms.Find(id);
        }

        public IEnumerable<Plateform> GetAll()
        {
            return _context.Plateforms.AsEnumerable();
        }

        public void Update(Plateform entity)
        {
            var existing = _context.Plateforms.Find(entity.Id);
            if (existing == null)
                throw new ArgumentNullException(nameof(Plateform));

            existing.Name = entity.Name;
            existing.Publisher = entity.Publisher;
            existing.Cost = entity.Cost;

            _context.SaveChanges();
        }
    }
}
