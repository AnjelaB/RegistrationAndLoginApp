using System;
using System.ComponentModel;
using System.Configuration;
using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Newtonsoft.Json;
using Registration.Commands;
using Registration.DataModel;

namespace Registration.ViewModels
{
    class RegistrationViewModel:INotifyPropertyChanged
    {
        private string firstName;
        private string lastName;
        private string login;
        private string password;
        private string confirmPassword;
        private string errorMessage;

        public string FirstName
        {
            get
            {
                return this.firstName;
            }
            set
            {
                this.firstName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("FirstName"));
            }
        }

        public string LastName
        {
            get
            {
                return this.lastName;
            }
            set
            {
                this.lastName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("LastName"));
            }
        }

        public string Login
        {
            get
            {
                return this.login;
            }
            set
            {
                this.login = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Login"));
            }
        }

        public string Password
        {
            get
            {
                return this.password;
            }
            set
            {
                this.password = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Password"));
            }
        }

        public string ConfirmPassword
        {
            get
            {
                return this.confirmPassword;
            }
            set
            {
                this.confirmPassword = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ConfirmPassword"));
            }
        }

        public string ErrorMessage
        {
            get
            {
                return this.errorMessage;
            }
            set
            {
                this.errorMessage = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ErrorMessage"));
            }
        }

        public ICommand RegisterCommand { get; set; }

        public RegistrationViewModel()
        {
            this.RegisterCommand = new Command(o => UserRegistration());
        }

        public void UserRegistration()
        {
            if (UserValidation())
            {
                HttpClient httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(ConfigurationSettings.AppSettings["userManagementBaseUri"]);
                var user = new User()
                {
                    FirstName = this.FirstName,
                    LastName = this.LastName,
                    Login = this.Login,
                    Password = this.Password
                };
                var requestResult = httpClient.PostAsync("api/Users", new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json")).Result;
                if (requestResult.IsSuccessStatusCode)
                {
                    var content = requestResult.Content;
                    var jsonContent = content.ReadAsStringAsync().Result;

                    var window = Application.Current.MainWindow;
                    var presenter = window.FindName("RegistrationPresent") as ContentPresenter;
                    presenter.ContentTemplate = window.FindResource("Login") as DataTemplate;
                }
                else
                {
                    this.ErrorMessage = "Please, try again.";
                }
            }
        }

        public bool UserValidation()
        {
            if (this.FirstName == null || this.FirstName == "")
            {
                this.ErrorMessage = "Please enter your first name.";
                return false;
            }
            else if (this.LastName == null || this.LastName == "")
            {
                this.ErrorMessage = "Please enter your last name.";
                return false;
            }
            else if(this.Login==null || this.Login == "")
            {
                this.ErrorMessage = "Please enter login.";
                return false;
            }
            else if(this.Password==null || this.Password == "")
            {
                this.ErrorMessage = "Enter password.";
                return false;
            }
            else if(this.ConfirmPassword==null || this.ConfirmPassword == "")
            {
                this.ErrorMessage = "Enter confirm password.";
                return false;
            }
            else if (this.ConfirmPassword != this.Password)
            {
                this.ErrorMessage = "Enter correct password.";
                return false;
            }
            else if (LoginValidation())
            {
                this.ErrorMessage = "That login already exists.";
                return false;
            }
            return true;
        }

        public bool LoginValidation()
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(ConfigurationSettings.AppSettings["userManagementBaseUri"]);
                var requestResult = httpClient.GetAsync($"api/UserVerification/{login}").Result;
                if (requestResult.IsSuccessStatusCode)
                    return true;
                return false;
            }
            catch
            {
                throw new Exception("There is something wrong.");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
