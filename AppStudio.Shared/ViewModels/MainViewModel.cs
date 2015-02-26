using System;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Net.NetworkInformation;

using Windows.UI.Xaml;

using AppStudio.Services;
using AppStudio.Data;

namespace AppStudio.ViewModels
{
    public class MainViewModel : BindableBase
    {
       private EnvironemtalNewsViewModel _environemtalNewsModel;
       private GoingGreenViewModel _goingGreenModel;
       private LivingGreenViewModel _livingGreenModel;
       private BackyardMattersViewModel _backyardMattersModel;
       private EnvironmentMattersViewModel _environmentMattersModel;
       private VideoTipsViewModel _videoTipsModel;
        private PrivacyViewModel _privacyModel;

        private ViewModelBase _selectedItem = null;

        public MainViewModel()
        {
            _selectedItem = EnvironemtalNewsModel;
            _privacyModel = new PrivacyViewModel();

        }
 
        public EnvironemtalNewsViewModel EnvironemtalNewsModel
        {
            get { return _environemtalNewsModel ?? (_environemtalNewsModel = new EnvironemtalNewsViewModel()); }
        }
 
        public GoingGreenViewModel GoingGreenModel
        {
            get { return _goingGreenModel ?? (_goingGreenModel = new GoingGreenViewModel()); }
        }
 
        public LivingGreenViewModel LivingGreenModel
        {
            get { return _livingGreenModel ?? (_livingGreenModel = new LivingGreenViewModel()); }
        }
 
        public BackyardMattersViewModel BackyardMattersModel
        {
            get { return _backyardMattersModel ?? (_backyardMattersModel = new BackyardMattersViewModel()); }
        }
 
        public EnvironmentMattersViewModel EnvironmentMattersModel
        {
            get { return _environmentMattersModel ?? (_environmentMattersModel = new EnvironmentMattersViewModel()); }
        }
 
        public VideoTipsViewModel VideoTipsModel
        {
            get { return _videoTipsModel ?? (_videoTipsModel = new VideoTipsViewModel()); }
        }

        public void SetViewType(ViewTypes viewType)
        {
            EnvironemtalNewsModel.ViewType = viewType;
            GoingGreenModel.ViewType = viewType;
            LivingGreenModel.ViewType = viewType;
            BackyardMattersModel.ViewType = viewType;
            EnvironmentMattersModel.ViewType = viewType;
            VideoTipsModel.ViewType = viewType;
        }

        public ViewModelBase SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                SetProperty(ref _selectedItem, value);
                UpdateAppBar();
            }
        }

        public Visibility AppBarVisibility
        {
            get
            {
                return SelectedItem == null ? AboutVisibility : SelectedItem.AppBarVisibility;
            }
        }

        public Visibility AboutVisibility
        {

         get { return Visibility.Visible; }
        }

        public void UpdateAppBar()
        {
            OnPropertyChanged("AppBarVisibility");
            OnPropertyChanged("AboutVisibility");
        }

        /// <summary>
        /// Load ViewModel items asynchronous
        /// </summary>
        public async Task LoadData(bool isNetworkAvailable)
        {
            var loadTasks = new Task[]
            { 
                EnvironemtalNewsModel.LoadItems(isNetworkAvailable),
                GoingGreenModel.LoadItems(isNetworkAvailable),
                LivingGreenModel.LoadItems(isNetworkAvailable),
                BackyardMattersModel.LoadItems(isNetworkAvailable),
                EnvironmentMattersModel.LoadItems(isNetworkAvailable),
                VideoTipsModel.LoadItems(isNetworkAvailable),
            };
            await Task.WhenAll(loadTasks);
        }

        /// <summary>
        /// Refresh ViewModel items asynchronous
        /// </summary>
        public async Task RefreshData(bool isNetworkAvailable)
        {
            var refreshTasks = new Task[]
            { 
                EnvironemtalNewsModel.RefreshItems(isNetworkAvailable),
                GoingGreenModel.RefreshItems(isNetworkAvailable),
                LivingGreenModel.RefreshItems(isNetworkAvailable),
                BackyardMattersModel.RefreshItems(isNetworkAvailable),
                EnvironmentMattersModel.RefreshItems(isNetworkAvailable),
                VideoTipsModel.RefreshItems(isNetworkAvailable),
            };
            await Task.WhenAll(refreshTasks);
        }

        //
        //  ViewModel command implementation
        //
        public ICommand RefreshCommand
        {
            get
            {
                return new DelegateCommand(async () =>
                {
                    await RefreshData(NetworkInterface.GetIsNetworkAvailable());
                });
            }
        }

        public ICommand AboutCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    NavigationServices.NavigateToPage("AboutThisAppPage");
                });
            }
        }

        public ICommand PrivacyCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    NavigationServices.NavigateTo(_privacyModel.Url);
                });
            }
        }
    }
}
