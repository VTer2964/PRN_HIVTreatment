using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class ArvProtocolRepository
    {
        private readonly AppDbContext _context;

        public ArvProtocolRepository() => _context = new AppDbContext();

        public List<ArvProtocol> GetAll() =>
            _context.ArvProtocols.Include(p => p.ArvProtocolDetails).ThenInclude(d => d.Arv).ToList();

        public ArvProtocol? GetById(int id) =>
            _context.ArvProtocols.Include(p => p.ArvProtocolDetails).ThenInclude(d => d.Arv)
                     .FirstOrDefault(p => p.ProtocolId == id);

        public void Add(ArvProtocol protocol)
        {
            _context.ArvProtocols.Add(protocol);
            _context.SaveChanges();
        }

        public void Update(ArvProtocol protocol)
        {
            _context.ArvProtocols.Update(protocol);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var protocol = _context.ArvProtocols.Find(id);
            if (protocol != null)
            {
                _context.ArvProtocols.Remove(protocol);
                _context.SaveChanges();
            }
        }
        public bool UpdateProtocolWithDetails(ArvProtocol protocol, List<ArvProtocolDetail> newDetails)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var existingProtocol = _context.ArvProtocols
                    .Include(p => p.ArvProtocolDetails)
                    .FirstOrDefault(p => p.ProtocolId == protocol.ProtocolId);

                if (existingProtocol == null)
                    return false;

                // Update protocol properties
                existingProtocol.Name = protocol.Name;
                existingProtocol.Description = protocol.Description;
                existingProtocol.Status = protocol.Status;

                // Remove existing details
                _context.ArvProtocolDetails.RemoveRange(existingProtocol.ArvProtocolDetails);

                // Add new details
                foreach (var detail in newDetails)
                {
                    detail.ProtocolId = protocol.ProtocolId;
                    _context.ArvProtocolDetails.Add(detail);
                }

                // Save all changes
                _context.SaveChanges();
                transaction.Commit(); // Quan trọng: Phải commit transaction
                return true;
            }
            catch (Exception ex)
            {
                transaction.Rollback(); // Rollback nếu có lỗi
                Console.WriteLine($"Error updating protocol with details: {ex.Message}");
                return false;
            }
        }

        // ✅ Sửa lại cho khớp với CreateNewProtocol
        public int SaveProtocolWithDetails(ArvProtocol protocol, List<ArvProtocolDetail> details)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                // Add protocol (this will generate ProtocolId)
                _context.ArvProtocols.Add(protocol);
                _context.SaveChanges(); // Lưu để lấy ID

                // Add details with the generated ProtocolId
                foreach (var detail in details)
                {
                    detail.ProtocolId = protocol.ProtocolId;
                    _context.ArvProtocolDetails.Add(detail);
                }

                _context.SaveChanges();
                transaction.Commit(); // Quan trọng
                return protocol.ProtocolId;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                Console.WriteLine($"Lỗi khi lưu: {ex.Message}");
                return -1;
            }
        }
        public ArvProtocol? GetProtocolWithDetails(int protocolId)
        {
            return _context.ArvProtocols
                .Include(p => p.ArvProtocolDetails)
                .ThenInclude(d => d.Arv)
                .FirstOrDefault(p => p.ProtocolId == protocolId);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

    }
}