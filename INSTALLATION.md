# 📖 Kurulum Rehberi

## 📋 Gereksinimler
- Windows 7, 8, 10, 11
- .NET 6.0 Runtime (otomatik kurulur)
- Steam kurulu
- Admin hakları

## 📥 İndirme Seçenekleri

### 1️⃣ Portable (Tavsiye Edilen)
```
1. SteamGameAdder-Portable.zip indir
2. Herhangi bir klasöre çıkart
3. SteamGameAdder.exe çalıştır
4. Hazır! Kurulum yok.
```

### 2️⃣ Installer (Masaüstü Shortcut)
```
1. SteamGameAdder-Setup.exe indir
2. Çalıştır ve kur
3. Masaüstündeki shortcut'tan başlat
4. Otomatik güncellemeler
```

## 🚀 İlk Kullanım

### Adım 1: Uygulamayı Başlat
```
SteamGameAdder.exe dosyasına çift tıkla
```

### Adım 2: Oyun Bilgilerini Gir

**Zorunlu alanlar:**
- 📝 **Oyun Adı** - Örn: "Grand Theft Auto V"
- 📁 **Oyun Klasörü** - game.exe'nin olduğu yol
- 🔢 **Steam App ID** - Oyunun Steam ID'si
- 📄 **Executable** - Ana executable dosyası (örn: GTA5.exe)

**İsteğe Bağlı:**
- 🖼️ **Icon** - Oyun ikonu (.ico dosyası)

### Adım 3: App ID Bul

**Yol 1:** SteamDB (En hızlı)
- https://steamdb.info git
- Oyun adını ara
- "App ID" sütunundan kopyala

**Yol 2:** Google
- "[Oyun Adı] steam app id" ara
- İlk sonuçtan kopyala

**Yol 3:** Steam URL'den
- Steam'de oyunun sayfasını aç
- URL'deki "app/[ID]" kısmını bul
- Örn: store.steampowered.com/app/271590 → ID: 271590

### Adım 4: Oyun Ekle

1. Tüm bilgileri doldur
2. "➕ STEAM'E EKLE" butonuna tıkla
3. Başarı mesajı bekle
4. Steam'i **tamamen kapat ve yeniden aç**
5. Oyun kütüphanesinde görüntülenecek!

## ⚙️ Ayarlar

### Steam Yolunu Değiştir
1. "⚙️ Ayarlar" butonuna tıkla
2. Steam dizinini yazı kutusuna gir
3. Veya "📁 Seç" ile klasörü seç
4. "✓ Kaydet"'e tıkla

**Varsayılan yollar:**
- Windows (32-bit): `C:\Program Files (x86)\Steam`
- Windows (64-bit): `C:\Program Files\Steam`
- Başka disk: `D:\Steam` vb.

## 🎮 Örnek - GTA V Ekleme

```
Oyun Adı: Grand Theft Auto V
Oyun Klasörü: D:\Games\GTA5
Steam App ID: 271590
Executable: GTA5.exe
Icon: (Opsiyonel)

[Tıkla: ➕ STEAM'E EKLE]

✓ Başarıyla eklendi!

[Steam'i yeniden başlat]

✅ Oyun kütüphanede görunüyor!
```

## 🔧 Sorun Giderme

### Problem: "Executable not found"
**Çözüm:**
- Oyun klasörü doğru mu?
- game.exe dosyası orada var mı?
- Dosya adını kontrol et (büyük-küçük harf sensitive olabilir)

### Problem: "Admin rights required"
**Çözüm:**
1. Uygulamayı kapat
2. SteamGameAdder.exe'ye sağ tıkla
3. "Run as Administrator" seç
4. "Yes" e tıkla

### Problem: Oyun Steam'de görünmüyor
**Çözüm:**
1. Steam'i tamamen kapat (System Tray'den da)
2. SteamGameAdder'ı tekrar aç
3. Oyun seç ve Refresh butonuna tıkla
4. Steam'i yeniden aç

### Problem: "Steam dizini bulunamadı"
**Çözüm:**
1. "⚙️ Ayarlar"'a git
2. Steam yolunu manuel gir
3. "📁 Seç" ile klasörü seç
4. "✓ Kaydet"'e tıkla

## 📋 Popüler Oyunlar ve App ID'leri

| Oyun | App ID |
|------|--------|
| GTA V | 271590 |
| GTA Online | 271590 |
| Cyberpunk 2077 | 1091500 |
| Elden Ring | 2778580 |
| Minecraft Java | 1086210 |
| The Witcher 3 | 292030 |
| Portal 2 | 620 |
| Half-Life 2 | 220 |
| Counter-Strike 2 | 730 |
| PUBG: Battlegrounds | 578080 |

## 💡 İpuçları

✅ **Yedekleme**
- `games.json` dosyasını yedekle
- Dosyayı başka bilgisayara kopyalayarak oyun listesini taşı

✅ **Batch İşlemler**
- Birden fazla oyun eklemek için işlemi tekrarla
- Tüm oyunlar otomatik olarak kaydedilir

✅ **Güvenlik**
- Sadece kendi oyunlarını ekle
- App ID doğru olduğundan emin ol
- Steam DRM hakkında bilgi al

## 🔗 Faydalı Linkler

- SteamDB: https://steamdb.info
- Steam App ID Finder: https://steamid.io
- Steam Community: https://steamcommunity.com

---

**Sorular?** [GitHub Issues](https://github.com/securtiyshoop-bot/SteamGameAdder/issues) aç!
