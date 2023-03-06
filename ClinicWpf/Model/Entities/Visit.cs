using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicWpf.Model.Entities
{
    public class Visit
    {
        public string DoctorName { get; private set; }
        public string? Speciality { get; private set; }
        public string DateTime { get; private set; }
        public string Complaint { get; private set; }
        public string Branch { get; private set; }

        public Visit(string doctorName, string? speciality, string dateTime, string complaint, string branch)
        {
            DoctorName = doctorName;
            Speciality = speciality;
            DateTime = dateTime;
            Complaint = complaint;
            Branch = branch;
        }
    }
}
