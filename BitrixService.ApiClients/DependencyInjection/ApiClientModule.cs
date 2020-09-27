using System.Globalization;
using System.Reflection;
using Autofac;
using BitrixService.Common.Extensions;
using CsvHelper.Configuration;
using Module = Autofac.Module;

namespace BitrixService.ApiClients.DependencyInjection
{
    public class ApiClientModule : Module
    {
        private readonly Assembly _currentAssembly;

        public ApiClientModule()
        {
            _currentAssembly = Assembly.GetAssembly(typeof(ApiClientModule));
        }
        
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(_currentAssembly)
                .Where(t => t.Name.EndsWith("Client"))
                .AsSelf()
                .AsImplementedInterfaces();
            builder.RegisterInstance(GetCsvConfiguration()).AsSelf();
        }

        private CsvConfiguration GetCsvConfiguration()
        {
            var csvConfig = new CsvConfiguration(CultureInfo.CurrentCulture)
            {
                Delimiter = ";",
                HasHeaderRecord = true,
                TrimOptions = TrimOptions.Trim,
                BadDataFound = null
            };
            csvConfig.RegisterClassMapCollection(_currentAssembly);
            return csvConfig;
        }
    }
}