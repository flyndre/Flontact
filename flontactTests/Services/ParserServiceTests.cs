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
    /// <summary>
    /// This Class is used for Testing the Equality-Classes.
    /// </summary>
    [TestClass]
    public class ParserServiceTests
    {
        /// <summary>
        /// Test for Equality-Class "Name before Surname".
        /// This Tests the Input "Bernd Müller" and Asserts that the Output matches the Requirements.
        /// </summary>
        [TestMethod]
        public void TestNameBeforeSurname()
        {
            // Arrange
            var parser = new ParserService();
            string name = "Bernd";
            string surname = "Müller";

            // Act
            List<ContactPart> retValues = (List<ContactPart>)parser.Parse(name + " " + surname);
            var contact = parser.ToContact(retValues, Gender.Neutral);

            // Assert
            Assert.IsNotNull(contact);
            Assert.AreEqual(Gender.Neutral, contact.Gender);
            Assert.AreEqual(name, contact.FirstNames.FirstOrDefault()?.Text);
            Assert.AreEqual(surname, contact.LastNames.FirstOrDefault()?.Text);
        }


        /// <summary>
        /// Test for Equality-Class "Name after Surname".
        /// This Tests the Input "Müller, Bernd" and Asserts that the Output matches the Requirements.
        /// </summary>
        [TestMethod]
        public void TestSurnameBeforeName()
        {
            // Arrange
            var parser = new ParserService();
            string surname = "Müller";
            string name = "Bernd";

            // Act
            List<ContactPart> retValues = (List<ContactPart>)parser.Parse(surname + ", " + name);
            var contact = parser.ToContact(retValues, Gender.Neutral);

            // Assert
            Assert.IsNotNull(contact);
            Assert.AreEqual(Gender.Neutral, contact.Gender);
            Assert.AreEqual(name, contact.FirstNames.FirstOrDefault()?.Text);
            Assert.AreEqual(surname, contact.LastNames.FirstOrDefault()?.Text);
        }


        /// <summary>
        /// TODO: Fix Error
        /// Test for Equality-Class "Noble Title".
        /// This Tests the Input "Bernd von Freihaus Müller" and Asserts that the Output matches the Requirements.
        /// </summary>
        [TestMethod]
        public void TestNameWithNobleTitle()
        {
            // Arrange
            var parser = new ParserService();
            string name = "Bernd";
            string title = "von Freihaus";
            string surname = "Müller";

            // Act
            List<ContactPart> retValues = (List<ContactPart>)parser.Parse(name + " " + title + " " + surname);
            var contact = parser.ToContact(retValues, Gender.Neutral);

            // Assert
            Assert.IsNotNull(contact);
            Assert.AreEqual(Gender.Neutral, contact.Gender);
            Assert.AreEqual(name, contact.FirstNames.FirstOrDefault()?.Text);
            Assert.AreEqual(title, contact.Degrees.FirstOrDefault()?.Text);
            Assert.AreEqual(surname, contact.LastNames.FirstOrDefault()?.Text);
        }

        /// <summary>
        /// Test for Equality-Class "Doctor Title".
        /// This Tests the Input "Dr. Bernd Müller" and Asserts that the Output matches the Requirements
        /// </summary>
        [TestMethod]
        public void TestNameWithDoctorTitle()
        {
            // Arrange
            var parser = new ParserService();
            string title = "Dr.";
            string name = "Bernd";
            string surname = "Müller";

            // Act
            List<ContactPart> retValues = (List<ContactPart>)parser.Parse(title + " " + name + " " + surname);
            var contact = parser.ToContact(retValues, Gender.Neutral);

            // Assert
            Assert.IsNotNull(contact);
            Assert.AreEqual(Gender.Neutral, contact.Gender);
            Assert.AreEqual(title, contact.Degrees.FirstOrDefault()?.Text);
            Assert.AreEqual(name, contact.FirstNames.FirstOrDefault()?.Text);
            Assert.AreEqual(surname, contact.LastNames.FirstOrDefault()?.Text);
        }

        /// <summary>
        /// Test for Equality-Class "Professor Title".
        /// This Tests the Input "Prof. Bernd Müller" and Asserts that the Output matches the Requirements.
        /// </summary>
        [TestMethod]
        public void TestNameWithProfessorTitle()
        {
            // Arrange
            var parser = new ParserService();
            string title = "Prof.";
            string name = "Bernd";
            string surname = "Müller";

            // Act
            List<ContactPart> retValues = (List<ContactPart>)parser.Parse(title + " " + name + " " + surname);
            var contact = parser.ToContact(retValues, Gender.Neutral);

            // Assert
            Assert.IsNotNull(contact);
            Assert.AreEqual(Gender.Neutral, contact.Gender);
            Assert.AreEqual(title, contact.Degrees.FirstOrDefault()?.Text);
            Assert.AreEqual(name, contact.FirstNames.FirstOrDefault()?.Text);
            Assert.AreEqual(surname, contact.LastNames.FirstOrDefault()?.Text);
        }

        /// <summary>
        /// Test for Equality-Class "Professor Title".
        /// This Tests the Input "Prof. Bernd Müller" and Asserts that the Output matches the Requirements.
        /// </summary>
        [TestMethod]
        public void TestNameWithMultipleTitle()
        {
            // Arrange
            var parser = new ParserService();
            string title = "Prof.";
            string title2 = "Dr.";
            string name = "Bernd";
            string surname = "Müller";

            // Act
            List<ContactPart> retValues = (List<ContactPart>)parser.Parse(title + " " + title2 + " " + name + " " + surname);
            var contact = parser.ToContact(retValues, Gender.Neutral);

            // Assert
            Assert.IsNotNull(contact);
            Assert.AreEqual(Gender.Neutral, contact.Gender);
            Assert.AreEqual(title, contact.Degrees.FirstOrDefault()?.Text);
            Assert.AreEqual(title2, contact.Degrees[1]?.Text);
            Assert.AreEqual(name, contact.FirstNames.FirstOrDefault()?.Text);
            Assert.AreEqual(surname, contact.LastNames.FirstOrDefault()?.Text);
        }

        /// <summary>
        /// Test for Equality-Class "Double Surname".
        /// This Tests the Input "Bernd Müller-Maurer" and Asserts that the Output matches the Requirements.
        /// </summary>
        [TestMethod]
        public void TestDoubleSurname()
        {
            // Arrange
            var parser = new ParserService();
            string name = "Bernd";
            string surname = "Müller-Maurer";

            // Act
            List<ContactPart> retValues = (List<ContactPart>)parser.Parse(name + " " + surname);
            var contact = parser.ToContact(retValues, Gender.Neutral);

            // Assert
            Assert.IsNotNull(contact);
            Assert.AreEqual(Gender.Neutral, contact.Gender);
            Assert.AreEqual(surname, contact.LastNames.FirstOrDefault()?.Text);
        }

        /// <summary>
        /// Test for Equality-Class "Double Firstname".
        /// This Tests the Input "Bernd-Lukas Müller" and Asserts that the Output matches the Requirements.
        /// 
        /// </summary>
        [TestMethod]
        public void TestDoubleFirstName()
        {
            // Arrange
            var parser = new ParserService();
            string name = "Bernd-Lukas";
            string surname = "Müller";

            // Act
            List<ContactPart> retValues = (List<ContactPart>)parser.Parse(name + " " + surname);
            var contact = parser.ToContact(retValues, Gender.Neutral);

            // Assert
            Assert.IsNotNull(contact);
            Assert.AreEqual(Gender.Neutral, contact.Gender);
            Assert.AreEqual(name, contact.FirstNames.FirstOrDefault()?.Text);
        }

        /// <summary>
        /// Test for Equality-Class "Double First- and Surname".
        /// This Tests the Input "Bernd-Lukas Müller-Maurer" and Asserts that the Output matches the Requirements.
        /// </summary>
        [TestMethod]
        public void TestDoubleFirstAndSurname()
        {
            // Arrange
            var parser = new ParserService();
            string name = "Bernd-Lukas";
            string surname = "Müller-Maurer";

            // Act
            List<ContactPart> retValues = (List<ContactPart>)parser.Parse(name + " " + surname);
            var contact = parser.ToContact(retValues, Gender.Neutral);

            // Assert
            Assert.IsNotNull(contact);
            Assert.AreEqual(Gender.Neutral, contact.Gender);
            Assert.AreEqual(surname, contact.LastNames.FirstOrDefault()?.Text);
            Assert.AreEqual(name, contact.FirstNames.FirstOrDefault()?.Text);
        }

        /// <summary>
        /// Test for Equality-Class "Female Gender".
        /// This Tests the Input "Frau Helga Brauer" and Asserts that the Output matches the Requirements
        /// </summary>
        [TestMethod]
        public void TestFemaleNameRecognition()
        {
            // Arrange
            var parser = new ParserService();
            string name = "Frau Helga Brauer";

            // Act
            List<ContactPart> retValues = (List<ContactPart>)parser.Parse(name);
            var contact = parser.ToContact(retValues, Gender.Female);

            // Assert
            Assert.IsNotNull(contact);
            Assert.AreEqual(Gender.Female, contact.Gender);
        }

        /// <summary>
        /// Test for Equality-Class "Male Gender".
        /// This Tests the Input "Herr Bernd Müller" and Asserts that the Output matches the Requirements.
        /// </summary>
        [TestMethod]
        public void TestMaleNameRecognition()
        {
            // Arrange
            var parser = new ParserService();
            string name = "Herr Bernd Müller";

            // Act
            List<ContactPart> retValues = (List<ContactPart>)parser.Parse(name);
            var contact = parser.ToContact(retValues, Gender.Male);

            // Assert
            Assert.IsNotNull(contact);
            Assert.AreEqual(Gender.Male, contact.Gender);
        }

        /// <summary>
        /// Test for Equality-Class "No Gender".
        /// This Tests the Input "Dr. Bernd Müller" and Asserts that the Output matches the Requirements.
        /// </summary>
        [TestMethod]
        public void TestNoGenderRecognition()
        {
            // Arrange
            var parser = new ParserService();
            string name = "Dr. Bernd Müller";

            // Act
            List<ContactPart> retValues = (List<ContactPart>)parser.Parse(name);
            var contact = parser.ToContact(retValues, Gender.Neutral);

            // Assert
            Assert.IsNotNull(contact);
            Assert.AreEqual(Gender.Neutral, contact.Gender);
        }
    }
}
      
