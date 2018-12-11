using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace MarathonSkills
{
    public interface ITimerService : INotifyPropertyChanged
    {

        DateTime DateStart { get; }
        TimeSpan TimeToEnd { get; }

        void Start(DateTime dateStart);
        void Stop();

    }

    public class TimerService : ITimerService
    {
        public DateTime DateStart { get; set; }

        private DispatcherTimer _dispatcherTimer;

        private TimeSpan _timeToEnd;
        public TimeSpan TimeToEnd
        {
            get => _timeToEnd;
            private set
            {
                _timeToEnd = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TimeToEnd)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void Start(DateTime dateStart)
        {

            DateStart = dateStart;

            _dispatcherTimer = new DispatcherTimer();
            _dispatcherTimer.Tick += (sender, e) =>
            {
                TimeToEnd = DateStart - DateTime.Now;

            };
            _dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            _dispatcherTimer.Start();
        }

        public void Stop()
        {
            if (_dispatcherTimer != null)
            {
                _dispatcherTimer.Stop();
            }
        }
    }


}
