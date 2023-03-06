using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicDatabaseModel
{
    public interface IServiceDatabaseInteractor : IDisposable
    {
        long Authetificate(string username, string password);
        long SignUp(string login,
            string password,
            string firstName,
            string lastName,
            bool Gender,
            DateOnly birthDate,
            decimal snils,
            decimal oms,
            string? Patronymic);
    }
}
