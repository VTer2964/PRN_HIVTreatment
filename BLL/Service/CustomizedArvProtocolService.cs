using DAL.Entities;
using DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
{
    public class CustomizedArvProtocolService
    {
        private readonly CustomizedArvProtocolRepository _repo = new CustomizedArvProtocolRepository();

        public List<CustomizedArvProtocol> GetByDoctor(int doctorId) => _repo.GetByDoctorId(doctorId);

        public CustomizedArvProtocol? GetById(int id) => _repo.GetById(id);

        public void Create(CustomizedArvProtocol protocol) => _repo.Add(protocol);

        public void Update(CustomizedArvProtocol protocol) => _repo.Update(protocol);

        public void Delete(int id) => _repo.Delete(id);
    }
}