using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace Consyl_Engine.EngineContents
{
    class Save
    {
        public string saveFileName; // Stores the file name

        public Save(string fileName) // Initializes the Save object
        {
            saveFileName = fileName;
        }

        /// <summary>
        /// Saves whatever is in vars into the save file.
        /// </summary>
        /// <param name="vars"></param>
        /// <param name="overwriteData"></param>
        public void SaveToFile(string[] vars, bool overwriteData)
        {
            if (overwriteData) File.WriteAllText(saveFileName, "");

            for (int i = 0; i < vars.Length; i++)
            {
                File.WriteAllText(saveFileName, File.ReadAllText(saveFileName) + "[" + File.ReadAllLines(saveFileName).Length + "]=" + vars[i].ToString() + "\n");
            }
        }

        /// <summary>
        /// Returns all the contents of the save file.
        /// </summary>
        /// <returns></returns>
        public string[] ReadFileContents()
        {
            if (File.Exists(saveFileName))
            {
                string[] lines = File.ReadAllLines(saveFileName);

                for (int i = 0; i < lines.Length; i++)
                {
                    lines[i] = lines[i].Substring(lines[i].IndexOf('=') + 1);
                }

                return lines;
            }
            return null;
        }

        /// <summary>
        /// Returns a single saved value based on id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetSavedValuebyID(uint id)
        {
            if (File.Exists(saveFileName))
            {
                string[] lines = File.ReadAllLines(saveFileName);
                if (id < lines.Length)
                    return lines[id].Substring(lines[id].IndexOf('=') + 1);
            }
            return null;
        }

        /// <summary>
        /// Deletes the Save file
        /// </summary>
        public void DeleteSaveFile()
        {
            if (File.Exists(saveFileName)) File.Delete(saveFileName);
        }
    }
}
