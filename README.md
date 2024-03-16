# Scopa Oyunu

Bu proje, C# programlama dili kullanılarak konsol üzerinde oynanabilen bir Scopa oyununu içerir. Scopa, İtalyan bir kart oyunudur ve geleneksel bir İtalyan destesi kullanılarak oynanır.

# Proje Yapısı

- **Program.cs**: Oyunun ana çalıştırılabilir dosyasıdır. Oyunun başlatılması ve temel oyun döngüsü burada bulunur.
- **Player.cs**: Oyuncu sınıfını tanımlar. Oyuncunun özellikleri (adı, eldeki kartlar, kazanılan kartlar vb.) ve davranışları bu sınıfta bulunur.
- **Game.cs**: Oyun sınıfını tanımlar. Oyunun kurallarını, tur döngüsünü, kart dağıtımını ve puan hesaplamalarını yönetir.

# **Nasıl Oynanır**

### Oyunun Başlangıcı

1. **Oyuncu Sayısını Belirleme**: İlk adım olarak, oyunun başlatılacağı oyuncu sayısını belirleyin. Genellikle Scopa'da 2 veya 4 oyuncu yer alır.
2. **Oyuncu İsimlerini Girme**: Oyuncu sayısını belirledikten sonra, her oyuncunun adını girmeniz istenir. Bu isimler oyun sırasında kullanılacaktır.

### Kart Dağıtımı

1. **Kart Dağıtımı**: Oyunculara sırasıyla kartlar dağıtılır. Her oyuncuya başlangıçta 3 kart verilir. Ardından, masaya da 4 kart açık olarak bırakılır.

### Oyun Turu

1. **Sıra Kimdeyse O Kartı Seçme**: Oyun sırası hangi oyuncuda ise, o oyuncu sırasıyla masadaki açık kartlardan birini seçer. Eğer elindeki kartla masadaki kartların toplamı 10'a ulaşıyorsa, oyuncu bu kartları alabilir.
2. **Kart Seçme ve Yutma**: Oyuncunun seçtiği kart, masadaki kartlarla toplamı 10 olan bir kombinasyon oluşturuyorsa, oyuncu bu kartları yutar ve eline alır.
3. **Masadaki Kartları Alarak Puan Kazanma**: Bir oyuncu masadaki tüm kartları alırsa, "Scopa" yapmış olur ve bu oyuncu ekstra bir puan kazanır. Ayrıca masadaki tüm kartları almak, masada kalan tüm kartları almak anlamına gelir.
4. **Yeni Kartlar Atma**: Bir oyuncu kartları yuttuktan veya masadaki tüm kartları aldıktan sonra, her oyuncunun elindeki kartlar tamamlanır. Ardından, yeni kartlar dağıtılır ve bir sonraki tur başlar.

### Oyunun Sonu ve Puan Hesaplama

1. **Kartlar Bittiğinde Oyunun Sonu**: Kartlar bitene kadar oyun devam eder. Kartlar bittikten sonra, her oyuncunun elindeki kartlar ve masada kalan kartlar üzerinden puanlar hesaplanır.
2. **Puanlama**: Her oyuncunun elindeki kartlara ve masadaki kalan kartlara göre puanlar hesaplanır. Genellikle belirli kart kombinasyonları (örneğin, değerli kartları yakalama, carico, primiera vb.) belirli puanlar kazandırır.
3. **Kazanan Belirleme**: Puanlar toplandıktan sonra, en yüksek puana sahip olan oyuncu oyunun galibi ilan edilir.

### Sonraki Oyunlar

1. **Yeni Oyun Başlatma**: Oyunun galibi belirlendikten sonra, isterseniz yeni bir oyun başlatabilirsiniz. Yeni oyun başlatmak için aynı adımları takip edin.

Scopa oyunu, bu temel adımların tekrarıyla oynanır. Her oyunun sonucu oyuncuların stratejilerine ve kartları nasıl oynadıklarına bağlı olarak değişebilir. Oyunu daha iyi anlamak ve ustalaşmak için pratik yapmak önemlidir. İyi eğlenceler!

# Fonksiyonlar ve Açıklamaları

- **OyuncuKartSecVeAt**: Oyuncunun kart seçmesini ve masaya atmasını sağlar. Masada uygun durum yoksa kartı masaya atar.
- **KartYutma**: Oyuncunun seçtiği kart ile masada sağlanan durumları kontrol eder ve gerektiğinde oyuncunun kazandığı kartları yönetir.
- **SaglayanDurumlariBul**: Verilen listedeki sayılar arasından belirli bir hedef sayıya ulaşan kombinasyonları bulur.
- **OyunSonuPuanHesapla**: Oyun sonunda oyuncuların puanlarını hesaplar ve belirli koşullara göre puanları günceller.
- **PuanaGoreSirala**: Oyuncuları puanlarına göre sıralar. Eğer iki oyuncu aynı puana sahipse, önceki sıradaki oyuncu, sonraki sıradaki oyuncudan önce gelir.
- **SiralamayiYazdir**: Oyuncuları puanlarına göre sıralar ve ekrana yazar. Aynı puanı alan oyuncular arasında sıralama yapar ve renk değişimi yapar.

# Nasıl Çalıştırılır

1. Projenin dosyalarını bir C# uyumlu geliştirme ortamında (örneğin, Visual Studio) açın.
2. Proje derlenir ve çalıştırılır.
3. Konsol ekranında oyunun adımlarını takip ederek oyun oynanır.

# Katkıda Bulunma

Eğer bir hata bulursanız veya yeni özellikler eklemek isterseniz, lütfen GitHub'da bir "issue" açın veya bir "pull request" gönderin. Katkılarınızı memnuniyetle karşılarım..