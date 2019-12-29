using System;
using System.Collections;
using System.Configuration;
using Castle.Components.DictionaryAdapter;

namespace Utilities
{
    public static class SettingsLoader<T>
    {
        private static Configuration LoadFromFile(string fileName)
        {
            ExeConfigurationFileMap map = new ExeConfigurationFileMap { ExeConfigFilename = fileName };

            Configuration config = ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);

            return config;
        }

        public static T Load(string fileName)
        {
            var config = LoadFromFile(fileName);
            if (!config.HasFile)
                throw new Exception(string.Format("File '{0}' not found!", fileName));

            var hashTable = CreateHashtable(
                config.AppSettings.Settings);
            
            if (hashTable.Count == 0) 
                throw new Exception(string.Format("'appSettings' of file '{0}' not found or is empty", fileName));

            return new DictionaryAdapterFactory().GetAdapter<T>(hashTable);
        }

        private static Hashtable CreateHashtable(KeyValueConfigurationCollection collection)
        {
            var hashTable = new Hashtable();

            foreach (var key in collection.AllKeys)
            {
                var val = collection[key];

                hashTable.Add(key, val.Value);
            }

            return hashTable;
        }
    }
}
