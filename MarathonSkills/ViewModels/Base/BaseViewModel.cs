using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MarathonSkills
{
    /// <summary>
    /// Base class with view model
    /// </summary>
    public class BaseViewModel : INotifyPropertyChanged
    {

        #region Public Events
        
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Methods

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

    }
}
