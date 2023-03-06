using ClinicDatabaseModel.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicDatabaseModel
{
    public interface IPatientDatabaseInteractor : IDisposable
    {
        List<Visit> GetVisits(long patient_id);
        Visit AddVisit(long patient_id, DateOnly date, TimeOnly time, string complaint, long wdId);
        void DeleteVisit(long patient_id, DateOnly date, TimeOnly time);
        List<Attachment> GetAttachments(long patient_id);
        Branch? GetBranch(long branch_id);
        List<Recipe> GetRecipes(long patient_id);
        List<Employee> GetEmployees();
        List<Employee> GetEmployees(Func<Employee, bool> predicate);
        Patient? GetPatient(long patient_id);
        List<Speciality> GetSpecialities();
        List<Shedule> GetShedules(Func<Shedule, bool> predicate);
    }
}
