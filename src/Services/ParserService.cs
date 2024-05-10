using flontact.Interfaces;
using flontact.Models;
using System.Text;

namespace flontact.Services
{
    /// <summary>
    /// ParserService for parsing the Names.
    /// Implements the IParserService Interface.
    /// Author: Paul Lehmann
    /// </summary>
    public class ParserService : IParserService
    {
        private List<string> degrees = ["Dr.", "Prof.", "Professor", "Dipl.-Ing.", "Dipl.", "Ing.", "rer.", "nat.", "h.c.", "mult."];
        private Dictionary<string, Gender> titles = new() { { "Herr", Gender.Male }, { "Frau", Gender.Female } };
        private List<string> prefixes = ["von", "van", "de"];
        private List<ContactPart> returnList = [];
        private List<ContactPart> parsedList = [];
        private bool wasPrefix = false;

        /// <summary>
        /// Parses a string into a list of ContactParts. Trys to identify the right ContactPartTag for each part.
        /// </summary>
        /// <param name="input">A string containing several parts of a complete salutation.</param>
        /// <returns>A list of ContactPart each tagged with a possible tag.</returns>
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

        /// <summary>
        /// Method to generate a Contact out of a list of ContactPart. It uses the tag of each part to bring that part to the right place.
        /// </summary>
        /// <param name="parts">List of ContactParts to generate from.</param>
        /// <param name="gender">The wanted gender of the contact. Can be everyone available.</param>
        /// <returns>The Contact generated from the parts with the specified gender.</returns>
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
                    case ContactPartTag.Prefix:
                        contact.Prefixs.Add(part);
                        break;
                }
            });
            return contact;

        }

        /// <summary>
        /// Generates a formal form of address out of a given.Contact 
        /// </summary>
        /// <param name="contact">The Contact to parse into a string.</param>
        /// <returns>A string that represents the formal form of address.</returns>
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
                case Gender.Neutral:
                    returnString.Append("Guten Tag");
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
            returnString.AppendJoin(' ', contact.Prefixs.Select(x => x.Text));
            returnString.Append(' ');
            returnString.AppendJoin('-', contact.LastNames.Select(x => x.Text));
            return returnString.ToString();
        }

        /// <summary>
        /// Helper Method to parse a Part of the Name into a ContactPart
        /// </summary>
        /// <param name="stringPart">A part of the unformatted name</param>
        /// <returns>a formatted Part</returns>
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

        /// <summary>
        /// Method to decide which gendet is identified by a title.
        /// </summary>
        /// <param name="name">A string containing a title.</param>
        /// <returns>The gender to the given title. If couldn't determine it's neutral</returns>uthor: Paul Lehmann  
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
