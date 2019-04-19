using Castle.Windsor;

namespace Framework.Configuration.DependencyInjection
{
    public interface IRegistrar
    {
        void Setup(IWindsorContainer container);
    }
}