using EvilCorp.Pop.Api.Registers;

namespace EvilCorp.Pop.Api.Extensions
{
    public static class RegisterExtension
    {
        public static void RegisterService(this WebApplicationBuilder builder, Type scanningType)
        {
            var registers = GetRegister<IWebAppBuilderRgstr>(scanningType);
            foreach (var register in registers)
            {
                register.RegisterServices(builder);
            }
        }

        public static void RegisterPipelineComponent(this WebApplication app, Type scanningType)
        {
            var registers = GetRegister<IWebAppRgstr>(scanningType);
            foreach (var register in registers)
            {
                register.RegisterPipelineComponents(app);
            }
        }

        private static IEnumerable<T> GetRegister<T>(Type scanningType)
        {
            return scanningType.Assembly.GetTypes()
                .Where(t => t.IsAssignableTo(typeof(T)) && !t.IsAbstract && !t.IsInterface)
                .Select(Activator.CreateInstance)
                .Cast<T>();
        }
    }
}
