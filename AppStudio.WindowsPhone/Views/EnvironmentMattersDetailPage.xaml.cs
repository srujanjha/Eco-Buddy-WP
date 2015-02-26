using System;
using System.Net.NetworkInformation;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.ApplicationModel.DataTransfer;

using AppStudio.Services;
using AppStudio.ViewModels;

namespace AppStudio.Views
{
    public sealed partial class EnvironmentMattersDetail : Page
    {
        private NavigationHelper _navigationHelper;

        private DataTransferManager _dataTransferManager;

        public EnvironmentMattersDetail()
        {
            this.InitializeComponent();
            _navigationHelper = new NavigationHelper(this);
        }

        public EnvironmentMattersViewModel EnvironmentMattersModel { get; private set; }

        public NavigationHelper NavigationHelper
        {
            get { return _navigationHelper; }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _dataTransferManager = DataTransferManager.GetForCurrentView();
            _dataTransferManager.DataRequested += OnDataRequested;

            _navigationHelper.OnNavigatedTo(e);

            EnvironmentMattersModel = NavigationServices.CurrentViewModel as EnvironmentMattersViewModel;
            if (EnvironmentMattersModel != null)
            {
                EnvironmentMattersModel.ViewType = ViewTypes.Detail;
            }
            DataContext = this;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            _navigationHelper.OnNavigatedFrom(e);
            _dataTransferManager.DataRequested -= OnDataRequested;
        }

        private void OnDataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            if (EnvironmentMattersModel != null)
            {
                EnvironmentMattersModel.GetShareContent(args.Request);
            }
        }
    }
}
