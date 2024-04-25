using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace flontact.Models
{
    public class ContactPart
    {
        public string Text { get; set; } = string.Empty;
        public ContactPartTag Tag { get; set; }

        public ContactPart(string text,ContactPartTag tag)
        {
            Text = text;
            Tag = tag;
        }
    }
}
