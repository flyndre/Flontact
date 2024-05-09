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
    /*
    * ParserService for parsing the Names.
    * 
    * Implements the IParserService Interface
    * 
    * Author: Paul Lehmann
    */
    public class ParserService : IParserService
    {
        private List<string> degrees = ["Dr.", "Prof.", "Professor", "Dipl.-Ing.", "Dipl.", "Ing.", "rer.", "nat.", "h.c.", "mult."];
        private Dictionary<string, Gender> titles = new() { { "Herr", Gender.Male }, { "Frau", Gender.Female } };
        private List<string> prefixes = ["von", "van", "de"];

        private List<ContactPart> returnList = new();
        private List<ContactPart> parsedList = new();

        //flags
        private bool wasPrefix = false;

        /*
        * Parses the Input into ContactParts.
        * 
        *attribute: input -> Unformatted Input Name
        *returns: IList<ContactPart> -> A List of the Formatted Names 
        * 
        * Author: Paul Lehmann
        */
        public IList<ContactPart> Parse(string input)
        {
            bool reversed = false;

            var parts = (input.Split(' '));
            returnList = new List<ContactPart>();
            foreach(var part in parts)
            {
                if (part.Contains(","))
                {
                    reversed = true;
                }
                returnList.Add(ToContactPart(part.Replace(",", "")));
            }

            var lastFirstName = returnList.Last();
            if (lastFirstName != null)
            {
                lastFirstName.Tag = ContactPartTag.Name;
            }

            if (reversed)
            {
                foreach (var part in returnList)
                {
                    if(part.Tag == ContactPartTag.Firstname)
                    {
                        part.Tag = ContactPartTag.Name;
                    }
                    else if (part.Tag == ContactPartTag.Name)
                    {
                        part.Tag = ContactPartTag.Firstname;
                    }
                }
            }

            parsedList = new(returnList);

            return returnList;
        }

        /*
        * Parses the Input into ContactParts.
        * 
        *attribute: parts -> Formatted Parts of the Name, gender -> The Gender of the Person
        *returns: Contact -> The formatted Parts as the Contact-Class
        * 
        * Author: Paul Lehmann
        */
        public Contact ToContact(List<ContactPart> parts,Gender gender)
        {
            //learn on user corrected input
            parts.ForEach(part =>
            {
                switch (part.Tag)
                {
                    case ContactPartTag.Prefix:
                        prefixes.Add(part.Text);
                        break;
                    case ContactPartTag.Degree:
                        degrees.Add(part.Text);
                        break;
                }
            });

            //create contact out of list
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
                        contact.Gender = gender;
                        break;
                }
            });
            return contact;

        }

        /*
        *generates the Salutation for the given Contact.
        * 
        *attribute: contact -> the contact from witch the salutation should be generated
        *returns: string -> the salutation
        * 
        * Author: Paul Lehmann  
        */
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

            if (contact.Degrees.Count > 0)
            {
                returnString.Append(' ');
            }

            returnString.AppendJoin(' ', contact.Degrees.Select(x => x.Text));
            returnString.Append(' ');
            returnString.AppendJoin(' ', contact.FirstNames.Select(x => x.Text));
            returnString.Append(' ');
            returnString.AppendJoin('-', contact.LastNames.Select(x => x.Text));
            return returnString.ToString();
        }

        /*
        *Helper Function to parse a Part of the Name into a ContactPart
        * 
        *attribute: stringPart -> a Part of the unformatted Name
        *returns: ContactPart -> a formatted Part
        * 
        * Author: Paul Lehmann  
        */
        private ContactPart ToContactPart(string stringPart)
        {
            //check for known keywords
            if (prefixes.Contains(stringPart.ToLower()))
            {
                if(returnList.Count > 0)
                {
                    if (returnList.Last().Tag == ContactPartTag.Name)
                    {
                        returnList.Last().Tag = ContactPartTag.Firstname;
                    }
                }
                
                wasPrefix = true;
                return new(stringPart, ContactPartTag.Prefix);
            }
            if (degrees.Contains(stringPart))
            {
                return new(stringPart, ContactPartTag.Degree);
            }
            if (titles.ContainsKey(stringPart))
            {
                return new(stringPart, ContactPartTag.Title);
            }

            //try to parse based on context
            if (wasPrefix)
            {
                return new(stringPart, ContactPartTag.Name);
            }

            //unable to parse
            return new(stringPart, ContactPartTag.Firstname);
        }

        /*
        *returns the Gender of a given Name.
        * 
        *attribute: name -> the unformatted Name
        *returns: Gender -> the Gender of the Name
        * 
        * Author: Paul Lehmann  
        */
        public Gender GetGender(string name)
        {
            if (titles.TryGetValue(name, out var gender))
            {
                return gender;
            }
            return Gender.Neutral;
        }
    }
}
