using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Forms;
using System.IO;
using SteamGameAdder.Models;
using SteamGameAdder.Services;

namespace SteamGameAdder
{
    public partial class MainWindow : Window
    {
        private SteamService _steamService;
        private ConfigService _configService;
        private ObservableCollection<GameEntry> _games;

        public MainWindow()
        {
            InitializeComponent();
            _steamService = new SteamService();
            _configService = new ConfigService();
            _games = new ObservableCollection<GameEntry>();
            GamesList.ItemsSource = _games;
            RefreshGameList();
            UpdateStatus($"Steam Yolu: {_steamService.GetSteamPathValue()}");
        }

        private void BrowseFolder_Click(object sender, RoutedEventArgs e)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                dialog.Description = "Oyun klasörünü seçin";
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    GamePathInput.Text = dialog.SelectedPath;
                    DetectExecutable();
                }
            }
        }

        private void BrowseIcon_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Icon Files (*.ico)|*.ico|All Files (*.*)|*.*";
            dialog.Title = "Icon dosyasını seçin";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                IconPathInput.Text = dialog.FileName;
            }
        }

        private void DetectExecutable()
        {
            try
            {
                string path = GamePathInput.Text;
                if (Directory.Exists(path))
                {
                    string[] exeFiles = Directory.GetFiles(path, "*.exe", SearchOption.TopDirectoryOnly);
                    if (exeFiles.Length > 0)
                    {
                        ExecutableInput.Text = Path.GetFileName(exeFiles[0]);
                    }
                }
            }
            catch { }
        }

        private void AddGame_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Validasyon
                if (string.IsNullOrWhiteSpace(GameNameInput.Text))
                {
                    MessageBox.Show("Oyun adı boş olamaz!", "Uyarı", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(GamePathInput.Text) || !Directory.Exists(GamePathInput.Text))
                {
                    MessageBox.Show("Oyun klasörü seçilmemiş veya geçersiz!", "Uyarı", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!int.TryParse(AppIdInput.Text, out int appId))
                {
                    MessageBox.Show("Geçerli bir App ID gir! (Örn: 271590)", "Uyarı", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(ExecutableInput.Text))
                {
                    MessageBox.Show("Executable adı boş olamaz!", "Uyarı", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Oyun nesnesi oluştur
                var game = new GameEntry
                {
                    Name = GameNameInput.Text,
                    AppId = appId,
                    InstallPath = GamePathInput.Text,
                    Executable = ExecutableInput.Text,
                    Icon = IconPathInput.Text
                };

                // Steam'e ekle
                if (_steamService.AddGameToSteam(game))
                {
                    _configService.AddGame(game);
                    RefreshGameList();
                    UpdateStatus($"✓ {game.Name} Steam'e eklendi! (Steam'i yeniden başlat)");
                    MessageBox.Show($"✓ {game.Name} başarıyla Steam'e eklendi!\n\nSteam'i yeniden başlatarak oyunu görebilirsiniz.",
                        "Başarı", MessageBoxButton.OK, MessageBoxImage.Information);
                    ClearInputs();
                }
                else
                {
                    UpdateStatus($"✗ {game.Name} eklenirken hata oluştu!");
                    MessageBox.Show($"✗ {game.Name} eklenirken hata oluştu!\n\nLütfen:\n- Oyun klasörünün doğru olduğunu kontrol et\n- Executable dosyasının var olduğunu kontrol et\n- Admin haklarında çalıştırıyor musun?",
                        "Hata", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RemoveGame_Click(object sender, RoutedEventArgs e)
        {
            if (GamesList.SelectedItem is GameEntry game)
            {
                if (MessageBox.Show($"{game.Name} silinsin mi?", "Onayla", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    if (_steamService.RemoveGameFromSteam(game))
                    {
                        _configService.RemoveGame(game);
                        RefreshGameList();
                        UpdateStatus($"✓ {game.Name} Steam'den kaldırıldı!");
                        MessageBox.Show($"✓ {game.Name} kaldırıldı!", "Başarı", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show($"✗ {game.Name} kaldırılırken hata oluştu!", "Hata", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Lütfen silmek istediğiniz oyunu seçin!", "Uyarı", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void RefreshGameList()
        {
            _games.Clear();
            foreach (var game in _configService.GetAllGames())
            {
                _games.Add(game);
            }
            UpdateStatus($"📋 {_games.Count} oyun listele");
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            ClearInputs();
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            RefreshGameList();
            UpdateStatus("✓ Liste yenilendi");
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            var settingsWindow = new SettingsWindow(_configService, _steamService);
            settingsWindow.ShowDialog();
        }

        private void ClearInputs()
        {
            GameNameInput.Clear();
            GamePathInput.Clear();
            AppIdInput.Clear();
            ExecutableInput.Text = "game.exe";
            IconPathInput.Clear();
        }

        private void UpdateStatus(string message)
        {
            StatusText.Text = message;
        }
    }
}
