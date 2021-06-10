using System;
using CityFinder.ViewModels;
using Xamarin.Forms;

namespace CityFinder
{
    public partial class MainPage : ContentPage
    {
        readonly MainPageViewModel _viewModel;

        public MainPage()
        {
            InitializeComponent();
            _viewModel = new MainPageViewModel();
            BindingContext = _viewModel;
        }
    }
}
