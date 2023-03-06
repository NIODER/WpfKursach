using ClinicWpf.Model;
using ClinicWpf.Model.Entities;
using System;

namespace ClinicWpf.ViewModel
{
	public class VisitViewModel : ViewModelBase
    {
		private readonly ClinicModel model;

		private string _doctorName = string.Empty;
		private string _speciality = string.Empty;
		private string _dateTime = string.Empty;
		private string _complaint = string.Empty;
		private string _branch = string.Empty;

		public VisitViewModel(ClinicModel model)
		{
			this.model = model;
			model.OnSelectedVisitChanged += OnSelectedVisitChanged;
		}

		private void OnSelectedVisitChanged(Visit? visit)
		{
			if (visit is null) return;
            Doctor = visit.DoctorName;
            Speciality = visit.Speciality ?? "unknown";
            DateTime = visit.DateTime;
            Complaint = visit.Complaint;
            BranchName = visit.Branch;
        }

		public string Doctor
		{
			get { return _doctorName; }
			set 
			{ 
				_doctorName = value; 
				OnPropertyChanged(nameof(Doctor));
			}
		}

		public string Speciality
		{
			get { return _speciality; }
			set { _speciality = value; }
		}

		public string DateTime
		{
			get { return _dateTime; }
			set 
			{
				_dateTime = value;
				OnPropertyChanged(nameof(DateTime));
			}
		}

		public string Complaint
		{
			get { return _complaint; }
			set 
			{ 
				_complaint = value;
                OnPropertyChanged(nameof(Complaint));
            }
        }

		public string BranchName
		{
			get { return _branch; }
			set 
			{ 
				_branch = value;
                OnPropertyChanged(nameof(BranchName));
            }
        }
	}
}
