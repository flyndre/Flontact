using flontact.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace flontact.Interfaces
{
    public interface IParserService
    {
        IList<ContactPart> Parse(string input);

        string ToString(Contact contact);

        Contact ToContact(List<ContactPart> parts,Gender gender);

        Gender GetGender(string name);
    }
}
