using BLL.Service;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
    public partial class ChangeProtocolDialog : Window
    {
        private readonly ArvProtocolService _protocolService = new();
        private readonly User _patient;
        private readonly User _doctor;
        private List<ArvProtocol> _allProtocols = new();
        private List<CustomizedArvProtocolDetail> _customizedDetails = new();

        public CustomizedArvProtocol NewProtocol { get; private set; }

        public ChangeProtocolDialog(User patient, User doctor)
        {
            InitializeComponent();
            _patient = patient;
            _doctor = doctor;
            LoadProtocols();
        }

        private void LoadProtocols()
        {
            _allProtocols = _protocolService.GetAll();
            cbProtocols.ItemsSource = _allProtocols;
        }

        private void cbProtocols_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbProtocols.SelectedItem is ArvProtocol selected)
            {
                txtDesc.Text = selected.Description;

                _customizedDetails = selected.ArvProtocolDetails.Select(d => new CustomizedArvProtocolDetail
                {
                    ArvId = d.ArvId,
                    Dosage = d.Dosage,
                    UsageInstruction = d.UsageInstruction,
                    Status = d.Status
                }).ToList();

                dgArvDetails.ItemsSource = null;
                dgArvDetails.ItemsSource = _customizedDetails;
            }
        }

        private void BtnConfirm_Click(object sender, RoutedEventArgs e)
        {
            if (_customizedDetails == null || _customizedDetails.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một phác đồ.");
                return;
            }

            NewProtocol = new CustomizedArvProtocol
            {
                PatientId = _patient.UserId,
                Status = "Active",
                CustomizedArvProtocolDetails = _customizedDetails
            };

            DialogResult = true;
            Close();
        }
    }
}