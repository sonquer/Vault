using System.Windows;
using Vault.Core.Models;

namespace Vault
{
    /// <summary>
    /// Interaction logic for ProfilePasswordWindow.xaml
    /// </summary>
    public partial class ProfilePasswordWindow : Window
    {
        private readonly Profile _profile;

        public ProfilePasswordWindow(Profile profile)
        {
            InitializeComponent();

            _profile = profile;
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if(!_profile.PasswordIsValid(PasswordBox.Password))
            {
                MessageBox.Show("Invalid password!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            //TODO
        }
    }
}
