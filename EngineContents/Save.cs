using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace Consyl_Engine.EngineContents
{
    class Save
    {
        public string saveFileName;

        public Save(string fileName)
        {
            saveFileName = fileName;
        }

        public void SaveToFile(object[] vars, bool overwriteData)
        {
            try
            {
                if (overwriteData) File.WriteAllText(saveFileName, "");

                for (int i = 0; i < vars.Length; i++)
                {
                    File.WriteAllText(saveFileName, File.ReadAllText(saveFileName) + "[" + File.ReadAllLines(saveFileName).Length + "]=" + vars[i].ToString() + "\n");
                }
            } catch { }
        }

        public string[] ReadFileContents()
        {
            string[] lines = File.ReadAllLines(saveFileName);

            for (int i = 0; i < lines.Length; i++)
            {
                lines[i] = lines[i].Substring(lines[i].IndexOf('=')+1);
            }

            return lines;
        }
    }
}
