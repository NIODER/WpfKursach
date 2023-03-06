using System.Runtime.Serialization;

namespace ClinicDatabaseModel
{
    public class RegistrationException : DbException
    {
        public RegistrationException()
        {
        }

        public RegistrationException(string? message) : base(message)
        {
        }

        public RegistrationException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected RegistrationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public enum ExceptionCode
        {
            NotSpecified = 1,
            LoginEngaged = 1 << 2,
            InvalidPassword = 1 << 3,
            InvalidUserData = 1 << 4,
            InvalidLogin = 1 << 5
        }

        public ExceptionCode RegistrationExceptionCode
        {
            get
            {
                return (ExceptionCode)(Data["ExceptionCode"] ?? default(ExceptionCode));
            }
            set
            {
                Data.Add("ExceptionCode", value);
            }
        }

        public string? UserFieldName
        {
            get
            {
                return Data["UserField"]?.ToString();
            }
            set
            {
                Data.Add("UserField", value);
            }
        }
    }
}
