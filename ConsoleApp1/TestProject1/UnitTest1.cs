using Microsoft.VisualStudio.TestPlatform.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;


namespace CSVProgram.Test
{
    [TestClass]
    public class CSVProgramTest
    {
        [TestMethod]
        public void TestCalculateFrequencies()
        {
            //Arrange
            var data = new List<(string Name, string Address)>

            {
                ("Matt Brown" , "12 Acton St"),
                ("Heinrich Jones" , "31 Clifton Rd"),
                ("Johnson Smith" , "22 Jones Rd"),
                ("Tim Johnson" , "7 Elm St")
            };

            //Act
            var firstNameFrequencies = Program.CalculateFrequencies(data, name => name.Split(' ').First());
            var lastNameFrequencies = Program.CalculateFrequencies(data, name => name.Split(' ').Last());


            //Assert
            Assert.AreEqual(2, firstNameFrequencies["Johnson"]);
            Assert.AreEqual(1, firstNameFrequencies["Brown"]);
            Assert.AreEqual(1, firstNameFrequencies["Heinrich"]);
            Assert.AreEqual(1, firstNameFrequencies["Jones"]);
            Assert.AreEqual(1, firstNameFrequencies["Matt"]);
            Assert.AreEqual(1, firstNameFrequencies["Smith"]);
            Assert.AreEqual(1, firstNameFrequencies["Tim"]);

            Assert.AreEqual(1, lastNameFrequencies["Brown"]);
            Assert.AreEqual(1, lastNameFrequencies["Jones"]);
            Assert.AreEqual(1, lastNameFrequencies["Smith"]);
            Assert.AreEqual(2, lastNameFrequencies["Johnson"]);




        }
    }
}