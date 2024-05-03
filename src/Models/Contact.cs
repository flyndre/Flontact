using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace flontact.Models
{
    public class Contact
    {
        public Gender Gender { get; set; } = Gender.NotSet;
        public List<ContactPart> Degrees { get; set; } = [];
        public List<ContactPart> FirstNames { get; set; } = [];
        public List<ContactPart> LastNames { get; set; } = [];
    }
}
