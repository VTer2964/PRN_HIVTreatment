using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class CustomizedArvProtocolRepository
    {
        private readonly AppDbContext _context;

        public CustomizedArvProtocolRepository() => _context = new AppDbContext();

        public List<CustomizedArvProtocol> GetByDoctorId(int doctorId)
        {
            return _context.CustomizedArvProtocols
                .Include(p => p.CustomizedArvProtocolDetails)
                .ThenInclude(d => d.Arv)
                .Where(p => p.DoctorId == doctorId)
                .ToList();
        }

        public CustomizedArvProtocol? GetById(int id)
        {
            return _context.CustomizedArvProtocols
                .Include(p => p.CustomizedArvProtocolDetails)
                .ThenInclude(d => d.Arv)
                .FirstOrDefault(p => p.CustomProtocolId == id);
        }

        public void Add(CustomizedArvProtocol protocol)
        {
            _context.CustomizedArvProtocols.Add(protocol);
            _context.SaveChanges();
        }

        public void Update(CustomizedArvProtocol protocol)
        {
            _context.CustomizedArvProtocols.Update(protocol);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var entity = _context.CustomizedArvProtocols.Find(id);
            if (entity != null)
            {
                _context.CustomizedArvProtocols.Remove(entity);
                _context.SaveChanges();
            }
        }
    }
}
