using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicWpf.Model.Entities
{
    public class Shedule
    {
        public long Id { get; set; }
        public DateTime VisitStartTime { get; set; }
        public short WeekDay { get; set; }
    }
}
