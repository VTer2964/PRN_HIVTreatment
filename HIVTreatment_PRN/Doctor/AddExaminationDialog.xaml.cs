using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HIVTreatment_PRN.Doctor
{
    public partial class AddExaminationDialog : Window
    {
        private User _patient;
        private User _doctor;

        public Examination Examination { get; private set; }

        public AddExaminationDialog(User patient, User doctor)
        {
            InitializeComponent();
            _patient = patient;
            _doctor = doctor;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(txtCD4.Text, out int cd4) ||
                !int.TryParse(txtHIVLoad.Text, out int hiv))
            {
                MessageBox.Show("Vui lòng nhập số hợp lệ cho CD4 và HIV Load.");
                return;
            }

            Examination = new Examination
            {
                PatientId = _patient.UserId,
                DoctorId = _doctor.UserId,
                ExamDate = DateOnly.FromDateTime(DateTime.Now),
                Cd4Count = cd4,
                HivLoad = hiv,
                Result = txtResult.Text,
                Status = "Active"
            };

            DialogResult = true;
            Close();
        }
    }
}
