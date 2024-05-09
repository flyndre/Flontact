using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace flontact.Models
{
    /*
    * Contact-Parts for Saving the actual Element and asigning a Tag (For example Tag: Title, Text: Doktor). 
    * 
    * Author: Lukas Burkhardt
    */
    public class ContactPart
    {
        public string Text { get; set; } = string.Empty;
        public ContactPartTag Tag { get; set; } = ContactPartTag.Firstname;

        public ContactPart(string text,ContactPartTag tag)
        {
            Text = text;
            Tag = tag;
        }
    }
}
