using Microsoft.Extensions.Configuration;

namespace Utility
{
    public class ConfigHelper
    {
        private static IConfigurationRoot _configuration;

        public void AppSettings()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            _configuration = builder.Build();
        }

        public string GetValue(string key)
        {
            return _configuration[key];
        }

        public static int GetIntConfigurationValue(string configurationName)
        {
            string value = _configuration[configurationName];

            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException($"A configuração '{configurationName}' não foi encontrada ou está vazia.");
            }

            if (!int.TryParse(value, out int intValue))
            {
                throw new InvalidOperationException($"A configuração '{configurationName}' não pode ser convertida para um número inteiro válido.");
            }

            return intValue;
        }
    }
}