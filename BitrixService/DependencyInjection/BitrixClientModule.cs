using System.Reflection;
using Autofac;
using Module = Autofac.Module;

namespace BitrixService.DependencyInjection
{
    public class BitrixClientModule : Module
    {
        private readonly Assembly _currentAssembly;

        public BitrixClientModule()
        {
            _currentAssembly = Assembly.GetAssembly(typeof(BitrixClientModule));
        }
        
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(_currentAssembly)
                .Where(t => t.Name.EndsWith("Client"))
                .AsSelf()
                .AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(_currentAssembly)
                .Where(c => c.Name.EndsWith("Config"))
                .AsSelf();
        }
    }
}