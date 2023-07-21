using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace CSVProgram

{
    class Program
    {
        static void Main(string[] args)
        {
            //Provide the path to your csv file
            string csvFilePath = "C:\\Users\\Irfaan Osman\\Desktop\\Data\\Data.csv";

            if (!File.Exists(csvFilePath))
            {
                Console.WriteLine($"CSV file not found at '{csvFilePath}'");
                return;
            }
            try
            {
                //Step 1 : Read the CSV file and retrive the data
                var csvData = ReadCSVFile(csvFilePath);

                //Step 2: Calculate the frequencies of the first names and last names
                var firstNameFrequencies = CalculateFrequencies(csvData, name => name.Split(' ').First());
                var lastNameFrequencies = CalculateFrequencies(csvData, name => name.Split(' ').Last());

                //Step 3: Sort the first name frequencies by decsending frequency and acsending name
                var sortedFirstNames = SortByNameAndFrequency(firstNameFrequencies);

                //Step4: Sort the last name frequencies by decsending frequency and acsending name
                var sortedLastName = SortByNameAndFrequency(lastNameFrequencies);

                //Step5:Save the sorted first name frequencies to a text file
                SaveToFile("FirstNameFrequecies.txt", sortedFirstNames);

                //Step6: Save the last name frequencies to a text file
                SaveToFile("LastNameFrequencies.txt", sortedLastName);

                //Step7: Sort the addresses alphabetically by street name
                var addresses = csvData.Select(row => row.Address).OrderBy(address => address);

                //Step8: Save the sorted addresses to a text file
                SaveToFile("SortedAddresses.txt", addresses);


            }
            catch (Exception ex)
            {
                Console.WriteLine($"An Error occurred: {ex.Message}");
            }
        }

        //Step 1: Read the CSV file and retrieve the data
        static List<(string Name , string Address)> ReadCSVFile(string filepath)
        {
            var csvData = new List<(string Name , string Address)> ();

            using (var reader = new StreamReader(filepath))
            {
                //Skip the reader line
                reader.ReadLine();

                //Read each line of the CSV File 
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    var name = values[0].Trim();
                    var address = values[1].Trim();

                    //Store the name and address to the csv file list

                    csvData.Add((name, address));

                }

            }
            return csvData;

        }

        //Calculate the frequencies of the first names or last names
        static Dictionary<string , int> CalculateFrequencies(List<(string Name , string Address)> data , Func<string , string> extractNameFunc)
        {
            var frequencies = new Dictionary<string , int>(); 

            //iterate over each data item in the list
            foreach (var item in data) 
            {
                //Extract the first or last name based on the provided extractNameFunc
                var name = extractNameFunc(item.Name);

                //If the name already exists in the frequency dictionary , increment its count

                if (frequencies.ContainsKey(name))
                    frequencies[name]++;
                else
                    frequencies[name] = 1;
            }
            return frequencies;
        }

        //Step 3 and 4 : sort the name frequencies by decsending frequency and name 
        static IEnumerable<string>SortByNameAndFrequency(Dictionary<string , int> frequencies)
        {
            return frequencies.OrderByDescending(entry => entry.Value)
                .ThenBy(entry => entry.Key)
                .Select(entry => $"{entry.Key},{entry.Value}");
        }
        //Step 5 , 6 and 8 : save the lines to a text file
        static void SaveToFile(string filePath , IEnumerable<string> lines) 
        { 
            File.WriteAllLines(filePath, lines);
        }
    }
}