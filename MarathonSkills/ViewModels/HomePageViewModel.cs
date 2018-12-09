namespace MarathonSkills
{

    public class HomePageViewModel : BaseViewModel
    {

        #region Commands

        public RelayCommand SponsorCommand { get; set; }
        public RelayCommand RunnerCommand { get; set; }

        #endregion

        public HomePageViewModel(IPageService pageService) : base(pageService)
        {
            SponsorCommand = new RelayCommand(() =>
            {
                PageService.GoToPage(ApplicationPage.SponsorPage);
            });

            RunnerCommand = new RelayCommand(() =>
            {
                PageService.GoToPage(ApplicationPage.RunnerPage);
            });

        }

    }

}
