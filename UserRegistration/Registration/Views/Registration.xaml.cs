using System.Windows;
using System.Windows.Controls;
using Registration.ViewModels;

namespace Registration.Views
{
    /// <summary>
    /// Interaction logic for Registration.xaml
    /// </summary>
    public partial class Registration : UserControl
    {
        public Registration()
        {
            this.DataContext = new RegistrationViewModel();
            InitializeComponent();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)

        {

            // converting sender to Password box

            var passwordBox = (PasswordBox)sender;
            var dataContext = DataContext as RegistrationViewModel;


            // setting to view model fields

            if (passwordBox == this.password)

                dataContext.Password = passwordBox.Password;

            else
                dataContext.ConfirmPassword = passwordBox.Password;

        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            var window = Application.Current.MainWindow;
            var presenter = window.FindName("RegistrationPresent") as ContentPresenter;
            presenter.ContentTemplate = window.FindResource("Login") as DataTemplate;
        }
    }
}
