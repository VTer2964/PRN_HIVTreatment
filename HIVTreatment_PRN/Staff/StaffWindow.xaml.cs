using BLL.Service;
using DAL.Entities;
using HIVTreatment_PRN.Staff.Dialogs;
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

namespace HIVTreatment_PRN.Staff
{
    /// <summary>
    /// Interaction logic for StaffWindow.xaml
    /// </summary>
    public partial class StaffWindow : Window
    {
        private readonly ArvService _arvService = new ArvService();
        private readonly ArvProtocolService _protocolService = new ArvProtocolService();

        public StaffWindow()
        {
            InitializeComponent();
            LoadData();
        }

        #region Data Loading Methods

        private void LoadData()
        {
            try
            {
                LoadArvData();
                LoadProtocolData();
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Lỗi khi tải dữ liệu: {ex.Message}");
            }
        }

        private void LoadArvData()
        {
            try
            {
                var arvs = _arvService.GetAll();
                ArvGrid.ItemsSource = arvs;
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Lỗi khi tải danh sách ARV: {ex.Message}");
            }
        }

        private void LoadProtocolData()
        {
            try
            {
                // Giờ chỉ cần gọi bình thường
                ProtocolGrid.ItemsSource = _protocolService.GetAll();
                ProtocolGrid.Items.Refresh();
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Lỗi khi tải danh sách Protocol: {ex.Message}");
            }
        }

        #endregion

        #region ARV Management Events

        private void AddArv_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var dialog = new ArvDialog();
                if (dialog.ShowDialog() == true)
                {
                    _arvService.Create(dialog.ResultArv);
                    LoadArvData();
                    ShowSuccessMessage("Thêm ARV thành công!");
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
                if (ArvGrid.SelectedItem is Arv selected)
                {
                    var dialog = new ArvDialog(selected);
                    if (dialog.ShowDialog() == true)
                    {
                        _arvService.Update(dialog.ResultArv);
                        LoadArvData();
                        ShowSuccessMessage("Cập nhật ARV thành công!");
                    }
                }
                else
                {
                    ShowWarningMessage("Vui lòng chọn ARV cần sửa.");
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Lỗi khi sửa ARV: {ex.Message}");
            }
        }

        private void DeleteArv_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ArvGrid.SelectedItem is Arv selected)
                {
                    var result = MessageBox.Show(
                        $"Bạn có chắc chắn muốn xóa ARV '{selected.Name}'?\n\nHành động này không thể hoàn tác!",
                        "Xác nhận xóa ARV",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Warning);

                    if (result == MessageBoxResult.Yes)
                    {
                        _arvService.Delete(selected.ArvId);
                        LoadArvData();
                        ShowSuccessMessage("Xóa ARV thành công!");
                    }
                }
                else
                {
                    ShowWarningMessage("Vui lòng chọn ARV cần xóa.");
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
                LoadArvData();
                ShowSuccessMessage("Đã làm mới danh sách ARV!");
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Lỗi khi làm mới danh sách ARV: {ex.Message}");
            }
        }

        private void AddProtocol_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var dialog = new ArvProtocolDialog();
                if (dialog.ShowDialog() == true)
                {
                    LoadProtocolData(); // Refresh lại danh sách
                    ShowSuccessMessage("Thêm Protocol thành công!");
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Lỗi khi thêm Protocol: {ex.Message}");
            }
        }


        private void EditProtocol_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ProtocolGrid.SelectedItem is ArvProtocol selected)
                {
                    var dialog = new ArvProtocolDialog(selected);
                    if (dialog.ShowDialog() == true)
                    {
                        LoadProtocolData(); // Refresh lại danh sách
                        ShowSuccessMessage("Cập nhật Protocol thành công!");
                    }
                }
                else
                {
                    ShowWarningMessage("Vui lòng chọn Protocol cần sửa.");
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Lỗi khi sửa Protocol: {ex.Message}");
            }
        }

        private void DeleteProtocol_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ProtocolGrid.SelectedItem is ArvProtocol selected)
                {
                    var result = MessageBox.Show(
                        $"Bạn có chắc chắn muốn xóa Protocol '{selected.Name}'?\n\nTất cả ARV trong protocol này cũng sẽ bị xóa!\nHành động này không thể hoàn tác!",
                        "Xác nhận xóa Protocol",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Warning);

                    if (result == MessageBoxResult.Yes)
                    {
                        _protocolService.Delete(selected.ProtocolId);
                        LoadProtocolData();
                        ShowSuccessMessage("Xóa Protocol thành công!");
                    }
                }
                else
                {
                    ShowWarningMessage("Vui lòng chọn Protocol cần xóa.");
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Lỗi khi xóa Protocol: {ex.Message}");
            }
        }

        private void RefreshProtocol_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LoadProtocolData();
                ShowSuccessMessage("Đã làm mới danh sách Protocol!");
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Lỗi khi làm mới danh sách Protocol: {ex.Message}");
            }
        }

        private void ViewProtocolDetail_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Get the selected protocol from the button's DataContext
                var button = sender as Button;
                var selectedProtocol = button?.DataContext as ArvProtocol;

                if (selectedProtocol != null)
                {
                    var dialog = new ArvProtocolDialog(selectedProtocol);
                    if (dialog.ShowDialog() == true)
                    {
                        LoadProtocolData(); // Refresh the list if changes were made
                    }
                }
                else
                {
                    ShowWarningMessage("Không thể xác định Protocol được chọn.");
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Lỗi khi xem chi tiết Protocol: {ex.Message}");
            }
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
                // Additional initialization if needed
                LoadData();
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Lỗi khi khởi tạo ứng dụng: {ex.Message}");
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                // Cleanup if needed
                var result = MessageBox.Show(
                    "Bạn có chắc chắn muốn thoát ứng dụng?",
                    "Xác nhận thoát",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.No)
                {
                    e.Cancel = true;
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Lỗi khi thoát ứng dụng: {ex.Message}");
            }
        }

        #endregion
    }
}