using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reactive.Disposables;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace MarathonSkills
{

    public class BaseViewModel : INotifyPropertyChanged
    {

        public IPageService PageService { get; private set; }
        public Page CurrentPage { get => PageService.CurrentPage; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public BaseViewModel(IPageService pageService)
        {
            PageService = pageService;

        }

    }
}
