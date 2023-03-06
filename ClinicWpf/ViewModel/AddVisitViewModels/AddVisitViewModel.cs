using ClinicWpf.Model;
using ClinicWpf.Model.Entities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ClinicWpf.ViewModel.AddVisitViewModels
{
    public class AddVisitViewModel : ViewModelBase
    {
        private readonly ClinicModel model;
        private ViewModelBase? _currentDialogView;
        private string _nextButtonText = "Далее";

        private RelayCommand? _next;
        private RelayCommand? _previous;

        public RelayCommand? PreviousCommand
        {
            get => _previous ??= new RelayCommand(OnPreviousButtonClick);
        }

        public RelayCommand? NextCommand
        {
            get => _next ??= new RelayCommand(OnNextButtonClick);
        }

        private int _dialog_index = 0;

        private ObservableCollection<string> _specialities = new();
        private ObservableCollection<Branch>? _branches;
        private ObservableCollection<Doctor>? _doctors;
        private ObservableCollection<Shedule>? _freeShedules;


        private string _chosenSpeciality = string.Empty;
        private Doctor _chosenDoctor = new();
        private Branch _chosenBranch = new();
        private Shedule _chosenShedule = new();
        private string _complaint = "Введите жалобу сюда";


        public AddVisitViewModel(ClinicModel model)
        {
            this.model = model;
            Specialities = new(model.GetSpecialities());
            CurrentDialogView = new AddVisitViewModel1();
        }

        private void OnNextButtonClick()
        {
            if (_dialog_index == 4)
            {
                return;
            }
            _dialog_index++;
            SetVisitViewModel();
        }

        private void OnPreviousButtonClick()
        {
            if (_dialog_index == 0)
            {
                return;
            }
            _dialog_index--;
            SetVisitViewModel();
        }

        private void SetVisitViewModel()
        {
            switch (_dialog_index)
            {
                case 0:
                    CurrentDialogView = new AddVisitViewModel1();
                    break;
                case 1:
                    if (_chosenSpeciality.IsNullOrEmpty())
                    {
                        MessageBox.Show("Выберете специальность врача.", "Ошибка");
                        return;
                    }
                    Branches = new(model.GetBranches(_chosenSpeciality));
                    CurrentDialogView = new AddVisitViewModel2();
                    break;
                case 2:
                    Doctors = new(model.GetDoctors(_chosenSpeciality, _chosenBranch.Id));
                    CurrentDialogView = new AddVisitViewModel3();
                    break;
                case 3:
                    FreeShedules = new(model.GetFreeShedules(_chosenBranch.Id, _chosenDoctor.EmployeeId));
                    ChosenShedule = FreeShedules.First();
                    NextButtonText = "Добавить";
                    CurrentDialogView = new AddVisitViewModel4();
                    break;
                case 4:
                    model.CreateNewVisit(ChosenShedule.VisitStartTime, ChosenShedule.Id, Complaint);
                    CurrentDialogView = new AddVisitViewModel1();
                    _dialog_index = 0;
                    NextButtonText = "Далее";
                    break;
                default:
                    break;
            }
        }


        public ObservableCollection<string> Specialities
        {
            get => _specialities;
            set
            {
                _specialities = value;
                OnPropertyChanged(nameof(Specialities));
            }
        }

        public ObservableCollection<Branch> Branches
        {
            get => _branches ?? new();
            set
            {
                _branches = value;
                OnPropertyChanged(nameof(Branches));
            }
        }

        public ObservableCollection<Doctor> Doctors
        {
            get { return _doctors ?? new(); }
            set
            {
                _doctors = value;
                OnPropertyChanged(nameof(Doctors));
            }
        }

        public ObservableCollection<Shedule> FreeShedules
        {
            get => _freeShedules ?? new();
            set
            {
                _freeShedules = value;
                OnPropertyChanged(nameof(FreeShedules));
            }
        }


        public string ChosenSpeciality
        {
            get => _chosenSpeciality;
            set
            {
                _chosenSpeciality = value;
                OnPropertyChanged(nameof(ChosenSpeciality));
            }
        }

        public Branch ChosenBranch 
        {
            get => _chosenBranch; 
            set
            {
                _chosenBranch = value;
                OnPropertyChanged(nameof(ChosenBranch));
            }
        }

        public Doctor ChosenDoctor
        {
            get => _chosenDoctor;
            set
            {
                _chosenDoctor = value;
                OnPropertyChanged(nameof(ChosenDoctor));
            }
        }

        public Shedule ChosenShedule
        {
            get { return _chosenShedule; }
            set
            {
                _chosenShedule = value;
                OnPropertyChanged(nameof(ChosenShedule));
            }
        }

        public string Complaint
        {
            get => _complaint;
            set
            {
                _complaint = value;
                OnPropertyChanged(nameof(Complaint));
            }
        }


        public ViewModelBase? CurrentDialogView
        {
            get { return _currentDialogView; }
            set
            {
                _currentDialogView = value;
                OnPropertyChanged(nameof(CurrentDialogView));
            }
        }

        public string NextButtonText
        {
            get => _nextButtonText;
            set
            {
                _nextButtonText = value;
                OnPropertyChanged(nameof(NextButtonText));
            }
        }
    }
}
