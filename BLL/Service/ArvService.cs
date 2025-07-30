using DAL.Entities;
using DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
{
    public class ArvService
    {
        private readonly ArvRepository _repo = new ArvRepository();

        public List<Arv> GetAll() => _repo.GetAll();

        public Arv? GetById(int id) => _repo.GetById(id);

        public void Create(Arv arv) => _repo.Add(arv);

        public void Update(Arv arv) => _repo.Update(arv);

        public void Delete(int id) => _repo.Delete(id);
    }
}