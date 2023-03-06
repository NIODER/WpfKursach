using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ClinicDatabaseModel
{
    public class DbException : Exception
    {
        public DbException()
        {
        }

        public DbException(string? message) : base(message)
        {
        }

        public DbException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected DbException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public string? DbSourse
		{
			get 
			{ 
				return base.Data["DbSource"]?.ToString(); 
			}
			set 
			{
				base.Data.Add("DbSource", value);
			}
		}
	}
}
