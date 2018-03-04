using System.Windows;
using Autofac;
using FriendOrganizer.UI.StartUp;

namespace FriendOrganizer.UI
{
    /// <summary>
    /// Logika interakcji dla klasy App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            var bootstrapper = new Bootstrapper();
            var container = bootstrapper.Bootstrap();

            var mainWindow = container.Resolve<MainWindow>();            
            mainWindow.Show();
        }
    }
}
