using ClinicDatabaseModel.Database;
using ClinicDatabaseModel.Database.Context;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicDatabaseModel
{
    public class PatientDatabaseInteractor : 
        IServiceDatabaseInteractor,
        IPatientDatabaseInteractor
    {
        private readonly ClinicdbContext _dbContext;


        public PatientDatabaseInteractor(string connectionString)
        {
            _dbContext = new ClinicdbContext(connectionString);
        }

        public PatientDatabaseInteractor(ConnectionCredentials connectionCredentials)
        {
            _dbContext = new ClinicdbContext(connectionCredentials.ToString());
        }

        /// <exception cref="DbException"></exception>
        public Visit AddVisit(long patient_id, DateOnly date, TimeOnly time, string complaint, long wdId)
        {
            var engaged = _dbContext.Visits.Any(v => v.VisitDate == date && v.SheduledVisitTime.IsBetween(time, time.AddMinutes(15)));
            if (engaged)
            {
                throw new DbException("This visit time is engaged.")
                {
                    DbSourse = "AddVisit"
                };
            }
            var visit = new Visit()
            {
                PatientId = patient_id,
                VisitDate = date,
                SheduledVisitTime = time,
                Complaint = complaint,
                WdId = wdId
            };
            var patient = _dbContext.Patients
                .FirstOrDefault(p => p.PatientId == patient_id) ?? throw new DbException($"No patient with id {patient_id}")
                {
                    DbSourse = "AddVisit"
                };
            visit.Patient = patient;
            visit = _dbContext.Visits.Add(visit).Entity;
            _dbContext.SaveChanges();
            return visit;
        }

        /// <exception cref="DbException"></exception>
        public long Authetificate(string username, string password)
        {
            var patient = _dbContext.Patients.FirstOrDefault(p => p.Login == username);
            if (patient is null)
            {
                throw new DbException($"No user with login {username}")
                {
                    DbSourse = "Patient.Authentificate"
                };
            }
            return Crypt.Compare(password, patient.Password) ? patient.PatientId : -1;
        }

        public void DeleteVisit(long patient_id, DateOnly date, TimeOnly time)
        {
            var visit = _dbContext.Visits
                .FirstOrDefault(v => v.PatientId == patient_id && v.VisitDate == date && v.SheduledVisitTime == time);
            if (visit is null)
            {
                return;
            }
            _dbContext.Visits.Remove(visit);
            _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public List<Attachment> GetAttachments(long patient_id)
        {
            return _dbContext.Attachments.Where(a => a.PatientId == patient_id).ToList();
        }

        public Branch? GetBranch(long branch_id)
        {
            return _dbContext.Branches
                .Include(b => b.Shedules)
                .FirstOrDefault(b => b.BranchId == branch_id);
        }

        public List<Employee> GetEmployees()
        {
            return _dbContext.Employees
                .Include(e => e.Shedules)
                    .ThenInclude(s => s.Branch)
                .Include(e => e.Specility).ToList();
        }

        public List<Employee> GetEmployees(Func<Employee, bool> predicate)
        {
            return _dbContext.Employees
                .Include(e => e.Shedules)
                    .ThenInclude(s => s.Branch)
                .Include(e => e.Specility)
                .Where(predicate).ToList();
        }

        public Patient? GetPatient(long patient_id)
        {
            return _dbContext.Patients
                .Include(p => p.Recipes)
                .Include(p => p.Attachments)
                .Include(p => p.Visits)
                .FirstOrDefault(p => p.PatientId == patient_id);
        }

        public List<Recipe> GetRecipes(long patient_id)
        {
            return _dbContext.Recipes.Where(r => r.PatientId == patient_id).ToList();
        }

        public List<Visit> GetVisits(long patient_id)
        {
            return _dbContext.Visits
                .Include(v => v.Wd)
                    .ThenInclude(wd => wd.Branch)
                .Include(v => v.Wd)
                    .ThenInclude(wd => wd.Employee)
                        .ThenInclude(e => e.Specility)
                .Include(v => v.Patient)
                .Where(v => v.PatientId == patient_id).ToList();
        }

        public List<Visit> GetEngagedVisitTimes(long branchId, long employeeId, short weekDay)
        {
            return _dbContext.Visits
                .Include(v => v.Wd)
                .Where(v => v.Wd.BranchId == branchId)
                .Where(v => v.Wd.Weekday == weekDay)
                .Where(v => v.Wd.EmployeeId == employeeId)
                .Where(v => v.VisitDate > DateOnly.FromDateTime(DateTime.UtcNow))
                .ToList();
        }

        private bool LoginIsEngaged(string login)
        {
            return _dbContext.Patients.Any(p => p.Login == login);
        }

        /// <exception cref="RegistrationException"></exception>
        public long SignUp(string login, string password, string firstName, string lastName, bool gender, DateOnly birthDate, decimal snils, decimal oms, string? Patronymic)
        {
            if (LoginIsEngaged(login))
            {
                throw new RegistrationException($"Login {login} already engaged.")
                {
                    DbSourse = "Patient.SignUp (Registration)",
                    RegistrationExceptionCode = RegistrationException.ExceptionCode.LoginEngaged
                };
            }
            var patient = new Patient()
            {
                Login = login,
                Password = Crypt.GetMD5Hash(password),
                Firstname = firstName,
                Lastname = lastName,
                Patronymic = Patronymic,
                Gender = gender,
                BirthDate = birthDate,
                Snils = snils,
                Oms = oms
            };
            patient = _dbContext.Patients.Add(patient).Entity;
            _dbContext.SaveChanges();
            return patient.PatientId;
        }

        public List<Speciality> GetSpecialities()
        {
            return _dbContext.Specialities.ToList();
        }

        public List<Shedule> GetShedules(Func<Shedule, bool> predicate)
        {
            return _dbContext.Shedules
                .Where(predicate).ToList();
        }
    }
}
