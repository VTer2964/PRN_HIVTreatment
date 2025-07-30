using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class ArvRepository
    {
        private readonly AppDbContext _context;

        public ArvRepository() => _context = new AppDbContext();

        public List<Arv> GetAll() => _context.Arvs.ToList();

        public Arv? GetById(int id) => _context.Arvs.Find(id);

        public void Add(Arv arv)
        {
            _context.Arvs.Add(arv);
            _context.SaveChanges();
        }

        public void Update(Arv arv)
        {
            _context.Arvs.Update(arv);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var entity = _context.Arvs.Find(id);
            if (entity != null)
            {
                _context.Arvs.Remove(entity);
                _context.SaveChanges();
            }
        }
    }
}
