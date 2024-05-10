﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    /// This class is used for testing the equality classes.
    /// </summary>
    [TestClass]
    public class ParserServiceTests
    {
        /// <summary>
        /// Test for equality class "name before surname".
        /// This tests the input "Bernd Müller" and asserts that the output matches the requirements.
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
        /// Test for equality class "name after surname".
        /// This tests the input "Müller, Bernd" and asserts that the output matches the requirements.
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
        /// Test for equality class "noble title".
        /// This tests the input "Bernd von Freihaus Müller" and asserts that the output matches the requirements.
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
        /// Test for equality class "Doctor Title".
        /// This tests the Input "Dr. Bernd Müller" and asserts that the output matches the requirements
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
        /// Test for equality class "Professor Title".
        /// This tests the input "Prof. Bernd Müller" and asserts that the output matches the requirements.
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
        /// Test for equality class "Double Surname".
        /// This tests the input "Bernd Müller-Maurer" and asserts that the output matches the requirements.
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
        /// Test for equality class "Double Firstname".
        /// This tests the input "Bernd-Lukas Müller" and asserts that the output matches the requirements.
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
        /// Test for equality class "Double First- and Surname".
        /// This tests the input "Bernd-Lukas Müller-Maurer" and asserts that the output matches the requirements.
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
        /// Test for equality class "Female Gender".
        /// This tests the input "Frau Helga Brauer" and asserts that the output matches the requirements
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
        /// Test for equality class "Male Gender".
        /// This tests the Input "Herr Bernd Müller" and asserts that the output matches the requirements.
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
        /// Test for equality class "No Gender".
        /// This tests the input "Dr. Bernd Müller" and asserts that the output matches the equirements.
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
      
