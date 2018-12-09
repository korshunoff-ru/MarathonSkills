using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Controls;

namespace MarathonSkills
{
    public interface IPageService
    {
        Page CurrentPage { get; }
        void GoToPage(ApplicationPage page);
    }
}