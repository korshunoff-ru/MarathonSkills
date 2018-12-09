using Autofac;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MarathonSkills
{
    public class MainViewModel : BaseViewModel
    {

        public ITimerService TimerService { get; set; }

        #region Commands

        public RelayCommand MinimizeCommand { get; set; }
        public RelayCommand MaximizeCommand { get; set; }
        public RelayCommand CloseCommand { get; set; }

        #endregion

        public MainViewModel(IPageService pageService, ITimerService timerService) : base(pageService)
        {

            TimerService = timerService;
            TimerService.Start(new DateTime(2018, 12, 10, 0, 0, 0));

            PageService.GoToPage(ApplicationPage.HomePage);

            MinimizeCommand = new RelayCommand(() =>
            {
                Application.Current.MainWindow.WindowState = WindowState.Minimized;
            });

            MaximizeCommand = new RelayCommand(() =>
            {
                Application.Current.MainWindow.WindowState = WindowState.Maximized;
            });

            CloseCommand = new RelayCommand(() =>
            {
                Application.Current.MainWindow.Close();
            });

        }

    }
}
