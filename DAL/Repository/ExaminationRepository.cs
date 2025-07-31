using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class ExaminationRepository
    {
        private readonly AppDbContext _context = new();

        public List<Examination> GetByPatientId(int patientId)
        {
            return _context.Examinations
                .Where(e => e.PatientId == patientId)
                .OrderByDescending(e => e.ExamDate)
                .ToList();
        }

        public Examination? GetLatestByPatientId(int patientId)
        {
            return _context.Examinations
                .Where(e => e.PatientId == patientId)
                .OrderByDescending(e => e.ExamDate)
                .FirstOrDefault();
        }

        public void Add(Examination exam)
        {
            _context.Examinations.Add(exam);
            _context.SaveChanges();
        }
    }
}
