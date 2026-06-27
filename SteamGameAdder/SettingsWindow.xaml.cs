using System.Windows;
using System.Windows.Forms;
using SteamGameAdder.Services;

namespace SteamGameAdder
{
    public partial class SettingsWindow : Window
    {
        private ConfigService _configService;
        private SteamService _steamService;

        public SettingsWindow(ConfigService configService, SteamService steamService)
        {
            InitializeComponent();
            _configService = configService;
            _steamService = steamService;
            SteamPathInput.Text = _configService.GetSteamPath();
        }

        private void BrowseFolder_Click(object sender, RoutedEventArgs e)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                dialog.Description = "Steam dizinini seçin";
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    SteamPathInput.Text = dialog.SelectedPath;
                }
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            _configService.SetSteamPath(SteamPathInput.Text);
            _steamService.SetSteamPath(SteamPathInput.Text);
            MessageBox.Show("✓ Ayarlar kaydedildi!", "Başarı", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
