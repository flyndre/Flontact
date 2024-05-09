using flontact.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace flontact.Interfaces
{

    /*
     * Interface for the Parser Service. 
     * 
     * standardizes the required Parser-Methods and Attributs. With this Interface the Parser can later be easily changed. 
     * 
     * Author: Lukas Burkhardt
     */
    public interface IParserService
    {
        IList<ContactPart> Parse(string input);

        string ToString(Contact contact);

        Contact ToContact(List<ContactPart> parts,Gender gender);

        Gender GetGender(string name);
    }
}
