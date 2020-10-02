using Microsoft.Extensions.DependencyInjection;
using Movies.UI.Pages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Movies.UI.Navigation
{
    public class PageNavigator
    {
        private readonly IServiceProvider _serviceProvider;
        public PageNavigator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public void GoTo(PageType pageType)
        {
            MainWindow mainWindown = _serviceProvider.GetRequiredService<MainWindow>();
            Page page = null;
            switch (pageType)
            {
                case PageType.Favorite:
                    page = _serviceProvider.GetRequiredService<FavoritePage>();
                    break;
                case PageType.Search:
                    page = _serviceProvider.GetRequiredService<SearchPage>();
                    break;
            }

            mainWindown.Content = page;
        }
    }
}
