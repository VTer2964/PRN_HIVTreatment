using DAL.Entities;
using DAL.Repository;
using HIVTreatment_PRN.Staff.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for ArvProtocolDialog.xaml
    /// </summary>
    public partial class ArvProtocolDialog : Window
    {
        private readonly ArvProtocolRepository _protocolRepo;
        private readonly ArvRepository _arvRepo;
        private readonly ObservableCollection<ArvSelectionModel> _arvSelections;
        private ArvProtocol _currentProtocol;
        private bool _isEditMode;

        public bool DialogResult { get; private set; }
        public ArvProtocol ResultProtocol { get; private set; }

        public ArvProtocolDialog(ArvProtocol protocol = null)
        {
            InitializeComponent();
            _protocolRepo = new ArvProtocolRepository();
            _arvRepo = new ArvRepository();
            _arvSelections = new ObservableCollection<ArvSelectionModel>();

            dgArvSelections.ItemsSource = _arvSelections;

            _currentProtocol = protocol;
            _isEditMode = protocol != null;

            LoadProtocolData();
        }

        #region Data Loading Methods

        private void LoadProtocolData()
        {
            try
            {
                if (_isEditMode && _currentProtocol != null)
                {
                    // Load existing protocol data
                    txtProtocolName.Text = _currentProtocol.Name ?? "";
                    txtProtocolDescription.Text = _currentProtocol.Description ?? "";

                    // Set status combobox
                    var statusItem = cmbProtocolStatus.Items
                        .Cast<ComboBoxItem>()
                        .FirstOrDefault(item => item.Content.ToString() == _currentProtocol.Status);

                    if (statusItem != null)
                        cmbProtocolStatus.SelectedItem = statusItem;
                    else
                        cmbProtocolStatus.SelectedIndex = 0;

                    // Load ARV details
                    LoadArvDetails();
                    Title = "Sửa ARV Protocol";
                }
                else
                {
                    // New protocol - set defaults
                    cmbProtocolStatus.SelectedIndex = 0; // Default to Active
                    Title = "Thêm ARV Protocol mới";
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Lỗi khi tải dữ liệu protocol: {ex.Message}");
            }
        }

        private void LoadArvDetails()
        {
            try
            {
                _arvSelections.Clear();

                if (_currentProtocol?.ArvProtocolDetails != null)
                {
                    foreach (var detail in _currentProtocol.ArvProtocolDetails)
                    {
                        var arvSelection = new ArvSelectionModel
                        {
                            ArvId = detail.ArvId,
                            ArvName = detail.Arv?.Name ?? "Unknown",
                            ArvDescription = detail.Arv?.Description ?? "",
                            Dosage = detail.Dosage ?? "",
                            UsageInstruction = detail.UsageInstruction ?? "",
                            Status = detail.Status
                        };

                        _arvSelections.Add(arvSelection);
                    }
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Lỗi khi tải chi tiết ARV: {ex.Message}");
            }
        }

        #endregion

        #region ARV Management Events

        private void AddArv_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var arvSelectionDialog = new ArvSelectionDialog();
                arvSelectionDialog.Owner = this;

                if (arvSelectionDialog.ShowDialog() == true && arvSelectionDialog.SelectedArvModel != null)
                {
                    var newSelection = arvSelectionDialog.SelectedArvModel;

                    // Kiểm tra trùng lặp
                    if (_arvSelections.Any(a => a.ArvId == newSelection.ArvId))
                    {
                        ShowWarningMessage("ARV này đã có trong protocol!");
                        return;
                    }

                    // Thêm vào danh sách
                    _arvSelections.Add(newSelection);

                    // Focus và scroll tới item mới
                    dgArvSelections.SelectedItem = newSelection;
                    dgArvSelections.ScrollIntoView(newSelection);
                    dgArvSelections.UpdateLayout(); // Thêm dòng này để đảm bảo UI cập nhật

                    ShowSuccessMessage("Đã thêm ARV vào protocol thành công!");
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Lỗi khi thêm ARV: {ex.Message}");
            }
        }

        private void EditArv_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedArv = dgArvSelections.SelectedItem as ArvSelectionModel;
                if (selectedArv == null)
                {
                    ShowWarningMessage("Vui lòng chọn ARV cần sửa!");
                    return;
                }

                var editDialog = new ArvSelectionDialog(selectedArv);
                if (editDialog.ShowDialog() == true)
                {
                    var updatedSelection = editDialog.SelectedArvModel;

                    // Update the selected item
                    var index = _arvSelections.IndexOf(selectedArv);
                    if (index >= 0)
                    {
                        _arvSelections[index] = updatedSelection;
                        ShowSuccessMessage("Cập nhật ARV thành công!");
                    }
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Lỗi khi sửa ARV: {ex.Message}");
            }
        }

        private void RemoveArv_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedArv = dgArvSelections.SelectedItem as ArvSelectionModel;
                if (selectedArv == null)
                {
                    ShowWarningMessage("Vui lòng chọn ARV cần xóa!");
                    return;
                }

                var result = MessageBox.Show(
                    $"Bạn có chắc chắn muốn xóa ARV '{selectedArv.ArvName}' khỏi protocol?",
                    "Xác nhận xóa",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    _arvSelections.Remove(selectedArv);
                    ShowSuccessMessage("Đã xóa ARV khỏi protocol!");
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Lỗi khi xóa ARV: {ex.Message}");
            }
        }

        private void RefreshArv_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LoadArvDetails();
                ShowSuccessMessage("Đã làm mới danh sách ARV!");
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Lỗi khi làm mới: {ex.Message}");
            }
        }

        private void dgArvSelections_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                var column = e.Column as DataGridBoundColumn;
                if (column != null)
                {
                    var bindingPath = (column.Binding as Binding)?.Path.Path;
                    if (!string.IsNullOrEmpty(bindingPath))
                    {
                        var row = e.Row.Item as ArvSelectionModel;
                        var element = e.EditingElement as TextBox;

                        if (row != null && element != null)
                        {
                            var property = row.GetType().GetProperty(bindingPath);
                            if (property != null)
                            {
                                property.SetValue(row, element.Text);
                            }
                        }
                    }
                }
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!ValidateInput())
                    return;

                bool success;
                if (_isEditMode)
                {
                    // Cập nhật protocol hiện có
                    _currentProtocol.Name = txtProtocolName.Text.Trim();
                    _currentProtocol.Description = txtProtocolDescription.Text.Trim();
                    _currentProtocol.Status = ((ComboBoxItem)cmbProtocolStatus.SelectedItem).Content.ToString();

                    var details = _arvSelections.Select(s => new ArvProtocolDetail
                    {
                        ArvId = s.ArvId,
                        Dosage = s.Dosage,
                        UsageInstruction = s.UsageInstruction,
                        Status = s.Status
                    }).ToList();

                    success = _protocolRepo.UpdateProtocolWithDetails(_currentProtocol, details);
                }
                else
                {
                    // Tạo protocol mới
                    var newProtocol = new ArvProtocol
                    {
                        Name = txtProtocolName.Text.Trim(),
                        Description = txtProtocolDescription.Text.Trim(),
                        Status = ((ComboBoxItem)cmbProtocolStatus.SelectedItem).Content.ToString()
                    };

                    var details = _arvSelections.Select(s => new ArvProtocolDetail
                    {
                        ArvId = s.ArvId,
                        Dosage = s.Dosage,
                        UsageInstruction = s.UsageInstruction,
                        Status = s.Status
                    }).ToList();

                    var newId = _protocolRepo.SaveProtocolWithDetails(newProtocol, details);
                    success = newId > 0;
                }

                if (success)
                {
                    DialogResult = true;
                    Close();
                }
                else
                {
                    ShowErrorMessage("Lưu protocol thất bại!");
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Lỗi khi lưu protocol: {ex.Message}");
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtProtocolName.Text))
            {
                ShowWarningMessage("Vui lòng nhập tên protocol!");
                txtProtocolName.Focus();
                return false;
            }

            if (_arvSelections.Count == 0)
            {
                ShowWarningMessage("Protocol phải có ít nhất một ARV!");
                return false;
            }

            // Validate each ARV selection
            foreach (var selection in _arvSelections)
            {
                if (string.IsNullOrWhiteSpace(selection.Dosage))
                {
                    ShowWarningMessage($"Vui lòng nhập liều lượng cho ARV '{selection.ArvName}'!");
                    return false;
                }

                if (string.IsNullOrWhiteSpace(selection.UsageInstruction))
                {
                    ShowWarningMessage($"Vui lòng nhập hướng dẫn sử dụng cho ARV '{selection.ArvName}'!");
                    return false;
                }
            }

            return true;
        }

        private void CreateNewProtocol()
        {
            var protocolDetails = _arvSelections.Select(selection => new ArvProtocolDetail
            {
                ArvId = selection.ArvId,
                Dosage = selection.Dosage,
                UsageInstruction = selection.UsageInstruction,
                Status = selection.Status
            }).ToList();

            var newProtocol = new ArvProtocol
            {
                Name = txtProtocolName.Text.Trim(),
                Description = txtProtocolDescription.Text.Trim(),
                Status = ((ComboBoxItem)cmbProtocolStatus.SelectedItem).Content.ToString()
            };

            _protocolRepo.SaveProtocolWithDetails(newProtocol, protocolDetails);

            ResultProtocol = newProtocol;
        }

        private void UpdateExistingProtocol()
        {
            _currentProtocol.Name = txtProtocolName.Text.Trim();
            _currentProtocol.Description = txtProtocolDescription.Text.Trim();
            _currentProtocol.Status = ((ComboBoxItem)cmbProtocolStatus.SelectedItem).Content.ToString();

            var protocolDetails = _arvSelections.Select(selection => new ArvProtocolDetail
            {
                ArvId = selection.ArvId,
                Dosage = selection.Dosage,
                UsageInstruction = selection.UsageInstruction,
                Status = selection.Status
            }).ToList();

            _protocolRepo.UpdateProtocolWithDetails(_currentProtocol, protocolDetails);

            ResultProtocol = _currentProtocol;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        #endregion

        #region Helper Methods

        private void ShowSuccessMessage(string message)
        {
            MessageBox.Show(message, "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ShowWarningMessage(string message)
        {
            MessageBox.Show(message, "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void ShowErrorMessage(string message)
        {
            MessageBox.Show(message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        #endregion

        #region Window Events

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                // Focus on the protocol name textbox for new protocols
                if (!_isEditMode)
                {
                    txtProtocolName.Focus();
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Lỗi khi khởi tạo dialog: {ex.Message}");
            }
        }

        #endregion
    }
}