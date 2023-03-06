using ClinicDatabaseModel;
using ClinicWpf.Model;
using ClinicWpf.View;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ClinicWpf.ViewModel
{
    public class RegistrationViewModel : ViewModelBase
    {
        private readonly ClinicModel model;

        private string? firstname;
        private string? lastname;
        private string? patronymic;
        private bool gender;
        private DateOnly birthDate;
        private string? _snils;
        private string? _oms;
        private string? login;
        private string? password;

        private RelayCommand? commitCommand;

        public RelayCommand? CommitCommand
        {
            get { return commitCommand ??= new RelayCommand(OnCommitCommand); }
            set
            {
                commitCommand = value;
                OnPropertyChanged(nameof(CommitCommand));
            }
        }

        private void OnCommitCommand()
        {
            if (firstname.IsNullOrEmpty())
            {
                MessageBox.Show("Необходимо указать имя.", "Оишбка");
                return;
            }
            if (lastname.IsNullOrEmpty())
            {
                MessageBox.Show("Необходимо указать фамилию.", "Ошибка");
                return;
            }
            if (birthDate >= DateOnly.FromDateTime(DateTime.Today))
            {
                MessageBox.Show("Недопустимая дата рождения.", "Ошибка");
                return;
            }
            if (_snils.IsNullOrEmpty())
            {
                MessageBox.Show("Укажите СНИЛС.", "Ошибка");
                return;
            }
            if (!decimal.TryParse(_snils, out decimal snils))
            {
                MessageBox.Show("Недопустимое значение: СНИЛС.", "Ошибка");
                return;
            }
            if (_oms.IsNullOrEmpty())
            {
                MessageBox.Show("Укажите ОМС.", "Ошибка");
                return;
            }
            if (!decimal.TryParse(_oms, out decimal oms))
            {
                MessageBox.Show("Недопустимое значение: ОМС.", "Ошибка");
                return;
            }
            if (login.IsNullOrEmpty())
            {
                MessageBox.Show("Укажите логин.", "Ошибка");
                return;
            }
            if (password.IsNullOrEmpty())
            {
                MessageBox.Show("Укажите пароль.", "Ошибка");
                return;
            }
            if (password?.Length < 5)
            {
                MessageBox.Show("Пароль слишком короткий.", "Ошибка");
                return;
            }
            try
            {
                model.SignUp(login, password, firstname, lastname, Gender, birthDate, snils, oms, patronymic);
            }
            catch (RegistrationException e)
            {
                switch (e.RegistrationExceptionCode)
                {
                    case RegistrationException.ExceptionCode.NotSpecified:
                        MessageBox.Show("Возникла неизвестная ошибка, попробуйте позже.", "Ошибка");
                        break;
                    case RegistrationException.ExceptionCode.LoginEngaged:
                        MessageBox.Show($"Логин {Login} занят. Поробуйте другой.", "Ошибка");
                        break;
                    case RegistrationException.ExceptionCode.InvalidPassword:
                        MessageBox.Show("Недопустимый пароль.", "Ошибка");
                        break;
                    case RegistrationException.ExceptionCode.InvalidUserData:
                        MessageBox.Show($"Недопустимые данные пользователя: {e.UserFieldName}.", "Ошибка");
                        break;
                    case RegistrationException.ExceptionCode.InvalidLogin:
                        MessageBox.Show("Недопустимый логин.", "Ошибка");
                        break;
                }
                return;
            }
            var wd = WindowPresenter.Instance();
            wd.OpenWindow(typeof(MainWindow));
            wd.CloseWindow(typeof(RegistrationWindow));
        }

        public RegistrationViewModel(ClinicModel model)
        {
            this.model = model;
        }

        public string? Firstname
        {
            get { return firstname; }
            set 
            { 
                firstname = value;
                OnPropertyChanged(nameof(Firstname));
            }
        }

        public string? Lastname
        {
            get { return lastname; }
            set
            {
                lastname = value;
                OnPropertyChanged(nameof(Lastname));
            }
        }

        public string? Patronymic
        {
            get { return patronymic; }
            set
            {
                patronymic = value;
                OnPropertyChanged(nameof(Patronymic));
            }
        }

        public bool Gender
        {
            get { return gender; }
            set 
            {
                gender = value;
                OnPropertyChanged(nameof(Gender));
            }
        }

        public DateOnly BirthDate
        {
            get { return birthDate; }
            set 
            { 
                birthDate = value;
                OnPropertyChanged(nameof(BirthDate));
            }
        }

        public string? Snils
        {
            get => _snils;
            set
            {
                _snils = value;
                OnPropertyChanged(nameof(Snils));
            }
        }

        public string? Oms
        {
            get { return _oms; }
            set
            { 
                _oms = value;
                OnPropertyChanged(nameof(Oms));
            }
        }

        public string? Login
        {
            get { return login; }
            set { login = value; }
        }

        public string? Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
    }
}
