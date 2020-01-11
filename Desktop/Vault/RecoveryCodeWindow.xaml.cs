using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;

namespace Vault
{
    /// <summary>
    /// Interaction logic for RecoveryCodeWindow.xaml
    /// </summary>
    public partial class RecoveryCodeWindow : Window
    {
        private readonly string _recoveryCode;

        private readonly string _profileName;

        public RecoveryCodeWindow(string profileName, string recoveryCode)
        {
            InitializeComponent();

            _profileName = profileName;
            _recoveryCode = recoveryCode;

            RecoveryCodeTextBox.Text = recoveryCode;
        }

        private void SaveToFileButton_Click(object sender, RoutedEventArgs e)
        {
            var saveFileDialog = new SaveFileDialog
            {
                Filter = "Recovery code file (*.recf)|",
                FileName = $"Recovery - {_profileName}",
                DefaultExt = ".recf",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                RestoreDirectory = true
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                File.WriteAllText($"{Path.Combine(saveFileDialog.InitialDirectory, saveFileDialog.FileName)}.{saveFileDialog.DefaultExt}", 
                    _recoveryCode);
            }
        }

        private void CopyToClipboardButton_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(_recoveryCode);
        }
    }
}
