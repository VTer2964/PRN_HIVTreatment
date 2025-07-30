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
using BLL.Service;
using DAL.Entities;
using HIVTreatment_PRN.Doctor;
using HIVTreatment_PRN.Patient;
using HIVTreatment_PRN.Staff;
using DAL.Repository;
namespace HIVTreatment_PRN.Login
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private readonly LoginSer _loginService;

        public LoginWindow()
        {
            InitializeComponent();
            _loginService = new LoginSer();;
        }

        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string input = txtname_or_email.Text;
            string password = txtpassword.Password;

            // Gọi login
            Account? account = await _loginService.UserLogin(input, password);

            if (account == null)
            {
                ErrorText.Text = "Sai tài khoản hoặc mật khẩu.";
                return;
            }

            // Lấy role
            string? role = account.User?.Role;

            // Phân quyền chuyển form
            switch (role)
            {
                case "Patient":
                    new PatientWindow().Show();
                    break;
                case "Doctor":
                   new DoctorWindow().Show();
                    break;
                case "Staff":
                    new StaffWindow().Show();
                    break;
                default:
                    ErrorText.Text = "Không xác định vai trò.";
                    return;
            }
            this.Close();
        }
    }
}