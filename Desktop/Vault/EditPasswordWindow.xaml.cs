using System.Windows;
using Vault.Core.Dtos;

namespace Vault
{
    /// <summary>
    /// Interaction logic for EditPasswordWindow.xaml
    /// </summary>
    public partial class EditPasswordWindow : Window
    {
        private PasswordDto _passwordDto;

        public EditPasswordWindow(PasswordDto passwordDto)
        {
            _passwordDto = passwordDto;

            InitializeComponent();

            NameTextBox.Text = _passwordDto.Name;
            EmailTextBox.Text = _passwordDto.Email;
            PasswordBox.Password = _passwordDto.Password;
            DescriptionTextBox.Text = _passwordDto.Description;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            _passwordDto.Name = NameTextBox.Text;
            _passwordDto.Email = EmailTextBox.Text;
            _passwordDto.Password = PasswordBox.Password;
            _passwordDto.Description = DescriptionTextBox.Text;

            DialogResult = true;
            Close();
        }
    }
}
