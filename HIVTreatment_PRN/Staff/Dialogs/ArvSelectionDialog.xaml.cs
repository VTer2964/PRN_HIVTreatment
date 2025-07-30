using DAL.Entities;
using DAL.Repository;
using HIVTreatment_PRN.Staff.Models;
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
    /// Interaction logic for ArvSelectionDialog.xaml
    /// </summary>
    public partial class ArvSelectionDialog : Window
    {
        private readonly ArvRepository _arvRepo;
        private List<Arv> _availableArvs;
        private bool _isEditMode;

        public ArvSelectionModel SelectedArvModel { get; private set; }

        // Constructor for adding new ARV
        public ArvSelectionDialog()
        {
            InitializeComponent();
            _arvRepo = new ArvRepository();
            _isEditMode = false;
            LoadData();
            SetPlaceholderText();
        }

        // Constructor for editing existing ARV
        public ArvSelectionDialog(ArvSelectionModel existingSelection)
        {
            InitializeComponent();
            _arvRepo = new ArvRepository();
            _isEditMode = true;
            LoadData();
            LoadExistingSelection(existingSelection);
            Title = "Sửa ARV trong Protocol";
        }

        private void LoadData()
        {
            try
            {
                // Load all available ARVs
                _availableArvs = _arvRepo.GetAll().Where(a => a.Status == "Active").ToList();
                cmbArv.ItemsSource = _availableArvs;

                if (_availableArvs.Count > 0 && !_isEditMode)
                {
                    cmbArv.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu ARV: {ex.Message}", "Lỗi",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadExistingSelection(ArvSelectionModel existingSelection)
        {
            try
            {
                // Select the ARV in combobox
                var selectedArv = _availableArvs.FirstOrDefault(a => a.ArvId == existingSelection.ArvId);
                if (selectedArv != null)
                {
                    cmbArv.SelectedItem = selectedArv;
                    txtArvDescription.Text = selectedArv.Description ?? "";
                }

                // Load other fields
                txtDosage.Text = existingSelection.Dosage ?? "";
                txtDosage.Foreground = Brushes.Black;

                txtUsageInstruction.Text = existingSelection.UsageInstruction ?? "";
                txtUsageInstruction.Foreground = Brushes.Black;

                // Set status
                var statusItem = cmbStatus.Items.Cast<ComboBoxItem>()
                    .FirstOrDefault(item => item.Content.ToString() == existingSelection.Status);
                if (statusItem != null)
                {
                    cmbStatus.SelectedItem = statusItem;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu chỉnh sửa: {ex.Message}", "Lỗi",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SetPlaceholderText()
        {
            if (string.IsNullOrEmpty(txtDosage.Text) || txtDosage.Text == "Nhập liều lượng...")
            {
                txtDosage.Text = "Nhập liều lượng...";
                txtDosage.Foreground = Brushes.Gray;
            }

            if (string.IsNullOrEmpty(txtUsageInstruction.Text) || txtUsageInstruction.Text == "Nhập hướng dẫn sử dụng...")
            {
                txtUsageInstruction.Text = "Nhập hướng dẫn sử dụng...";
                txtUsageInstruction.Foreground = Brushes.Gray;
            }
        }

        private void CmbArv_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var selectedArv = cmbArv.SelectedItem as Arv;
                if (selectedArv != null)
                {
                    txtArvDescription.Text = selectedArv.Description ?? "Không có mô tả";
                }
                else
                {
                    txtArvDescription.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi chọn ARV: {ex.Message}", "Lỗi",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null)
            {
                if (textBox.Foreground == Brushes.Gray)
                {
                    textBox.Text = "";
                    textBox.Foreground = Brushes.Black;
                }
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null && string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Foreground = Brushes.Gray;

                if (textBox == txtDosage)
                {
                    textBox.Text = "Nhập liều lượng...";
                }
                else if (textBox == txtUsageInstruction)
                {
                    textBox.Text = "Nhập hướng dẫn sử dụng...";
                }
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!ValidateInput())
                    return;

                var selectedArv = cmbArv.SelectedItem as Arv;
                var dosage = txtDosage.Foreground == Brushes.Gray ? "" : txtDosage.Text.Trim();
                var usageInstruction = txtUsageInstruction.Foreground == Brushes.Gray ? "" : txtUsageInstruction.Text.Trim();
                var status = ((ComboBoxItem)cmbStatus.SelectedItem).Content.ToString();

                // Tạo mới SelectedArvModel với đầy đủ thông tin
                this.SelectedArvModel = new ArvSelectionModel
                {
                    ArvId = selectedArv.ArvId,
                    ArvName = selectedArv.Name,
                    ArvDescription = selectedArv.Description ?? "",
                    Dosage = dosage,
                    UsageInstruction = usageInstruction,
                    Status = status
                };

                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm ARV: {ex.Message}", "Lỗi",
                              MessageBoxButton.OK, MessageBoxImage.Error);
                this.DialogResult = false;
            }
            finally
            {
                this.Close();
            }
        }

        private bool ValidateInput()
        {
            if (cmbArv.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn ARV!", "Lỗi",
                              MessageBoxButton.OK, MessageBoxImage.Warning);
                cmbArv.Focus();
                return false;
            }

            if (txtDosage.Foreground == Brushes.Gray || string.IsNullOrWhiteSpace(txtDosage.Text))
            {
                MessageBox.Show("Vui lòng nhập liều lượng!", "Lỗi",
                              MessageBoxButton.OK, MessageBoxImage.Warning);
                txtDosage.Focus();
                return false;
            }

            if (txtUsageInstruction.Foreground == Brushes.Gray || string.IsNullOrWhiteSpace(txtUsageInstruction.Text))
            {
                MessageBox.Show("Vui lòng nhập hướng dẫn sử dụng!", "Lỗi",
                              MessageBoxButton.OK, MessageBoxImage.Warning);
                txtUsageInstruction.Focus();
                return false;
            }

            if (cmbStatus.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn trạng thái!", "Lỗi",
                              MessageBoxButton.OK, MessageBoxImage.Warning);
                cmbStatus.Focus();
                return false;
            }

            return true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}