using ClinicWpf.Model;
using ClinicWpf.ViewModel;
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

namespace ClinicWpf.View
{
    /// <summary>
    /// Логика взаимодействия для RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        public RegistrationWindow()
        {
            InitializeComponent();
            WindowPresenter.Instance().AddWindow(this, typeof(RegistrationWindow));
        }

        private void OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is not null)
            {
                ((RegistrationViewModel)DataContext).Password = ((PasswordBox)sender).Password;
            }
        }
    }
}
