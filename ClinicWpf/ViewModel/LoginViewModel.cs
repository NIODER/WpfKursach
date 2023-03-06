using ClinicDatabaseModel;
using ClinicWpf.Model;
using ClinicWpf.View;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ClinicWpf.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
		private readonly ClinicModel model;

        public LoginViewModel(ClinicModel model)
        {
            this.model = model;
        }

        private string? _login;

		public string? Login
		{
			get { return _login; }
			set
			{
				_login = value;
				OnPropertyChanged(nameof(Login));
			}
		}


		private string? password;

		public string? Password
		{
			get { return password; }
			set
			{
				password = value;
				OnPropertyChanged(nameof(Password));
			}
		}

		private RelayCommand? registrateCommand;

		public RelayCommand? RegistrateCommand
		{
			get { return registrateCommand ??= new RelayCommand(OnRegistrateCommand); }
			set 
			{ 
				registrateCommand = value;
				OnPropertyChanged(nameof(RegistrateCommand));
			}
		}

		private void OnRegistrateCommand()
		{
            var wp = WindowPresenter.Instance();
            wp.OpenWindow(typeof(RegistrationWindow));
            wp.CloseWindow(typeof(LoginWindow));
        }


		private RelayCommand? _loginCommand;

		public RelayCommand LoginCommand
		{
			get
			{
				return _loginCommand ??= new RelayCommand(TryLogin);
			}
		}

		private bool ValidatePassword()
		{
			return !password.IsNullOrEmpty() && password?.Length > 5;
		}

		private void TryLogin()
		{
			if (!(ValidatePassword() || !_login.IsNullOrEmpty()))
			{
				MessageBox.Show("Недопустимый пароль.", "Ошибка");
				return;
			}
			try
			{
				model.SignIn(_login, Password);
			}
			catch (InvalidOperationException)
			{
                MessageBox.Show("Неправильный пароль.", "Ошибка");
                return;
			}
			catch (DbException)
			{
                MessageBox.Show($"Пользователя с логином {_login} не существует.", "Ошибка");
				return;
            }
			var wp = WindowPresenter.Instance();
			wp.OpenWindow(typeof(MainWindow));
			wp.CloseWindow(typeof(LoginWindow));
        }
    }
}
