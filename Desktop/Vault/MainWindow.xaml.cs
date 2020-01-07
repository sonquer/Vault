using System.Windows;
using Vault.Core.Models;

namespace Vault
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IPasswordRepository _passwordRepository;

        private Profile _profile;

        public MainWindow(IPasswordRepository passwordRepository, Profile profile)
        {
            _passwordRepository = passwordRepository;
            _profile = profile;

            InitializeComponent();

            TextBox1.Text = $"{profile.Id}|{profile.Name}|{profile.PasswordHash}";
        }
    }
}
