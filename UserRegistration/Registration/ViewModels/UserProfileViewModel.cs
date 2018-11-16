using System;
using System.ComponentModel;
using System.Configuration;
using System.Net.Http;
using Newtonsoft.Json;
using Registration.DataModel;

namespace Registration.ViewModels
{
    class UserProfileViewModel:INotifyPropertyChanged
    {
        private string userText;
        public string UserText
        {
            get { return this.userText; }
            set
            {
                this.userText = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("UserText"));
            }
        }
        public UserProfileViewModel()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(ConfigurationSettings.AppSettings["userManagementBaseUri"]);
            var requestResult = httpClient.GetAsync($"api/Users/{ConfigurationSettings.AppSettings["userId"]}").Result;
            if (requestResult.IsSuccessStatusCode)
            {
                var content = requestResult.Content;
                var jsonContent = content.ReadAsStringAsync().Result;
                var user = JsonConvert.DeserializeObject<User>(jsonContent);
                this.UserText = "Hi " + user.FirstName + " " + user.LastName + ". Have a nice day.";
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
