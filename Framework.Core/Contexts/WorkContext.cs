using Framework.Core.DependencyInjection;
using Framework.Core.Settings;

namespace Framework.Core.Contexts
{
    public class WorkContext
    {
        private WorkContext()
        {
        }

        public static IWorkContextProvider Current => ServiceLocator.Current.Resolve<IWorkContextProvider>();

        public static ISettingsService Settings => ServiceLocator.Current.Resolve<ISettingsService>();
    }
}
