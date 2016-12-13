using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ProcessSimLevelEditor
{
    class JSONWriter
    {
        public void OutputJson(LevelOutputJSON levelOutput, string fileName)
        {
            string gridArray = JsonConvert.SerializeObject(levelOutput.GridArray, Formatting.None);
            levelOutput.LevelGrid = gridArray;
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
    }
}
