using System;
using System.Collections.Generic;
//doğruluğunu kontrol et
//sınıflar referans tipli oldukları için ref anahtar kelimesine gerek yoktur zaten referanslarını değişkene verirler(string hariç o bir istisnadır.)
namespace ScopaGame
{
    class Game
    {
        Player enSonKartYutanOyuncu;// Oyun sonunda masada kalan kartları en son yutan oyuncu alır.

        string[] kartTurleri = { "KARO" , "KUPA" , "MAÇA" , "SİNEK" };// 4 ile bölümünden kalan 0(0. index) ise karo 1 ise kupa 2 ise maça 3 ise sinektir.

        Random random;
        public int oyuncuSayisi;

        public List<int> MasadakiKartlar { get; set; }// Oyunun herhangi bir anında masada bulunan kartları tutar.
        public List<int> OyunDestesi { get; set; }// Oyunun başında rastgele oluşturulan 40 kartlık desteyi tutar.

        public Game()
        {
            this.random = new Random();
            this.MasadakiKartlar = new List<int>();
            this.OyunDestesi = new List<int>();

            RastgeleDesteOlustur();

        }

        // Kullanıcıdan oyuncu sayısını alır alınan oyuncu sayısına göre oyuncuları oluşturur ve oyuncuların adlarını alır.
        public Player[] OyuncuOlustur(Game oyun)
        {
            int oyuncuSayisi;

            while (true)
            {
                try
                {
                    Console.Write("Oynayacak oyuncu sayısını giriniz (2, 3 veya 4 olmalıdır): ");
                    oyuncuSayisi = Convert.ToInt32(Console.ReadLine());
                    if(oyuncuSayisi >= 2 && oyuncuSayisi <= 4)
                        break;
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Lütfen 2, 3 veya 4 oyuncu sayısı giriniz.");
                    }
                }
                catch (FormatException)
                {
                    Console.Clear();
                    Console.WriteLine("Lütfen geçerli bir sayı giriniz.");
                }
                catch (Exception ex)
                {
                    Console.Clear();
                    Console.WriteLine("Beklenmeyen hata oluştu: " + ex.Message);
                }
            }

            this.oyuncuSayisi = oyuncuSayisi;
            Player[] oyuncular = new Player[oyuncuSayisi];

            for (int i = 1; i <= oyuncuSayisi; i++)
            {
                Console.Write($"Oyuncu {i} adını giriniz: ");
                oyuncular[i - 1] = new Player(Console.ReadLine() ?? $"Oyuncu {i}");
            }

            return oyuncular;
        }

        // Sadece oyunun başında masaya 4 kart atar.Başka bir işlevi yoktur.
        public void MasayaKartAt()
        {
            for (int i = 0; i <= 3; i++)
            {
                this.MasadakiKartlar.Add(OyunDestesi[0]);
                OyunDestesi.RemoveAt(0);
            }
        }

        // Oyunculara her turda 3'er kart dağıtır.
        public void KartDagit(Player[] oyuncular)
        {
            if(OyunDestesi.Count != 0)
                for (int i = 0; i < oyuncuSayisi; i++)
                    for (int j = 0; j < 3; j++)// Her oyuncuya 3 kart dağıt
                    {
                        oyuncular[i].eldekiKartlar.Add(OyunDestesi[0]);
                        OyunDestesi.RemoveAt(0);
                    }
        }

        // Bu fonksiyon, 40 kartlık bir deste oluşturur ve her kartı rastgele sırayla ekler. Deste, 0'dan 39'a kadar olan sayıları içerir.
        public void RastgeleDesteOlustur()
        {
            // Deste oluşturulurken kullanılan bir liste oluşturulur.
            List<int> cikmayanlar = new List<int>();

            // 0'dan 39'a kadar olan sayılar liste içine eklenir. Bu sayılar, oluşturulacak deste için temel kart numaralarını temsil eder.
            for (int i = 0; i < 40; i++)
            {
                cikmayanlar.Add(i);
            }

            // Tüm kartlar dağıtılıncaya kadar döngü devam eder.
            while (cikmayanlar.Count != 0)
            {
                // Rastgele bir indeks seçilir. Bu indeks, cikmayanlar listesindeki kartların yerini belirler.
                int randomIndex = random.Next(0, cikmayanlar.Count);

                // Seçilen rastgele kart, oluşturulan deste listesine eklenir.
                OyunDestesi.Add(cikmayanlar[randomIndex]);

                // Eklenen kart, daha fazla kullanılmaması için cikmayanlar listesinden kaldırılır.
                cikmayanlar.RemoveAt(randomIndex);
            }
            /* Kart numarası ile kartın türünü belirlemek için kullanılan bir formül vardır. 
            Eğer kart numarası 4 ile bölümünden kalan 0 ise karo, 1 ise kupa, 2 ise maça, 3 ise sinek türündedir.
            Ayrıca, 4 ile bölümünden bölüme 1 eklenerek kartın değeri bulunur.
            Örneğin, 4 için 4 % 4 = 0 ve 4 / 4 = 1, bu değere 1 eklenir ve sonuç 2 olur. 
            Yani, 4 değeri karo 2'ye eşdeğerdir. */
        }


        // Masada bulunan kartları güzel bir şekilde ekrana yazdırır.
        public void MasayiGoster()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkGreen;

            Console.WriteLine("╔═══════════════════╗");
            Console.WriteLine("║  MASADAKİ KARTLAR ║");
            Console.WriteLine("╠═══════════════════╣");


            if (MasadakiKartlar.Count == 0)
                Console.WriteLine("║  MASADA KART YOK  ║");
            else
                for (int i = 0; i < MasadakiKartlar.Count; i++)
                {
                    // Kart türünü ve değerini güzel bir şekilde yazdırır.
                    string kart = $"{kartTurleri[MasadakiKartlar[i] % 4]} {MasadakiKartlar[i] / 4 + 1}";
                    Console.WriteLine($"║ {kart,-17} ║"); // Kartları sola hizalar.
                }

            Console.WriteLine("╚═══════════════════╝");
            Console.WriteLine();
            Console.ResetColor();
        }

        // Sıradaki oyuncunun elindeki kartları güzel bir şekilde ekrana yazdırır.
        public void OyuncuEliniGoster(Player oyuncu)
        {
            Console.WriteLine($"SIRADAKİ OYUNCU ---> {oyuncu.adi}");

            Console.WriteLine($"╔══════════════════════════╗");
            Console.WriteLine($"║      ELİNDEKİ KARTLAR     ║");
            Console.WriteLine("╠══════════════════════════╣");

            if (oyuncu.eldekiKartlar.Count == 0)
            {
                Console.WriteLine("║     ELİNDE KART YOK       ║");
            }
            else
            {
                for (int i = 0; i < oyuncu.eldekiKartlar.Count; i++)
                {
                    // Kart türünü ve değerini güzel bir şekilde yazdırır.
                    string kart = $"{kartTurleri[oyuncu.eldekiKartlar[i] % 4]} {oyuncu.eldekiKartlar[i] / 4 + 1}";
                    Console.WriteLine($"║ {kart,10} => {i + 1,10} ║"); // Kartları sağa hizalar.
                }
            }

            Console.WriteLine("╚══════════════════════════╝");
            Console.WriteLine();
        }

        /// <summary>
        /// Oyuncunun kart seçmesini ve masaya atmasını sağlar. Oyuncunun, seçtiği kartı masada uygun bir duruma getirip getiremeyeceğini kontrol eder ve gerekirse bu durumu yönetir.
        /// </summary>
        /// <param name="oyuncu">Kart seçecek oyuncu.</param>
        public void OyuncuKartSecVeAt(Player oyuncu)
        {
            int secilenKartIndex;

            // Oyuncunun geçerli bir kart seçmesini sağlar
            while (true)
            {
                while (true)
                {
                    MasayiGoster();
                    OyuncuEliniGoster(oyuncu);

                    Console.Write("Yazıp enter tuşuna basınız: ");

                    try
                    {
                        secilenKartIndex = Convert.ToInt32(Console.ReadLine());
                        break;
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Geçersiz kart numarası girdiniz. Lütfen tekrar deneyin.");
                    }
                }

                if (secilenKartIndex > 0 && secilenKartIndex <= oyuncu.eldekiKartlar.Count)
                    break;
                else
                    Console.WriteLine("Geçersiz kart numarası girdiniz. Lütfen tekrar deneyin.");
            }

            // Oyuncunun seçtiği kartın masada bulunan kartlarla sağlanan durumları bulur
            List<List<int>> saglayanDurumlar = SaglayanDurumlariBul(MasadakiKartlar, oyuncu.eldekiKartlar[secilenKartIndex - 1] / 4 + 1);

            // Eğer oyuncunun seçtiği kart, masadaki kartlarla hiçbir durumu sağlamıyorsa
            if (saglayanDurumlar.Count == 0)
            {
                Console.WriteLine("Seçtiğiniz kart ile yerden kart alamazsınız. Bu kartı masaya attınız.");
                MasadakiKartlar.Add(oyuncu.eldekiKartlar[secilenKartIndex - 1]);
                oyuncu.eldekiKartlar.RemoveAt(secilenKartIndex - 1);
                return; // Masada kart olmadığı için oyuncu masaya kart atmak zorundadır. Kart atıldıktan sonra fonksiyon sonlandırılır.
            }
            else
            {
                KartYutma(oyuncu, saglayanDurumlar, secilenKartIndex - 1);
            }
        }

        /// <summary>
        /// Oyuncunun seçtiği kart ile masada sağlanan durumları kontrol eder ve gerektiğinde oyuncunun kazandığı kartları yönetir.
        /// </summary>
        /// <param name="oyuncu">Kart seçecek oyuncu.</param>
        /// <param name="saglayanDurumlar">Oyuncunun seçtiği kart ile sağlanan durumlar.</param>
        /// <param name="secilenKartIndex">Oyuncunun seçtiği kartın indeksi.</param>
        public void KartYutma(Player oyuncu, List<List<int>> saglayanDurumlar, int secilenKartIndex)
        {
            this.enSonKartYutanOyuncu = oyuncu; // Bu fonksiyonun çağrıldığı oyuncu, en son kart yutan oyuncu olur. Bu sayede oyunun sonunda en son kart yutan oyuncu belirlenir ve en sonda masadaki kartlar ona verilir.

            // Eğer sağlanan durumlar sadece bir tane ise
            if (saglayanDurumlar.Count == 1)
            {
                // Masadaki kartları sırasıyla kontrol eder ve kazanılan kartları oyuncunun kazandığı kartlara ekler
                for (int i = this.MasadakiKartlar.Count - 1; i >= 0; i--)
                {
                    if (saglayanDurumlar[0].Contains(i))
                    {
                        oyuncu.kazanilanKartlar.Add(this.MasadakiKartlar[i]);
                        this.MasadakiKartlar.RemoveAt(i);
                    }
                }

                // Scopa puanını kontrol eder ve gerektiğinde oyuncuya +1 puan ekler
                oyuncu.ScopaPuaniKontrolu(MasadakiKartlar);

                // Seçilen kartı oyuncunun kazandığı kartlara ekler ve elinden çıkarır
                oyuncu.kazanilanKartlar.Add(oyuncu.eldekiKartlar[secilenKartIndex]);
                oyuncu.eldekiKartlar.RemoveAt(secilenKartIndex);
            }
            // Eğer sağlanan durumlar birden fazla ise
            else if (saglayanDurumlar.Count > 1)
            {
                // Tüm kombinasyonları ekrana yazdırır
                for (int i = 0; i < saglayanDurumlar.Count; i++)
                {
                    Console.Write($"{i + 1}. kombinasyon: ");
                    for (int j = 0; j < saglayanDurumlar[i].Count; j++)
                    {
                        Console.Write($"{this.kartTurleri[this.MasadakiKartlar[saglayanDurumlar[i][j]] % 4]} {this.MasadakiKartlar[saglayanDurumlar[i][j]] / 4 + 1}   ");
                    }
                    Console.WriteLine();
                }

                int secilenKombinasyon;

                // Oyuncunun geçerli bir kombinasyon seçmesini sağlar
                while (true)
                {
                    try
                    {
                        Console.Write("\nKombinasyon numarası giriniz: ");
                        secilenKombinasyon = Convert.ToInt32(Console.ReadLine());

                        if (secilenKombinasyon > 0 && secilenKombinasyon <= saglayanDurumlar.Count)
                            break;
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Geçersiz kombinasyon numarası girdiniz. Lütfen tekrar deneyin.");
                            Console.ResetColor();
                        }
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Geçersiz kombinasyon numarası girdiniz. Lütfen tekrar deneyin.");
                    }
                }

                // Masadaki kartları sırasıyla kontrol eder ve kazanılan kartları oyuncunun kazandığı kartlara ekler
                for (int i = this.MasadakiKartlar.Count - 1; i >= 0; i--)
                {
                    if (saglayanDurumlar[secilenKombinasyon - 1].Contains(i))
                    {
                        oyuncu.kazanilanKartlar.Add(this.MasadakiKartlar[i]);
                        this.MasadakiKartlar.RemoveAt(i);
                    }
                }

                // Scopa puanını kontrol eder ve gerektiğinde oyuncuya +1 puan ekler
                oyuncu.ScopaPuaniKontrolu(MasadakiKartlar);

                // Seçilen kartı oyuncunun kazandığı kartlara ekler ve elinden çıkarır
                oyuncu.kazanilanKartlar.Add(oyuncu.eldekiKartlar[secilenKartIndex]);
                oyuncu.eldekiKartlar.RemoveAt(secilenKartIndex);
            }
        }

        // Tur dönderme işlemini yapar.
        public void TurDonder(Player[] oyuncular)
        {
            for (int i = 1; i <= 3; i++)// Her oyuncuda 3 kart olduğu için 3 kere dönünce 1 tur biter.
                for(int j = 0; j < oyuncuSayisi; j++)// her oyuncu sırasıyla kart atar.
                    OyuncuKartSecVeAt(oyuncular[j]);
        }

        /// <summary>
        /// Verilen listedeki sayılar arasından belirli bir hedef sayıya ulaşan kombinasyonları bulur.
        /// </summary>
        /// <param name="masadakiler">Liste, üzerinde kombinasyonlar aranacak olan sayıları içerir.</param>
        /// <param name="hedefSayi">Hedef sayı, kombinasyonların toplamının bu sayıya ulaşması gerektiğini belirtir.</param>
        /// <returns>Bulunan kombinasyonları içeren bir liste döndürür.</returns>
        public List<List<int>> SaglayanDurumlariBul(List<int> masadakiler, int hedefSayi)
        {
            // Kombinasyonları tutacak bir liste oluşturulur
            List<List<int>> saglayanDurumlar = new List<List<int>>();

            // Kombinasyonları bulan yardımcı fonksiyon çağrılır
            KombinasyonlariBul(masadakiler, hedefSayi, new List<int>(), saglayanDurumlar, 0, 0);

            // Bulunan kombinasyonlar döndürülür
            return saglayanDurumlar;
        }

        /// <summary>
        /// Verilen listedeki sayılar arasından belirli bir hedef sayıya ulaşan kombinasyonları bulan yardımcı bir fonksiyondur.
        /// </summary>
        /// <param name="liste">Liste, üzerinde kombinasyonlar aranacak olan sayıları içerir.</param>
        /// <param name="hedefSayi">Hedef sayı, kombinasyonların toplamının bu sayıya ulaşması gerektiğini belirtir.</param>
        /// <param name="mevcutKombinasyon">Şu anki kombinasyonun elemanlarını içeren bir liste.</param>
        /// <param name="sonuclar">Bulunan kombinasyonların toplandığı ana liste.</param>
        /// <param name="toplam">Şu ana kadar toplanmış olan sayıların toplamı.</param>
        /// <param name="indeks">Listenin hangi indeksinden başlanacağını belirtir.</param>
        public void KombinasyonlariBul(List<int> liste, int hedefSayi, List<int> mevcutKombinasyon, List<List<int>> sonuclar, int toplam, int indeks)
        {
            // Eğer şu ana kadar toplam, hedef sayıya ulaşırsa
            if (toplam == hedefSayi)
            {
                // Şu anki kombinasyon sonuçlar listesine eklenir
                sonuclar.Add(new List<int>(mevcutKombinasyon));
                return; // Fonksiyon sonlandırılır
            }

            // Listenin verilen indeksinden başlayarak her bir eleman için tekrar eden işlemler gerçekleştirilir
            for (int i = indeks; i < liste.Count; i++)
            {
                // Şu anki eleman kombinasyon listesine eklenir
                mevcutKombinasyon.Add(i);

                // Şu anki elemanın değeri toplama eklenir ve indeks bir arttırılarak bir sonraki elemana geçilir
                KombinasyonlariBul(liste, hedefSayi, mevcutKombinasyon, sonuclar, toplam + liste[i] / 4 + 1, i + 1);

                // En son eklenen eleman kombinasyon listesinden çıkarılır ve işlem diğer elemanlarla devam eder
                mevcutKombinasyon.RemoveAt(mevcutKombinasyon.Count - 1);
            }
        }

        // Oyunun sonunda masada kalan kartları son kart yutan oyuncuya verir.
        public void MasadaKalanKartlariOyuncuyaVer()
        {
            this.enSonKartYutanOyuncu.kazanilanKartlar.AddRange(this.MasadakiKartlar);// add fonksiyonu ile masadaki kartları oyuncunun kazandığı kartlara ekler.
        }

        // Oyun sonunda oyuncuların puanlarını hesaplar ve belirli koşullara göre puanları günceller.
        public void OyunSonuPuanHesapla(Player[] oyuncular)
        {
            // Her oyuncunun kartlarını kontrol eder ve primeria skorlarını hesaplar
            for (int i = 0; i < oyuncuSayisi; i++)
            {
                oyuncular[i].KaroYediliVePrimeriaKontrolu();
                oyuncular[i].PrimeriaPuaniHesapla();
            }

            // Oyuncuları primeria skorlarına göre sıralar
            for (int i = 0; i < oyuncular.Length; i++)
                for (int j = i + 1; j < oyuncular.Length; j++)
                    if (oyuncular[j].primeriaSkoru > oyuncular[i].primeriaSkoru)
                    {
                        Player temp = oyuncular[i];
                        oyuncular[i] = oyuncular[j];
                        oyuncular[j] = temp;
                    }

            // En yüksek primeria skoruna sahip oyuncuya puan verir
            if (oyuncular[0].primeriaSkoru != oyuncular[1].primeriaSkoru)
                oyuncular[0].puan++;

            // Oyuncuları karo kart sayılarına göre sıralar
            for (int i = 0; i < oyuncular.Length; i++)
                for (int j = i + 1; j < oyuncular.Length; j++)
                    if (oyuncular[j].karoKartSayisi > oyuncular[i].karoKartSayisi)
                    {
                        Player temp = oyuncular[i];
                        oyuncular[i] = oyuncular[j];
                        oyuncular[j] = temp;
                    }

            // En yüksek karo kart sayısına sahip oyuncuya puan verir
            if (oyuncular[0].karoKartSayisi != oyuncular[1].karoKartSayisi)
                oyuncular[0].puan++;

            // Oyuncuları kazanılan kart sayılarına göre sıralar
            for (int i = 0; i < oyuncular.Length; i++)
                for (int j = i + 1; j < oyuncular.Length; j++)
                    if (oyuncular[j].kazanilanKartlar.Count > oyuncular[i].kazanilanKartlar.Count)
                    {
                        Player temp = oyuncular[i];
                        oyuncular[i] = oyuncular[j];
                        oyuncular[j] = temp;
                    }

            // En fazla kazanılan kart sayısına sahip oyuncuya puan verir
            if (oyuncular[0].kazanilanKartlar.Count != oyuncular[1].kazanilanKartlar.Count)
                oyuncular[0].puan++;
        }

        // Oyuncuları puanlarına göre sıralar.Eğer iki oyuncu aynı puana sahipse, önceki sıradaki oyuncu, sonraki sıradaki oyuncudan önce gelir.
        public void PuanaGoreSirala(Player[] oyuncular)
        {
            // Oyuncuları puanlarına göre sıralar
            for (int i = 0; i < oyuncular.Length; i++)
            {
                for (int j = i + 1; j < oyuncular.Length; j++)
                {
                    if (oyuncular[j].puan > oyuncular[i].puan)
                    {
                        Player temp = oyuncular[i];
                        oyuncular[i] = oyuncular[j];
                        oyuncular[j] = temp;
                    }
                }
            }
        }

        // Oyuncuları puanlarına göre sıralar ve ekrana yazar.Aynı puanı alan oyuncular arasında sıralama yapar ve renk değişimi yapar.
        public void SiralamayiYazdir(Player[] oyuncular)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkGreen;

            int kacinciSirada = 0; // Kaçıncı sırada olduğunu belirtir
            int puan = -1; // -1 denen bir değer yoktur. Bu sayede alttaki for döngüsünde 0. indexteki oyuncu için if bloğuna her halükarda girilir.

            // Oyuncuları puanlarına göre sıralar ve ekrana yazar
            for (int i = 0; i < oyuncular.Length; i++)
            {
                // Her oyuncunun puanı kontrol edilir ve aynı puanı alan oyuncuların aynı sıradaki bir sonraki oyuncu ile renk değişimi yapılır
                if (oyuncular[i].puan != puan)
                {
                    kacinciSirada++;
                    puan = oyuncular[i].puan;
                }

                // İkinci sıradaki oyuncunun yazı rengi değiştirilir
                if (kacinciSirada == 2)
                    Console.ResetColor();

                // Oyuncunun sıralaması ve puanı ekrana yazdırılır
                Console.WriteLine($"{kacinciSirada}. {oyuncular[i].adi} {oyuncular[i].puan} puan aldı.");
            }
        }

    }
}
