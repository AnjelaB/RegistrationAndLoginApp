using System;
using System.ComponentModel;
using System.Configuration;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using IdentityModel.Client;
using Newtonsoft.Json;
using Registration.Commands;
using Registration.DataModel;

namespace Registration.ViewModels
{
    class LoginViewModel:INotifyPropertyChanged
    {
        private string errorMessage;
        private string login;
        private string password;
        private DiscoveryResponse disco;
        private TokenClient tokenClient;
        private Visibility statusVisibility;
        private Visibility retryConnectTextBlock;

        public Visibility RetryConnectTextBlock
        {
            get { return this.retryConnectTextBlock; }
            set
            {
                this.retryConnectTextBlock = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("RetryConnectTextBlock"));
            }
        }

        public string ErrorMessage
        {
            get { return this.errorMessage; }
            set
            {
                this.errorMessage = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ErrorMessage"));
            }
        }

        public string Login
        {
            get { return this.login; }
            set
            {
                this.login = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Login"));
            }
        }

        public string Password
        {
            get { return this.password; }
            set
            {
                this.password = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Password"));
            }
        }

        public Visibility StatusVisibility
        {
            get { return this.statusVisibility; }
            set
            {
                this.statusVisibility = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("StatusVisibility"));
            }
        }

        public ICommand LoginCommand { get; set; }
        public ICommand RetryConnectCommand { get; set; }

        public LoginViewModel()
        {
            this.LoginCommand = new Command(o => UserLogin());
            this.RetryConnectCommand = new Command(o => RetryConnect());
            ConnectToServerAsync();
        }

        public async void UserLogin()
        {
            StatusVisibility = Visibility.Collapsed;

            if (Login == "")
            {
                ErrorMessage = "Please enter the login.";
                StatusVisibility = Visibility.Visible;
                return;
            }

            if (Password == "")
            {
                ErrorMessage = "Please enter the password";
                StatusVisibility = Visibility.Visible;
                return;
            }
            // request token
            var tokenResponse = await tokenClient.RequestResourceOwnerPasswordAsync(Login, Password, "userRegistration offline_access");

            if (tokenResponse.IsError)
            {
                ErrorMessage = tokenResponse.ErrorDescription ?? tokenResponse.Error;
                StatusVisibility = Visibility.Visible;
                return;
            }
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(ConfigurationSettings.AppSettings["userManagementBaseUri"]);
            var requestResult = httpClient.GetAsync($"api/UserVerification/{login}").Result;
            if (requestResult.IsSuccessStatusCode)
            {
                var content = requestResult.Content;
                var jsonContent = content.ReadAsStringAsync().Result;
                var user = JsonConvert.DeserializeObject<User>(jsonContent);
                if (user != null)
                {
                    ConfigurationSettings.AppSettings["userId"] = user.UserId.ToString();
                }
                var window = Application.Current.MainWindow;
                var presenter = window.FindName("RegistrationPresent") as ContentPresenter;
                presenter.ContentTemplate = window.FindResource("UserProfile") as DataTemplate;
            }
            

        }

        public void RetryConnect()
        {
            ConnectToServerAsync();
        }

        public async void ConnectToServerAsync()
        {
            this.disco = DiscoveryClient.GetAsync(ConfigurationSettings.AppSettings["authenticationService"]).Result;

            if (disco.IsError)
            {
                ErrorMessage = "Internet connection problems,\nplease check your internet access";
                StatusVisibility = Visibility.Visible;
                RetryConnectTextBlock = Visibility.Visible;
            }
            else
            {
                tokenClient = new TokenClient(disco.TokenEndpoint, "userRegistrationDesktopApp", "secret");
                RetryConnectTextBlock = Visibility.Collapsed;
                

                ErrorMessage = "Connected";
                await Task.Run(() => Thread.Sleep(3000));
                StatusVisibility = Visibility.Hidden;

            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
