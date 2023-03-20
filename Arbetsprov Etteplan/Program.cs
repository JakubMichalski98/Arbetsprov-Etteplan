using System.IO;
using System.Reflection.Metadata;
using System.Xml.Linq;

namespace Arbetsprov_Etteplan
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Variables

            // Initializing path for the file in which value is to be stored
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            // Adds file to path string
            path = Path.Combine(path, "file.txt");

            // Loading the xml file and saving it in document variable
            XDocument document = XDocument.Load("sma_gentext.xml");

            // Method calls
            var targetValue = FindTargetValueInXDocument(document, "42007");
            WriteStringToFileOnDesktop(path, targetValue);

        }

        // Finds the value of target element within trans-unit, where the id matches the parameter and returns the value as a string
        private static string? FindTargetValueInXDocument(XDocument document, string id)
        {

            var v = from t in document.Descendants("trans-unit")
                    where (string?)t.Attribute("id") == id
                    select t.Element("target")?.Value;

            string stringValue = String.Join("", v);

            return stringValue;
        }

        // Writes text to a text-file at the given path
        private static void WriteStringToFileOnDesktop(string path, string? text)
        {
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.Write(text);

                if (string.IsNullOrEmpty(text))
                {
                    Console.WriteLine("Something went wrong when retrieving the value");
                }
                else if (sw.BaseStream.Position == sw.BaseStream.Length)
                { 
                    Console.WriteLine("Value of element written to file successfuly!");
                }
                else
                {
                    Console.WriteLine("Something went wrong while writing to file.");
                }

                sw.Close();
            }
        }
    }
}