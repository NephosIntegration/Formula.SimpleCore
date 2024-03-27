using Xunit;
using Moq;
using Microsoft.Extensions.Configuration;

namespace Formula.SimpleCore.Tests
{
    public class ConfigLoaderTests
    {
        public class ConfigType
        {
            public string Key { get; set; } = "asdf";
            // Add other properties as needed
        }

        public class ConfigTypeLoader : ConfigLoader<ConfigType>
        {
            public string GetKeyValue()
            {
                return this.instance.Key;
            }
        }

        [Fact]
        public void SaveToFile_WhenInstanceValid_SavesToFile()
        {
            // Arrange
            var fileName = Path.GetRandomFileName() + ".json";

            var t = ConfigTypeLoader.Get(fileName, () => {
                return new ConfigType {
                    Key = "DefaultValue"
                };
            });

            // Act
            t.SaveToFile(fileName);
        }

        [Fact]
        public void LoadFromFile_WhenFileExists_LoadsFromFile()
        {
            // Arrange
            var j = new ConfigTypeLoader();

            // Act
            j.LoadFromFile("ConfigType.json");

            // Assert
            Assert.Equal("FileValue", j.GetKeyValue());
        }
    }
}