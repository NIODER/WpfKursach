using ClinicDatabaseModel;
using ClinicDatabaseModel.Database;
using ClinicWpf.Model.Entities;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ClinicWpf.Model
{
    public delegate void SelectedVisitChanged(Entities.Visit? newVisit);
    public delegate void VisitCreated(Entities.Visit? newVisit);
    
    public class ClinicModel
    {
        private readonly PatientDatabaseInteractor db;

        public event SelectedVisitChanged? OnSelectedVisitChanged;
        public event VisitCreated? OnVisitCreated;

        private Entities.Visit? _selectedVisit;
        private long _currentPatientId;

        public ClinicModel() 
        {
            db = new PatientDatabaseInteractor("Host=localhost;Port=5432;Database=clinicdb;Username=postgres;Password=nioder125");
        }

        public Entities.Visit? SelectedVisit
        {
            get => _selectedVisit;
            set
            {
                _selectedVisit = value;
                OnSelectedVisitChanged?.Invoke(_selectedVisit);
            }
        }

        public long PatientId
        {
            get => _currentPatientId;
            set 
            {
                if (_currentPatientId == 0)
                {
                    _currentPatientId = value; 
                }
            }
        }

        public List<Entities.Visit> GetVisits()
        {
            return db.GetVisits(_currentPatientId).Select(Converter.ToEntitiesVisit).ToList();
        }

        public List<string> GetSpecialities()
        {
            return db.GetSpecialities().Select(s => s.SpecialityName).ToList();
        }

        private Speciality? GetSpecialityByName(string specialityName)
        {
            return db.GetSpecialities().FirstOrDefault(s => s.SpecialityName == specialityName);
        }

        public List<Entities.Branch> GetBranches(string specialityName)
        {
            var speciality = GetSpecialityByName(specialityName);
            if (speciality is null)
            {
                return new();
            }
            var branches = new List<ClinicDatabaseModel.Database.Branch>();
            db.GetEmployees(e => e.Specility?.SpecialityName == specialityName)
                .ToList()
                .ForEach(e => e.Shedules.ToList().ForEach(s =>
                {
                    if (!branches.Any(b => b.Equals(s.Branch)))
                    {
                        branches.Add(s.Branch);
                    }
                }));
            return branches.Select(Converter.ToBranch).ToList();
        }

        public List<Doctor> GetDoctors(string spetialityName, long branchId)
        {
            var speciality = db.GetSpecialities().FirstOrDefault(s => s.SpecialityName == spetialityName);
            if (spetialityName is null)
            {
                return new();
            }
            return db.GetEmployees()
                .Where(e => e.Specility?.SpecialityName == spetialityName)
                .Where(e => e.Shedules.Any(s => s.BranchId == branchId))
                .Select(Converter.ToDoctor).ToList();
        }

        public List<Entities.Shedule> GetFreeShedules(long branchId, long employeeId)
        {
            var shedules = db.GetShedules(s => s.BranchId == branchId && s.EmployeeId == employeeId);
            var branch = db.GetBranch(branchId);
            var today = DateOnly.FromDateTime(DateTime.Today);
            var engaged = new List<ClinicDatabaseModel.Database.Visit>();
            for (int i = 0; i < 7; i++)
            {
                engaged.AddRange(db.GetEngagedVisitTimes(branchId, employeeId, (short)today.AddDays(i).DayOfWeek));
            }
            var free = new List<Entities.Shedule>();
            for (int i = 0; i < 7; i++)
            {
                var chosenDayShedule = shedules.FirstOrDefault(s => s.Weekday == (decimal)today.AddDays(i).DayOfWeek);
                if (chosenDayShedule == default)
                {
                    continue;
                }
                for (TimeOnly j = chosenDayShedule.StartWorking; j < new TimeOnly(18, 00); j = j.AddMinutes(15))
                {
                    if (!engaged.Any(e => j.IsBetween(e.SheduledVisitTime, e.SheduledVisitTime.AddMinutes(14))))
                    {
                        var day = today.AddDays(i);
                        var sh = new Entities.Shedule()
                        {
                            VisitStartTime = new(day.Year,
                                day.Month,
                                day.Day,
                                j.Hour, j.Minute, j.Second),
                            WeekDay = (short)day.DayOfWeek,
                            Id = chosenDayShedule.WdId
                        };
                        free.Add(sh);
                    }
                }
            }
            return free;
        }

        public Entities.Visit CreateNewVisit(DateTime dateTime, long wdId, string complaint)
        {
            var visit = Converter.ToEntitiesVisit(db.AddVisit(_currentPatientId,
                DateOnly.FromDateTime(dateTime),
                TimeOnly.FromDateTime(dateTime),
                complaint,
                wdId));
            OnVisitCreated?.Invoke(visit);
            return visit;
        }

        public void DeleteVisit(Entities.Visit visit)
        {
            var dt = DateTime.Parse(visit.DateTime);
            db.DeleteVisit(_currentPatientId, DateOnly.FromDateTime(dt), TimeOnly.FromDateTime(dt));
        }

        public void SignUp(
            string login,
            string password,
            string firstName,
            string lastName,
            bool gender,
            DateOnly birthDate,
            decimal snils,
            decimal oms,
            string? patronymic)
        {
            _currentPatientId = db.SignUp(login, password, firstName, lastName, gender, birthDate, snils, oms, patronymic);
        }

        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="DbException"></exception>
        public void SignIn(string login, string password)
        {
            _currentPatientId = db.Authetificate(login, password);
            if (_currentPatientId == -1)
            {
                throw new InvalidOperationException();
            }
        }
    }
}
