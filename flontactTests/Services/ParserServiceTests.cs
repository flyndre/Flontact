using Microsoft.VisualStudio.TestTools.UnitTesting;
using flontact.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using flontact.Models;

namespace flontact.Services.Tests
{
    [TestClass()]
    public class ParserServiceTests
    {
        //Frau Sandra Berger
        [TestMethod()]
        public void testFrauSandraBergerParser()
        {
            ParserService parser = new ParserService();
            List<ContactPart> retValues = (List<ContactPart>)parser.Parse("Frau Sandra Berger");

            Assert.AreEqual(3, retValues.Count);
            Assert.AreEqual("Frau", retValues[0].Text);
            Assert.AreEqual("Sandra", retValues[1].Text);
            Assert.AreEqual("Berger", retValues[2].Text);
            Assert.AreEqual(ContactPartTag.Title, retValues[0].Tag);
            Assert.AreEqual(ContactPartTag.Firstname, retValues[1].Tag);
            Assert.AreEqual(ContactPartTag.Name, retValues[2].Tag);

            var contact = parser.ToContact(retValues);

            Assert.IsNotNull(contact);
            Assert.AreEqual(Gender.Female, contact.Gender);
            Assert.AreEqual(0, contact.Degrees.Count);
            Assert.AreEqual(1, contact.FirstNames.Count);
            Assert.AreEqual(1, contact.LastNames.Count);
            
            var contactString = parser.ToString(contact);

            Assert.IsNotNull(contactString);
            Assert.AreEqual("Sehr geehrte Frau Sandra Berger", contactString);
        }
    }
}