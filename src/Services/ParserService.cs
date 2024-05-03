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
        private readonly List<string> degrees = ["Dr.", "Prof."];
        private readonly Dictionary<string, Gender> titles = new() { { "Herr", Gender.Male }, { "Frau", Gender.Female } };
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

        public Contact ToContact(List<ContactPart> parts)
        {
            var contact = new Contact();
            parts.ForEach(part =>
            {
                switch (part.Tag)
                {
                    case ContactPartTag.Degree:
                        contact.Degrees.Add(part);
                        break;
                    case ContactPartTag.Firstname:
                        contact.FirstNames.Add(part);
                        break;
                    case ContactPartTag.Name:
                        contact.LastNames.Add(part);
                        break;
                    case ContactPartTag.Title:
                        if (titles.TryGetValue(part.Text, out var gender))
                        {
                            contact.Gender = gender;
                        }
                        break;
                }
            });
            return contact;

        }

        public string ToString(Contact contact)
        {
            var returnString = new StringBuilder();
            switch (contact.Gender)
            {
                case Gender.Male:
                    returnString.Append("Sehr geehrter Herr");
                    break;
                case Gender.Female:
                    returnString.Append("Sehr geehrte Frau");
                    break;
            }

            returnString.Append(' ');
            returnString.AppendJoin(' ', contact.Degrees.Select(x => x.Text));
            returnString.Append(' ');
            returnString.AppendJoin(' ', contact.FirstNames.Select(x => x.Text));
            returnString.Append(' ');
            returnString.AppendJoin('-', contact.LastNames.Select(x => x.Text));
            return returnString.ToString();
        }

        private ContactPart ToContactPart(string stringPart)
        {
            if (degrees.IndexOf(stringPart) != -1)
            {
                return new(stringPart, ContactPartTag.Degree);
            }
            if(titles.ContainsKey(stringPart)) 
            {
                return new(stringPart, ContactPartTag.Title); 
            }
            return new(stringPart, ContactPartTag.Firstname);
        }
    }
}
