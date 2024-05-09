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

        [TestMethod()]
        public void TestNameBeforeSurname()
        {
                // Arrange
            var parser = new NameParser();
            string name = "Bernd";
            string surname = "Müller";

            // Act
            List<ContactPart> retValues = (List<ContactPart>)parser.Parse(name + " " + surname);
            var contact = parser.ToContact(retValues);

            // Assert
            Assert.IsNotNull(contact);
            Assert.AreEqual(Gender.Neutral, contact.Gender);
            Assert.AreEqual(name, contact.FirstNames.FirstOrDefault()?.Text);
            Assert.AreEqual(surname, contact.LastNames.FirstOrDefault()?.Text);
        }

        [TestMethod()]
        public void TestSurnameBeforeName()
        {
        // Arrange
            var parser = new NameParser();
            string surname = "Müller";
            string name = "Bernd";

            // Act
            List<ContactPart> retValues = (List<ContactPart>)parser.Parse(surname + ", " + name);
            var contact = parser.ToContact(retValues);

            // Assert
            Assert.IsNotNull(contact);
            Assert.AreEqual(Gender.Neutral, contact.Gender);
            Assert.AreEqual(name, contact.FirstNames.FirstOrDefault()?.Text);
            Assert.AreEqual(surname, contact.LastNames.FirstOrDefault()?.Text);
        }

        
        //TODO: NOCHMAL ÜBERPRÜFEN
        [TestMethod()]
        public void TestNameWithNobleTitle()
        {
            // Arrange
            var parser = new NameParser();
            string name = "Bernd von Freihaus";
            string surname = "Müller";

            // Act
            List<ContactPart> retValues = (List<ContactPart>)parser.Parse(name + " " + surname);
            var contact = parser.ToContact(retValues);

            // Assert
            Assert.IsNotNull(contact);
            Assert.AreEqual(Gender.Neutral, contact.Gender);
            Assert.IsTrue(name.Contains("von"));
            Assert.AreEqual(name, contact.FirstNames.FirstOrDefault()?.Text);
            Assert.AreEqual(surname, contact.LastNames.FirstOrDefault()?.Text);
        }

        [TestMethod()]
        public void TestNameWithDoctorTitle()
        {
            // Arrange
            var parser = new NameParser();
            string title = "Dr.";
            string name = "Bernd";
            string surname = "Müller";

            // Act
            List<ContactPart> retValues = (List<ContactPart>)parser.Parse(title + " " + name + " " + surname);
            var contact = parser.ToContact(retValues);

            // Assert
            Assert.IsNotNull(contact);
            Assert.AreEqual(Gender.Neutral, contact.Gender);
            Assert.IsTrue(name.Contains("Dr."));
            Assert.AreEqual(title, contact.Degrees.FirstOrDefault()?.Text);
            Assert.AreEqual(name, contact.FirstNames.FirstOrDefault()?.Text);
            Assert.AreEqual(surname, contact.LastNames.FirstOrDefault()?.Text);
        }

        [TestMethod()]
        public void TestNameWithProfessorTitle()
        {
            // Arrange
            var parser = new NameParser();
            string title = "Prof.";
            string name = "Bernd";
            string surname = "Müller";

            // Act
            List<ContactPart> retValues = (List<ContactPart>)parser.Parse(title + " " + name + " " + surname);
            var contact = parser.ToContact(retValues);

            // Assert
            Assert.IsNotNull(contact);
            Assert.AreEqual(Gender.Neutral, contact.Gender);
            Assert.IsTrue(name.Contains("Prof."));
            Assert.AreEqual(title, contact.Degrees.FirstOrDefault()?.Text);
            Assert.AreEqual(name, contact.FirstNames.FirstOrDefault()?.Text);
            Assert.AreEqual(surname, contact.LastNames.FirstOrDefault()?.Text);
        }

        [TestMethod()]
        public void TestNameWithMultipleTitle()
        {
            // Arrange
            var parser = new NameParser();
            string title = "Prof.";
            string title2 = "Dr.";
            string name = "Bernd";
            string surname = "Müller";

            // Act
            List<ContactPart> retValues = (List<ContactPart>)parser.Parse(title + " " + title2 + " " + name + " " + surname);
            var contact = parser.ToContact(retValues);

            // Assert
            Assert.IsNotNull(contact);
            Assert.AreEqual(Gender.Neutral, contact.Gender);
            Assert.IsTrue(name.Contains("Prof."));
            Assert.AreEqual(title, contact.Degrees.FirstOrDefault()?.Text);
            Assert.AreEqual(title2, contact.Degrees[1]?.Text);
            Assert.AreEqual(name, contact.FirstNames.FirstOrDefault()?.Text);
            Assert.AreEqual(surname, contact.LastNames.FirstOrDefault()?.Text);
        }

        [TestMethod()]
        public void TestDoubleSurname()
        {
            // Arrange
            var parser = new NameParser();
            string name = "Bernd";
            string surname = "Müller-Maurer"

            // Act
            List<ContactPart> retValues = (List<ContactPart>)parser.Parse(name);
            var contact = parser.ToContact(retValues);

            // Assert
            Assert.IsNotNull(contact);
            Assert.AreEqual(Gender.Neutral, contact.Gender);
            Assert.AreEqual(surname, contact.LastNames.FirstOrDefault()?.Text);
        }

        [TestMethod()]
        public void TestDoubleFirstName()
        {
            // Arrange
            var parser = new NameParser();
            string name = "Bernd-Lukas";
            string surname =  "Müller";

            // Act
            List<ContactPart> retValues = (List<ContactPart>)parser.Parse(name);
            var contact = parser.ToContact(retValues);

            // Assert
            Assert.IsNotNull(contact);
            Assert.AreEqual(Gender.Neutral, contact.Gender);
            Assert.AreEqual(name, contact.FirstNames.FirstOrDefault()?.Text);
        }

            [TestMethod()]
        public void TestDoubleFirstAndSurname()
        {
            // Arrange
            var parser = new NameParser();
            string name = "Bernd-Lukas";
            string surname =  "Müller-Maurer";

            // Act
            List<ContactPart> retValues = (List<ContactPart>)parser.Parse(name);
            var contact = parser.ToContact(retValues);

            // Assert
            Assert.IsNotNull(contact);
            Assert.AreEqual(Gender.Neutral, contact.Gender);
            Assert.AreEqual(surname, contact.LastNames.FirstOrDefault()?.Text);
            Assert.AreEqual(name, contact.FirstNames.FirstOrDefault()?.Text);
        }
        
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

        
        [TestMethod()]
        public void TestFemaleNameRecognition()
        {
            // Arrange
            var parser = new NameParser();
            string name = "Frau Helga Brauer";

            // Act
            List<ContactPart> retValues = (List<ContactPart>)parser.Parse(name);
            var contact = parser.ToContact(retValues);

            // Assert
            Assert.IsNotNull(contact);
            Assert.AreEqual(Gender.Female, contact.Gender);
        }

        [TestMethod()]
        public void TestMaleNameRecognition()
        {
            // Arrange
            var parser = new NameParser();
            string name = "Herr Bernd Müller";

            // Act
            List<ContactPart> retValues = (List<ContactPart>)parser.Parse(name);
            var contact = parser.ToContact(retValues);

            // Assert
            Assert.IsNotNull(contact);
            Assert.AreEqual(Gender.Male, contact.Gender);
        }

        [TestMethod()]
        public void TestNoGenderRecognition()
        {
            // Arrange
            var parser = new NameParser();
            string name = "Dr. Bernd Müller";

            // Act
            List<ContactPart> retValues = (List<ContactPart>)parser.Parse(name);
            var contact = parser.ToContact(retValues);

            // Assert
            Assert.IsNotNull(contact);
            Assert.AreEqual(Gender.Neutral, contact.Gender);
        }
        

        //Herr Dr. Sandro Gutmensch
        [TestMethod()]
        public void testHerrSandroGutmenschParser()
        {
            ParserService parser = new ParserService();
            List<ContactPart> retValues = (List<ContactPart>)parser.Parse("Herr Dr. Sandro Gutmensch");

            Assert.AreEqual(4, retValues.Count);
            Assert.AreEqual("Herr", retValues[0].Text);
            Assert.AreEqual("Dr.", retValues[1].Text);
            Assert.AreEqual("Sandro", retValues[2].Text);
            Assert.AreEqual("Gutmensch", retValues[3].Text);
            Assert.AreEqual(ContactPartTag.Title, retValues[0].Tag);
            Assert.AreEqual(ContactPartTag.Degree, retValues[1].Tag);
            Assert.AreEqual(ContactPartTag.Firstname, retValues[2].Tag);
            Assert.AreEqual(ContactPartTag.Name, retValues[3].Tag);

            var contact = parser.ToContact(retValues);

            Assert.IsNotNull(contact);
            Assert.AreEqual(Gender.Male, contact.Gender);
            Assert.AreEqual(1, contact.Degrees.Count);
            Assert.AreEqual(1, contact.FirstNames.Count);
            Assert.AreEqual(1, contact.LastNames.Count);

            var contactString = parser.ToString(contact);

            Assert.IsNotNull(contactString);
            Assert.AreEqual("Sehr geehrter Herr Dr. Sandro Gutmensch", contactString);
        }

        //Professor Heinrich Freiherr vom Wald
        [TestMethod()]
        public void testProfHeinrichVomWaldParser()
        {
            ParserService parser = new ParserService();
            List<ContactPart> retValues = (List<ContactPart>)parser.Parse("Professor Heinrich Freiherr vom Wald");

            Assert.AreEqual(5, retValues.Count);
            Assert.AreEqual("Professor", retValues[0].Text);
            Assert.AreEqual("Heinrich", retValues[1].Text);
            Assert.AreEqual("Freiherr", retValues[2].Text);
            Assert.AreEqual("vom", retValues[3].Text);
            Assert.AreEqual("Wald", retValues[4].Text);
            Assert.AreEqual(ContactPartTag.Degree, retValues[0].Tag);
            Assert.AreEqual(ContactPartTag.Firstname, retValues[1].Tag);
            Assert.AreEqual(ContactPartTag.Prefix, retValues[2].Tag);
            Assert.AreEqual(ContactPartTag.Prefix, retValues[3].Tag);
            Assert.AreEqual(ContactPartTag.Name, retValues[4].Tag);

            var contact = parser.ToContact(retValues);

            Assert.IsNotNull(contact);
            Assert.AreEqual(Gender.Male, contact.Gender);
            Assert.AreEqual(1, contact.Degrees.Count);
            Assert.AreEqual(1, contact.FirstNames.Count);
            Assert.AreEqual(1, contact.LastNames.Count);

            var contactString = parser.ToString(contact);
            Assert.AreEqual("Sehr geehrter Professor Heinrich Freiherr vom Wald", contactString);
        }
    }
}
