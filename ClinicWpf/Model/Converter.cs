using ClinicDatabaseModel.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicWpf.Model
{
    internal static class Converter
    {
        public static Entities.Visit ToEntitiesVisit(Visit visit)
        {
            var doctor = visit.Wd.Employee;
            return new Entities.Visit(
                $"{doctor.Lastname} {doctor.Firstname} {doctor.Patronymic}",
                doctor.Specility?.SpecialityName,
                $"{visit.VisitDate} {visit.SheduledVisitTime}",
                visit.Complaint,
                visit.Wd.Branch.Name);
        }

        public static Entities.Doctor ToDoctor(Employee employee)
        {
            return new Entities.Doctor()
            {
                EmployeeId = employee.EmployeeId,
                Fullname = $"{employee.Lastname} {employee.Firstname} {employee.Patronymic}"
            };
        }

        public static Entities.Branch ToBranch(Branch branch)
        {
            return new Entities.Branch(branch.BranchId, branch.Name, branch.Address);
        }

        public static DateTime ToDateTime(Visit visit) => new(visit.VisitDate.Year,
                    visit.VisitDate.Month,
                    visit.VisitDate.Day,
                    visit.SheduledVisitTime.Hour,
                    visit.SheduledVisitTime.Minute,
                    visit.SheduledVisitTime.Second);
    }
}
