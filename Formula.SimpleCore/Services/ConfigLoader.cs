using System;
using System.IO;
using System.Text.Json;

namespace Formula.SimpleCore
{
    public class ConfigLoader<T>
    {
        public delegate T GetDefaults();

        protected T instance = default;

        public ConfigLoader<T> LoadFromFile(string fileName, GetDefaults getDefaults = null)
        {
            if (File.Exists(fileName))
            {
                var json = File.ReadAllText(fileName);
                instance = JsonSerializer.Deserialize<T>(json);
            }
            else if (getDefaults != null)
            {
                instance = getDefaults();
            }

            return this;
        }

        public ConfigLoader<T> SaveToFile(string fileName)
        {
            if (InstanceValid())
            {
                var json = JsonSerializer.Serialize(instance);
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

            if (instance == null)
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