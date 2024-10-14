using Autofac;
using IContainer = Autofac.IContainer;

namespace ToDoApp.MVVM
{
    public class Resolver
    {
        private static IContainer container;

        public static void Init(IContainer c)
        {
            container = c;
        }

        public static T Resolve<T>()
        {
            return container.Resolve<T>();
        }
    }
}
