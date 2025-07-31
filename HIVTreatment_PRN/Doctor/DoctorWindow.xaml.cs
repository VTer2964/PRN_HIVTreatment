using BLL.Service;
using DAL.Entities;
using DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace HIVTreatment_PRN.Doctor
{
    public partial class DoctorWindow : Window
    {
        private DoctorService _doctorService = new();
        private List<User> _patients;
        private User? _selectedPatient;
        private User _doctor;

        public DoctorWindow(User doctor)
        {
            InitializeComponent();
            _doctor = doctor;
            txtWelcome.Text = $"Chào bác sĩ {_doctor.FullName}";
            LoadPatients();
        }

        private void LoadPatients()
        {
            using var context = new DAL.Entities.AppDbContext();
            _patients = context.Users
                .Where(u => u.Role == "Patient" && u.Status == "Active")
                .ToList();

            cbPatients.ItemsSource = _patients;
        }

        private void cbPatients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbPatients.SelectedItem is User patient)
            {
                _selectedPatient = patient;
                LoadPatientData(patient.UserId);
            }
        }

        private void LoadPatientData(int patientId)
        {
            // Hiển thị kết quả mới nhất
            var latestExam = _doctorService.GetLatestExam(patientId);
            if (latestExam != null)
            {
                txtCD4.Text = latestExam.Cd4Count.ToString();
                txtHIVLoad.Text = latestExam.HivLoad.ToString();
                txtExamDate.Text = latestExam.ExamDate?.ToString("dd/MM/yyyy") ?? "N/A";
                txtResult.Text = latestExam.Result ?? "Chưa có kết luận"; // ✅ Thêm dòng này
            }
            else
            {
                txtCD4.Text = txtHIVLoad.Text = txtExamDate.Text = txtResult.Text = "N/A";
            }

            // Lịch sử xét nghiệm
            dgExamHistory.ItemsSource = _doctorService.GetExamHistory(patientId);

            // Phác đồ hiện tại
            var protocol = _doctorService.GetCurrentProtocol(patientId);
            if (protocol != null)
            {
                txtProtocolName.Text = protocol.Name;
                txtProtocolDesc.Text = protocol.Description;
                dgCurrentProtocol.ItemsSource = protocol.CustomizedArvProtocolDetails;
            }
            else
            {
                txtProtocolName.Text = "Chưa có phác đồ";
                txtProtocolDesc.Text = "";
                dgCurrentProtocol.ItemsSource = null;
            }
        }

        private void BtnAddExam_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedPatient == null) return;

            var dialog = new AddExaminationDialog(_selectedPatient, _doctor);
            if (dialog.ShowDialog() == true)
            {
                _doctorService.AddExamination(dialog.Examination);
                LoadPatientData(_selectedPatient.UserId);
            }
        }

        private void BtnChangeProtocol_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedPatient == null) return;

            var dialog = new ChangeProtocolDialog(_selectedPatient, _doctor);
            if (dialog.ShowDialog() == true)
            {
                try
                {
                    _doctorService.ChangeProtocol(dialog.NewProtocol);
                    LoadPatientData(_selectedPatient.UserId);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi thay đổi phác đồ: {ex.Message}");
                }
            }
        }
    }
}
