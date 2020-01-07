using Ninject;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Vault.Core.Models;
using Vault.Security.Random;

namespace Vault
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private readonly IProfileRepository _profileRepository;

        private readonly IPasswordRepository _passwordRepository;

        [Inject]
        public LoginWindow(IProfileRepository profileRepository, IPasswordRepository passwordRepository)
        {
            _profileRepository = profileRepository;
            _passwordRepository = passwordRepository;

            InitializeComponent();

            transitioner.SelectedIndex = 0;
        }

        private void CreateProfileButton_Click(object sender, RoutedEventArgs e)
        {
            if(ProfileNameTextBox.Text.Length <= 3)
            {
                MessageBox.Show("Profile name is too short!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if(PasswordBox.Password.Length <= 7)
            {
                MessageBox.Show("Password is too short!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var recoveryCode = RecoveryCodeGenerator.Instance.Generate();
            var profile = new Profile(ProfileNameTextBox.Text, PasswordBox.Password, recoveryCode);

            var recoveryCodeWindow = new RecoveryCodeWindow(ProfileNameTextBox.Text, recoveryCode);
            recoveryCodeWindow.ShowDialog();

            profile = _profileRepository.Add(profile);

            MessageBox.Show($"Added profile named: {profile.Name} with id {profile.Id.ToString()}");

            RefreshProfiles();

            transitioner.SelectedIndex = 0;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshProfiles();
        }

        private void RefreshProfiles()
        {
            ListView.Items.Clear();

            var allProfiles = _profileRepository.GetAll();
            foreach (var profile in allProfiles)
            {
                ListView.Items.Add(new { profile.Id, profile.Name });
            }
        }

        private void listViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = sender as ListViewItem;
            var id = (Guid)(item.Content as dynamic).Id;

            CheckPasswordAndOpenProfile(id);
        }

        private void RemoveSelectedButton_Click(object sender, RoutedEventArgs e)
        {
            var id = (Guid)(ListView.SelectedItem as dynamic).Id;
            var profile = _profileRepository.GetById(id);

            if (MessageBox.Show("Are you want to remove profile?", "Warning!", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                _profileRepository.Remove(profile);
                RefreshProfiles();
            }
        }

        private void OpenSelectedButton_Click(object sender, RoutedEventArgs e)
        {
            var id = (Guid)(ListView.SelectedItem as dynamic).Id;

            CheckPasswordAndOpenProfile(id);
        }

        private void CheckPasswordAndOpenProfile(Guid id)
        {
            var profile = _profileRepository.GetById(id);

            var passwordWindow = new ProfilePasswordWindow(profile);
            if (passwordWindow.ShowDialog() == true)
            {
                var mainWindow = new MainWindow(_passwordRepository, profile);
                mainWindow.ShowDialog();
            }
        }
    }
}
