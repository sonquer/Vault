using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Vault.Core.Models;

namespace Vault
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IPasswordRepository _passwordRepository;

        private readonly Profile _profile;

        private readonly string _password;

        public MainWindow(IPasswordRepository passwordRepository, Profile profile, string password)
        {
            _passwordRepository = passwordRepository;
            _profile = profile;
            _password = password;

            InitializeComponent();

            RefreshPasswords();
        }

        private void RefreshPasswords()
        {
            ListView.Items.Clear();

            var allPasswords = _passwordRepository.GetByProfileId(_profile.Id);
            foreach (var password in allPasswords)
            {
                var passwordDto = password.GetPasswordDto(_password);
                ListView.Items.Add(new { password.Id, passwordDto.Name });
            }
        }

        private void NewPasswordButton_Click(object sender, RoutedEventArgs e)
        {
            var passwordDto = new Core.Dtos.PasswordDto();
            var passwordWindow = new EditPasswordWindow(passwordDto);
            if(passwordWindow.ShowDialog() == true)
            {
                _passwordRepository.Add(new Password(_profile.Id, passwordDto, _password));
                RefreshPasswords();
            }
        }

        private void listViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = sender as ListViewItem;
            var id = (Guid)(item.Content as dynamic).Id;

            var password = _passwordRepository.GetById(id);
            var passwordDto = password.GetPasswordDto(_password);

            var passwordWindow = new EditPasswordWindow(passwordDto);
            if (passwordWindow.ShowDialog() == true)
            {
                password.UpdatePasswordDto(passwordDto, _password);
                _passwordRepository.Update(password);

                RefreshPasswords();
            }
        }
    }
}
