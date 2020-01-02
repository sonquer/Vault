using Ninject;
using System.Windows;
using Vault.Core.Models;
using Vault.Infrastructure;

namespace Vault
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            IKernel kernel = new StandardKernel();
            kernel.Bind<IProfileRepository>().To<ProfileRepository>();
            kernel.Bind<LoginWindow>().To<LoginWindow>();
            kernel.Get<LoginWindow>().Show();
        }
    }
}
