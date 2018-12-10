using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Controls;

namespace MarathonSkills
{

    public class PageService : IPageService, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private BasePage _currentPage;
        public BasePage CurrentPage
        {
            get => _currentPage;
            set
            {
                _currentPage = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentPage)));
            }
        }

        private Stack<BasePage> _pagesHistory;
        public Stack<BasePage> PagesHistory
        {
            get => _pagesHistory;
            set
            {
                _pagesHistory = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PagesHistory)));
            }
        }

        private bool _isCanBack;
        public bool IsCanBack
        {
            get => _isCanBack;
            private set
            {
                _isCanBack = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsCanBack)));
            }
        }

        public PageService()
        {
            PagesHistory = new Stack<BasePage>();
        }

        private void PushPageHistroy(BasePage page)
        {
            if (PagesHistory == null)
                return;
            PagesHistory.Push(page);
            IsCanBack = true;
        }

        private BasePage PopPageHistroy()
        {
            if (PagesHistory == null || PagesHistory.Count == 0)
            {
                IsCanBack = false;
                return null;
            }
            var page = PagesHistory.Pop();
            if (PagesHistory.Count == 0)
            {
                IsCanBack = false;
            }
            return page;
        }

        public async void GoToPage(ApplicationPage page)
        {
            switch (page)
            {
                case ApplicationPage.HomePage:
                    if (CurrentPage != null)
                    {
                        await CurrentPage.SlideAndFadeOutToLeftAsync(0.4f);
                        PushPageHistroy(CurrentPage);
                    }
                    
                    CurrentPage = new HomePage();
                    CurrentPage.DataContext = BootStrapper.Resolve<HomePageViewModel>();
                    break;
                case ApplicationPage.SponsorPage:
                    if (CurrentPage != null)
                    {
                        await CurrentPage.SlideAndFadeOutToLeftAsync(0.4f);
                        PushPageHistroy(CurrentPage);
                    }
                    CurrentPage = new SponsorPage();
                    CurrentPage.DataContext = BootStrapper.Resolve<SponsorPageViewModel>();
                    break;
                case ApplicationPage.RunnerPage:
                    if (CurrentPage != null)
                    {
                        await CurrentPage.SlideAndFadeOutToLeftAsync(0.4f);
                        PushPageHistroy(CurrentPage);
                    }
                    CurrentPage = new RunnerPage();
                    CurrentPage.DataContext = BootStrapper.Resolve<RunnerPageViewModel>();
                    break;
                default:
                    break;
            }
        }

        public async void GoBack()
        {
            if (PagesHistory != null || PagesHistory.Count != 0)
            {
                CurrentPage = PopPageHistroy();
            }
        }


    }
}
