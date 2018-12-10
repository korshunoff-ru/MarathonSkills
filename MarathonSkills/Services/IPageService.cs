using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Controls;

namespace MarathonSkills
{
    public interface IPageService
    {
        BasePage CurrentPage { get; }
        bool IsCanBack { get; }
        Stack<BasePage> PagesHistory { get; }
        void GoToPage(ApplicationPage page);
        void GoBack();
    }
}