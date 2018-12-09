using System.ComponentModel;
using System.Windows.Controls;

namespace MarathonSkills
{

    public class PageService : IPageService, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private Page _currentPage;
        public Page CurrentPage
        {
            get => _currentPage;
            set
            {
                _currentPage = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentPage)));
            }
        }

        public async void GoToPage(ApplicationPage page)
        {
            switch (page)
            {
                case ApplicationPage.HomePage:
                    if (CurrentPage != null)
                    {
                        await CurrentPage.SlideAndFadeOutToLeftAsync(0.4f);
                    }
                    CurrentPage = new HomePage();
                    CurrentPage.DataContext = BootStrapper.Resolve<HomePageViewModel>();
                    break;
                case ApplicationPage.SponsorPage:
                    if (CurrentPage != null)
                    {
                        await CurrentPage.SlideAndFadeOutToLeftAsync(0.4f);
                    }
                    CurrentPage = new SponsorPage();
                    CurrentPage.DataContext = BootStrapper.Resolve<SponsorPageViewModel>();
                    break;
                case ApplicationPage.RunnerPage:
                    if (CurrentPage != null)
                    {
                        await CurrentPage.SlideAndFadeOutToLeftAsync(0.4f);
                    }
                    CurrentPage = new RunnerPage();
                    CurrentPage.DataContext = BootStrapper.Resolve<RunnerPageViewModel>();
                    break;
                default:
                    break;
            }
        }

    }
}
