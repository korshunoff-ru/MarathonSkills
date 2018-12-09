using NLog;
using System.Reactive.Disposables;
using System.Windows;

namespace MarathonSkills
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly CompositeDisposable _disposable;

        protected override void OnStartup(StartupEventArgs e)
        {
            Logger.Info("Starting");
            Logger.Info("Dispatcher managed thread identifier = {0}", System.Threading.Thread.CurrentThread.ManagedThreadId);

            base.OnStartup(e);

            BootStrapper.Start();

            var window = new MainWindow();

            // The window has to be created before the root visual - all to do with the idling service initialising correctly...
            window.DataContext = BootStrapper.RootVisual;

            window.Closed += (s, a) =>
            {
                BootStrapper.Stop();
            };

            Current.Exit += (s, a) =>
            {
                Logger.Info("Bye Bye!");
                LogManager.Flush();
            };

            var resizer = new WindowResizer(window);

            window.Show();

            Logger.Info("Started");
        }

    }
}
