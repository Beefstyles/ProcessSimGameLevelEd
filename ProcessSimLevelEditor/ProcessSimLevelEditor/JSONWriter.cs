using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace ProcessSimLevelEditor
{
    class JSONWriter
    {
        public void OutputJson(LevelOutputJSON levelOutput, char[,] levelGridArray, string fileName)
        {
            string GridArrayStringTotal = CharArrayToString(levelGridArray);

            var OutputStringList = GridArrayToStringList(GridArrayStringTotal, levelGridArray.GetLength(0));
            levelOutput.LevelGridArray = OutputStringList;
            try
            {
                WriteJsonFile(levelOutput, fileName);
            }
            catch (Exception error)
            {
                MessageBox.Show("Writing JsonValue output did not work correctly, error code is " + error.Message);
            }
        }

        private void WriteJsonFile(LevelOutputJSON levelOutput, string savePath)
        {
            JsonSerializer serialiser = new JsonSerializer();
            serialiser.NullValueHandling = NullValueHandling.Ignore;
            string myDocsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            using (StreamWriter sw = new StreamWriter(savePath))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serialiser.Formatting = Formatting.Indented;
                serialiser.Serialize(writer, levelOutput);
            }

            MessageBox.Show("Writing json done correctly, look in " + savePath);
        }
      

        //Splits the 2D char array to a single string
        private string CharArrayToString(char[,] array)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    sb.Append(array[i,j]);
                }
            }
            return sb.ToString();
        }

        //Splits the string created above to a List<string>
        private List<string> GridArrayToStringList(string stringToSplit, int splitSize)
        {
            List<string> StringList = new List<string>();
            for (int i = 0; i < stringToSplit.Length; i += splitSize)
            {
                if ((i + splitSize) < stringToSplit.Length)
                {
                    StringList.Add(stringToSplit.Substring(i, splitSize));
                }
                else
                {
                    StringList.Add(stringToSplit.Substring(i));
                }
            }
            return StringList;
        }
    }
}
