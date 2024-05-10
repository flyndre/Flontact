using flontact.Models;

namespace flontact.Interfaces
{
    /// <summary>
    /// Interface for the Parser Service.
    /// Standardizes the required Parser-Methods and Attributs. With this Interface the Parser can later be easily changed.
    /// Author: Lukas Burkhardt
    /// </summary>
    public interface IParserService
    {
        /// <summary>
        /// Parses a string into a list of Contactparts. Trys to identify the right ContactPartTag for each part.
        /// </summary>
        /// <param name="input">A string containing several parts of a complete salutation.</param>
        /// <returns>A list of ContactPart each tagged with a possible tag.</returns>
        IList<ContactPart> Parse(string input);

        /// <summary>
        /// Generates a formal form of address out of a given.Contact 
        /// </summary>
        /// <param name="contact">The Contact to parse into a string.</param>
        /// <returns>A string that represents the formal form of address.</returns>
        string ToString(Contact contact);

        /// <summary>
        /// Method to generate a Contact out of a list of ContactPart. It uses the tag of each part to bring that part to the right place.
        /// </summary>
        /// <param name="parts">List of ContactParts to generate from.</param>
        /// <param name="gender">The wanted gender of the contact. Can be everyone available.</param>
        /// <returns>The Contact generated from the parts with the specified gender.</returns>
        Contact ToContact(List<ContactPart> parts,Gender gender);

        /// <summary>
        /// Method to decide which gendet is identified by a title.
        /// </summary>
        /// <param name="name">A string containing a title.</param>
        /// <returns>The gender to the given title. If couldn't determine it's neutral</returns>
        Gender GetGender(string name);
    }
}
