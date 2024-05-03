using flontact.Interfaces;
using flontact.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace flontact.Services
{
    public class ParserService : IParserService
    {
        private readonly List<string> degrees = new() { "Dr.", "Prof." };
        private readonly List<string> titles = new() {"Herr","Frau" };
        public IList<ContactPart> Parse(string input)
        {
            var parts = (input.Split(' '));
            var returnList = new List<ContactPart>();
            foreach (var part in parts)
            {
                returnList.Add(ToContactPart(part));
            }
            var lastFirstName = returnList.FindLast(x => x.Tag.Equals(ContactPartTag.Firstname));
            if(lastFirstName  != null)
            {
                lastFirstName.Tag = ContactPartTag.Name;
            }
            return returnList;
        }

        private ContactPart ToContactPart(string stringPart)
        {
            if (degrees.IndexOf(stringPart) != -1)
            {
                return new(stringPart, ContactPartTag.Degree);
            }
            if(titles.IndexOf(stringPart) != -1) 
            {
                return new(stringPart, ContactPartTag.Title); 
            }
            return new(stringPart, ContactPartTag.Firstname);
        }
    }
}
