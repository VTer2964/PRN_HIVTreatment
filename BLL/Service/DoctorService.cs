using DAL.Entities;
using DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
{
    public class DoctorService
    {
        private readonly ExaminationRepository _examRepo = new();
        private readonly CustomizedArvProtocolRepository _protocolRepo = new();

        public List<Examination> GetExamHistory(int patientId)
        {
            return _examRepo.GetByPatientId(patientId);
        }

        public Examination? GetLatestExam(int patientId)
        {
            return _examRepo.GetLatestByPatientId(patientId);
        }

        public void AddExamination(Examination exam)
        {
            _examRepo.Add(exam);
        }

        public CustomizedArvProtocol? GetCurrentProtocol(int patientId)
        {
            return _protocolRepo.GetCurrentByPatientId(patientId);
        }

        public void ChangeProtocol(CustomizedArvProtocol newProtocol)
        {
            _protocolRepo.AssignNewProtocol(newProtocol);
        }
    }
}