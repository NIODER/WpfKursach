using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicWpf.Model.Entities
{
    public class Branch
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;

        public Branch()
        {

        }

        public Branch(long id, string name, string address)
        {
            Id = id;
            Name = name;
            Address = address;
        }
    }
}
