using System;
using System.Collections.Generic;
using System.Threading;

namespace ScopaGame
{
    class Player
    {
        static int[] primeriaPuanDegerleri = { 16, 12, 13, 14, 15, 18, 21, 10, 10, 10 };// 1'den 10'a kadar olan kartların sırasıyla primeria değerleri.
        int[] primeria = new int[4];// 0. eleman en büyük karo , 1. eleman en büyük kupa , 2. eleman en büyük maça , 3. eleman en büyük sinek.
        public int primeriaSkoru = 0;

        public int karoKartSayisi = 0;// Oyuncunun elindeki karo kartlarının sayısını tutar.

        public List<int> kazanilanKartlar;// Oyuncunun kazandığı kartlar.
        public List<int> eldekiKartlar;// Oyunun herhangi bir anında oyuncunun elinde bulunan kartları tutar.

        public readonly string adi;// Oyuncunun adı constructor tarafından atanır ve readonly sayesinde bir daha değiştirilemez.
        public int puan = 0;// Oyuncunun oyun sonundaki puanını tutar.Kazanan ve kaybedenler bu puana göre belirlenir.

        public Player(string adi)
        {
            this.adi = adi;
            this.kazanilanKartlar = new List<int>();
            this.eldekiKartlar = new List<int>();
        }

        // Oyunda herhangi bir oyuncu kart yuttuğunda masada kart kalmamışsa scopa yapmış olur ve +1 puan alır.Bu fonksiyon da bu durumu kontrol eder ve gerekli işlemleri yapar.
        public void ScopaPuaniKontrolu(List<int> masadakiKartlar)
        {
            if (masadakiKartlar.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nBravo {this.adi} SCOPA yapıp +1 puan aldın...");
                Console.ResetColor();
                this.puan++;
                Thread.Sleep(2000);
            }
        }

        // Karo yedili varsa oyuncuya 1 puan ekler ve primeria puanı hesaplanıken kullanılacak olan primeria dizisini günceller.
        public void KaroYediliVePrimeriaKontrolu()
        {
            for (int i = 0; i < kazanilanKartlar.Count; i++)
            {
                int kartTuru = this.kazanilanKartlar[i] % 4;
                int kartDegeri = this.kazanilanKartlar[i] / 4 + 1;

                if (this.primeria[kartTuru] < kartDegeri)
                    this.primeria[kartTuru] = kartDegeri;

                if (kartTuru == 0)
                    karoKartSayisi++;

                if (kazanilanKartlar[i] == 24)// 24 kartı karo yedili kartına denk gelir(24/4+1 = 7 , 24%4 = 0 => karo).
                    puan++;
            }
        }

        // Yukarıda tanımlanan primeria dizisini kullanarak oyuncunun primeria puanını hesaplar.
        public void PrimeriaPuaniHesapla()
        {
            for (int i = 0; i < 4; i++)
            {
                if (primeria[i] == 0)
                    continue;

                this.primeriaSkoru += primeriaPuanDegerleri[primeria[i] - 1];
            }
        }

    }
}
