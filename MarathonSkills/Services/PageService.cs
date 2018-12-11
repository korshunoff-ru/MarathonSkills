using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Threading;

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

        /// <summary>
        /// Pulls the last page of the stack
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Goes to the specified page
        /// </summary>
        /// <param name="page">Page to which you need to jump</param>
        public async void GoToPage(ApplicationPage page)
        {
            switch (page)
            {
                case ApplicationPage.HomePage:
                    if (CurrentPage != null)
                    {
                        await new Animation()
                            .AddSlide(AnimationSlide.SlideToBottom, CurrentPage.WindowHeight)
                            .AddFade(AnimationFade.FadeOut)
                            .StartAsync(CurrentPage);
                        PushPageHistroy(CurrentPage);
                    }
                    
                    CurrentPage = new HomePage();
                    CurrentPage.DataContext = BootStrapper.Resolve<HomePageViewModel>();
                    CurrentPage.Loaded += async (sender, e) =>
                    {
                        await new Animation()
                            .AddSlide(AnimationSlide.SlideFromTop, CurrentPage.WindowWidth)
                            .AddFade(AnimationFade.FadeIn)
                            .StartAsync(CurrentPage);
                    };
                    break;
                case ApplicationPage.SponsorPage:
                    if (CurrentPage != null)
                    {
                        await new Animation()
                            .AddSlide(AnimationSlide.SlideToBottom, CurrentPage.WindowHeight)
                            .AddFade(AnimationFade.FadeOut)
                            .StartAsync(CurrentPage);
                        PushPageHistroy(CurrentPage);
                    }
                    CurrentPage = new SponsorPage();
                    CurrentPage.DataContext = BootStrapper.Resolve<SponsorPageViewModel>();
                    CurrentPage.Loaded += async (sender, e) =>
                    {
                        await new Animation()
                            .AddSlide(AnimationSlide.SlideFromTop, CurrentPage.WindowWidth)
                            .AddFade(AnimationFade.FadeIn)
                            .StartAsync(CurrentPage);
                    };
                    break;
                case ApplicationPage.RunnerPage:
                    if (CurrentPage != null)
                    {
                        await new Animation()
                            .AddSlide(AnimationSlide.SlideToBottom, CurrentPage.WindowHeight)
                            .AddFade(AnimationFade.FadeOut)
                            .StartAsync(CurrentPage);
                        PushPageHistroy(CurrentPage);
                    }
                    CurrentPage = new RunnerPage();
                    CurrentPage.DataContext = BootStrapper.Resolve<RunnerPageViewModel>();
                    CurrentPage.Loaded += async (sender, e) =>
                    {
                        await new Animation()
                            .AddSlide(AnimationSlide.SlideFromTop, CurrentPage.WindowWidth)
                            .AddFade(AnimationFade.FadeIn)
                            .StartAsync(CurrentPage);
                    };
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Returns to the previous page
        /// </summary>
        public async void GoBack()
        {
            if (PagesHistory != null || PagesHistory.Count != 0)
            {

                await new Animation()
                   .AddSlide(AnimationSlide.SlideToTop, CurrentPage.WindowHeight)
                   .AddFade(AnimationFade.FadeOut)
                   .StartAsync(CurrentPage);

                CurrentPage = PopPageHistroy();

                CurrentPage.Loaded += async (senderTwo, eTwo) =>
                {
                    await new Animation()
                        .AddSlide(AnimationSlide.SlideFromBottom, CurrentPage.WindowWidth)
                        .AddFade(AnimationFade.FadeIn)
                        .StartAsync(CurrentPage);
                };


            }
        }


    }
}
