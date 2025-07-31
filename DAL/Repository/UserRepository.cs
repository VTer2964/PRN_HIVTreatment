using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class UserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository()
        {
            _context = new AppDbContext();
        }

        public User GetById(int userId)
        {
            return _context.Users.FirstOrDefault(u => u.UserId == userId);
        }

        public IEnumerable<User> GetPatientsByDoctor(int doctorId)
        {
            return _context.Examinations
                .Where(e => e.DoctorId == doctorId)
                .Select(e => e.Patient)
                .Distinct()
                .ToList();
        }
    }
}
