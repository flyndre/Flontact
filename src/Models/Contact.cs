using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace flontact.Models
{
    /// <summary>
    /// Contact-Class for Saving the Contact. 
    /// Saves gender, degrees, first- and lastnames.
    /// Author: Lukas Burkhardt
    /// </summary>
    public class Contact
    {
        public Gender Gender { get; set; } = Gender.Neutral;
        public List<ContactPart> Degrees { get; set; } = [];
        public List<ContactPart> Prefix { get; set; } = [];
        public List<ContactPart> FirstNames { get; set; } = [];
        public List<ContactPart> LastNames { get; set; } = [];
    }
}
