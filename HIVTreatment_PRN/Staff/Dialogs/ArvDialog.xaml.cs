using DAL.Entities;
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

namespace HIVTreatment_PRN.Staff.Dialogs
{
    /// <summary>
    /// Interaction logic for ArvDialog.xaml
    /// </summary>
    public partial class ArvDialog : Window
    {
        public Arv ResultArv { get; private set; }

        private bool isEditMode = false;

        public ArvDialog()
        {
            InitializeComponent();
            ResultArv = new Arv();
        }

        public ArvDialog(Arv existingArv) : this()
        {
            isEditMode = true;
            ResultArv = new Arv
            {
                ArvId = existingArv.ArvId,
                Name = existingArv.Name,
                Description = existingArv.Description,
                Status = existingArv.Status
            };

            // Fill data to UI
            txtName.Text = ResultArv.Name;
            txtDescription.Text = ResultArv.Description;
            cbStatus.SelectedItem = ResultArv.Status == "Inactive"
                ? cbStatus.Items[1] : cbStatus.Items[0];
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Tên ARV không được để trống.");
                return;
            }

            ResultArv.Name = txtName.Text.Trim();
            ResultArv.Description = txtDescription.Text.Trim();
            ResultArv.Status = (cbStatus.SelectedItem as ComboBoxItem)?.Content.ToString() ?? "Active";

            DialogResult = true;
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
