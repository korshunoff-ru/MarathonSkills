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
    
        int Seconds { get; }
        int Minutes { get; }
        int Hours { get; }
        int Days { get; }

        DateTime DateStart { get; }

        void Start(DateTime dateStart);
        void Stop();

    }

    public class TimerService : ITimerService
    {
        public DateTime DateStart { get; set; }

        private DispatcherTimer _dispatcherTimer;

        private int _seconds;
        public int Seconds
        {
            get => _seconds;
            set
            {
                _seconds = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Seconds)));
            }
        }

        private int _minutes;
        public int Minutes
        {
            get => _minutes;
            set
            {
                _minutes = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Minutes)));
            }
        }

        private int _hours;
        public int Hours
        {
            get => _hours;
            set
            {
                _hours = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Hours)));
            }
        }

        private int _days;
        public int Days
        {
            get => _days;
            set
            {
                _days = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Days)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void Start(DateTime dateStart)
        {

            DateStart = dateStart;

            _dispatcherTimer = new DispatcherTimer();
            _dispatcherTimer.Tick += (sender, e) =>
            {
                TimeSpan timeDifference = DateStart - DateTime.Now;

                Days = Convert.ToInt32(timeDifference.TotalDays);
                Hours = Convert.ToInt32(timeDifference.TotalHours % 24);
                Minutes = Convert.ToInt32(timeDifference.TotalMinutes % 60);
                Seconds = Convert.ToInt32(timeDifference.TotalSeconds % 60);

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
