using System;
using System.IO;
using System.Text.Json;

namespace Formula.SimpleCore
{
    public class ConfigLoader<T>
    {
        public delegate T GetDefaults();

        protected T instance = default(T);

        public ConfigLoader<T> LoadFromFile(string fileName, GetDefaults getDefaults = null)
        {
            if (File.Exists(fileName))
            {
                var json = File.ReadAllText(fileName);
                this.instance = JsonSerializer.Deserialize<T>(json);
            }
            else if (getDefaults != null)
            {
                this.instance = getDefaults();
            }

            return this;
        }

        public ConfigLoader<T> SaveToFile(string fileName)
        {
            if (this.InstanceValid())
            {
                var json = JsonSerializer.Serialize(this.instance);
                var fileStream = File.Open(fileName, FileMode.Append, FileAccess.Write);
                var fileWriter = new StreamWriter(fileStream);
                fileWriter.Write(json);
                fileWriter.Flush();
                fileWriter.Close();
            }

            return this;
        }

        protected bool InstanceValid(bool throwIfNot = true)
        {
            bool output = false;

            if (this.instance == null)
            {
                if (throwIfNot)
                {
                    throw new Exception("Configuration not found");
                }
            }
            else
            {
                output = true;
            }

            return output;
        }


        public static ConfigLoader<T> Get(string fileName, GetDefaults getDefaults = null)
        {
            var output = new ConfigLoader<T>();
            output.LoadFromFile(fileName, getDefaults);
            return output;
        }
    }
}