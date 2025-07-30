using DAL.Entities;
using DAL.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
{
    public class ArvProtocolService
    {
        private readonly ArvProtocolRepository _repo = new ArvProtocolRepository();

        public List<ArvProtocol> GetAll()
        {
            // Tạo và giải phóng context ngay trong phương thức
            using (var context = new AppDbContext())
            {
                return context.ArvProtocols
                       .Include(p => p.ArvProtocolDetails)
                       .ThenInclude(d => d.Arv)
                       .AsNoTracking()
                       .ToList();
            }
        }

        public ArvProtocol? GetById(int id) => _repo.GetById(id);

        public void Create(ArvProtocol protocol) => _repo.Add(protocol);

        public void Update(ArvProtocol protocol) => _repo.Update(protocol);

        public void Delete(int id) => _repo.Delete(id);
        public bool UpdateProtocolWithDetails(ArvProtocol protocol, List<ArvProtocolDetail> details)
        => _repo.UpdateProtocolWithDetails(protocol, details);

        public int SaveProtocolWithDetails(ArvProtocol protocol, List<ArvProtocolDetail> details)
            => _repo.SaveProtocolWithDetails(protocol, details);
        public ArvProtocol? GetProtocolWithDetails(int protocolId)
        {
            return _repo.GetProtocolWithDetails(protocolId);
        }

    }
}