using ClinicWpf.Model;
using ClinicWpf.Model.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ClinicWpf.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
		private readonly ClinicModel clinicModel;

		private Visit? _selectedVisit;
        private ObservableCollection<Visit>? _visits;
		private ViewModelBase _currentVisitView;

        public MainViewModel(ClinicModel clinicModel)
        {
            this.clinicModel = clinicModel;
			clinicModel.OnSelectedVisitChanged += OnSelectedVisitChanged;
			clinicModel.OnVisitCreated += OnVisitCreated;
			_currentVisitView = new VisitEmptyViewModel();
        }

		private void OnSelectedVisitChanged(Visit? visit)
		{
			if (visit is null)
			{
				CurrentVisitView = new VisitEmptyViewModel();
			}
			else
			{
				CurrentVisitView = new VisitViewModel(clinicModel);
			}
		}

		private void OnVisitCreated(Visit? visit)
		{
			if (visit is null)
			{
				return;
			}
			Visits.Add(visit);
			SelectedVisit = visit;
			OnPropertyChanged(nameof(Visits));
			OnPropertyChanged(nameof(SelectedVisit));
            CurrentVisitView = new VisitViewModel(clinicModel);
		}

		private RelayCommand? _createVisit;
		private RelayCommand? _deleteVisit;
		private RelayCommand? _itemClick;


		private void OnCreateVisit()
		{
			CurrentVisitView = new AddVisitViewModels.AddVisitViewModel(clinicModel);
        }

		private void OnDeleteVisit()
		{
			if (SelectedVisit is not null)
			{
				clinicModel.DeleteVisit(SelectedVisit);
				Visits.Remove(SelectedVisit);
			}
            OnPropertyChanged(nameof(Visits));
            OnPropertyChanged(nameof(SelectedVisit));
        }


		private void OnItemClick()
		{
			CurrentVisitView = new VisitViewModel(clinicModel);
		}

		public RelayCommand? ItemClick
		{
			get => _itemClick ??= new RelayCommand(OnItemClick);
		}

		public RelayCommand? DeleteVisit
		{
			get => _deleteVisit ??= new RelayCommand(OnDeleteVisit);
		}

		public RelayCommand CreateVisit
        {
            get => _createVisit ??= new RelayCommand(OnCreateVisit);
		}


		public ViewModelBase CurrentVisitView
		{
			get { return _currentVisitView; }
			set 
			{ 
				_currentVisitView = value;
				OnPropertyChanged(nameof(CurrentVisitView));
			}
		}

		public Visit? SelectedVisit
		{
			get => _selectedVisit;
			set
			{
				_selectedVisit = value;
				clinicModel.SelectedVisit = value;
                OnPropertyChanged(nameof(SelectedVisit));
            }
		}

		public ObservableCollection<Visit> Visits
		{
			get => _visits ?? new(clinicModel.GetVisits());
			set
			{
				_visits = value;
				OnPropertyChanged(nameof(Visits));
			}
		}
	}
}
