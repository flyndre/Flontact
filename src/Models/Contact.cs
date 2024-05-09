using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace flontact.Models
{
    /*
    * Contact-Class for Saving the Contact. 
    * 
    * Saves Gender, Degrees, First and Lastname as Lists. 
    * 
    * Author: Lukas Burkhardt
    */
    public class Contact
    {
        public Gender Gender { get; set; } = Gender.Neutral;
        public List<ContactPart> Degrees { get; set; } = [];
        public List<ContactPart> FirstNames { get; set; } = [];
        public List<ContactPart> LastNames { get; set; } = [];
    }
}
