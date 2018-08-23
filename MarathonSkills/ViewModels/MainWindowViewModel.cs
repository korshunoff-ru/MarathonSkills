using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace MarathonSkills
{
    public class MainWindowViewModel : BaseViewModel
    {

        #region Private Fields

        private ApplicationPage _currentPage = ApplicationPage.HomePage;
        private Window _window;
        /// <summary>
        /// Time to start Marathon Skills 2018
        /// </summary>
        private DateTime _timeToStart = new DateTime(2018, 10, 21, 9, 0, 0);
        private string _footerText;

        #endregion

        #region Public Properties

        public ApplicationPage CurrentPage
        {
            get => _currentPage;
            set
            {
                _currentPage = value;
                OnPropertyChanged();
            }
        }

        public string FooterText
        {
            get => _footerText;
            set
            {
                _footerText = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public MainWindowViewModel(Window window)
        {
            _window = window;

            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += Timer_Tick;
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            TimeSpan timeToStart = _timeToStart - DateTime.Now;
            FooterText = timeToStart.Days + " дней " + timeToStart.Hours + " часов и " + timeToStart.Minutes + " минут до старта марафона!";
        }

        #endregion

    }
}
