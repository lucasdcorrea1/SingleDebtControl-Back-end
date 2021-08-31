using Microsoft.Extensions.Configuration;
using System;

namespace Utils.Environments
{
    public class Parameters
    {
        public ParametersValues Data { get; set; }

        public Parameters()
        {
            Data = GetData();
        }

        private ParametersValues GetData()
        {
            try
            {
                var parameters = new ParametersValues();
                var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                var basePath = AppDomain.CurrentDomain.BaseDirectory;
                var builder = new ConfigurationBuilder()
                    .SetBasePath(basePath)
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{env}.json", optional: true)
                    .AddEnvironmentVariables();

                var configRoot = builder.Build();
                configRoot.GetSection("Parameters").Bind(parameters);
                parameters.Environment = env;

                return parameters;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }

    public class ParametersValues
    {
        public string ConnectionString { get; set; }
        public string ConnectionStringHangfire { get; set; }
        public string Environment { get; set; }
    }

}
