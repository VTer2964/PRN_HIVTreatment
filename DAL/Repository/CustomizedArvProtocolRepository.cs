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
        private readonly AppDbContext _context = new();

        public CustomizedArvProtocol? GetCurrentByPatientId(int patientId)
        {
            return _context.CustomizedArvProtocols
                .Include(p => p.CustomizedArvProtocolDetails)
                    .ThenInclude(d => d.Arv)
                .FirstOrDefault(p => p.PatientId == patientId && p.Status == "Active");
        }

        public void AssignNewProtocol(CustomizedArvProtocol protocol)
        {
            // Hủy kích hoạt các phác đồ cũ
            var oldProtocols = _context.CustomizedArvProtocols
                .Where(p => p.PatientId == protocol.PatientId && p.Status == "Active")
                .ToList();

            foreach (var old in oldProtocols)
                old.Status = "Inactive";

            _context.CustomizedArvProtocols.Add(protocol);
            _context.SaveChanges();
        }
    }
}
