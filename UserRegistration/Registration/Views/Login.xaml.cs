using System.Windows;
using System.Windows.Controls;
using Registration.ViewModels;

namespace Registration.Views
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : UserControl
    {
        public Login()
        {
            this.DataContext = new LoginViewModel();
            InitializeComponent();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            // converting sender to Password box

            var passwordBox = (PasswordBox)sender;
            var dataContext = DataContext as LoginViewModel;


            // setting to view model fields

            if (passwordBox == this.password)

                dataContext.Password = passwordBox.Password;

        }

        private void Registration_Click(object sender, RoutedEventArgs e)
        {
            var window = Application.Current.MainWindow;
            var presenter = window.FindName("RegistrationPresent") as ContentPresenter;
            presenter.ContentTemplate = window.FindResource("Registration") as DataTemplate;
        }
    }
}
