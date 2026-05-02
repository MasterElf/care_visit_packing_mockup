using System.Windows;

namespace CareVisitPackingMockup
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        CareDataManager careDataManager = new CareDataManager();
         protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            careDataManager.Initialize();
            MainWindow mainWindow = new MainWindow();
            mainWindow.DataContext = careDataManager.MainModel;
            mainWindow.Show();
        }
    }
}
