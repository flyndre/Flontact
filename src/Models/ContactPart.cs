using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace flontact.Models
{
    /// <summary>
    /// Represents a part of a salutation. Contains the text of the part and a tag to decribe the type of the part.
    /// Author: Lukas Burkhardt
    /// </summary>
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
