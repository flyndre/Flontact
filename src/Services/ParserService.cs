using flontact.Interfaces;
using flontact.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace flontact.Services
{
    public class ParserService : IParserService
    {
        public IList<ContactPart> Parse(string input)
        {
            var parts = (input.Split(' '));
            var returnList = new List<ContactPart>();
            foreach (var part in parts)
            {
                returnList.Add(new(part,ContactPartTag.Name));
            }
            return returnList;
        }
    }
}
