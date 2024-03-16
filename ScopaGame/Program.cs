using System;
using System.Collections.Generic;

namespace ScopaGame
{
    class Program
    {
        public static void Main()
        {
            Console.WriteLine("Scopa Oyununa Hoşgeldiniz!");

            Game oyun = new Game();
            Player[] oyuncular = oyun.OyuncuOlustur(oyun);// Oyuncular oluşturulur.

            int turSayisi = (40 - 4) / oyun.oyuncuSayisi * 3;

            int kacinciTurdayiz = 1;// tur sayısı
            while (oyun.OyunDestesi.Count != 0)
            {
                Console.WriteLine($"\n{kacinciTurdayiz}. tur başlıyor...");
                oyun.KartDagit(oyuncular);  // oyunculara kart dağıtılır.

                if (kacinciTurdayiz == 1)
                    oyun.MasayaKartAt();// 1. turda ekstra masaya da 4 kart dağıtılır.

                oyun.TurDonder(oyuncular);  // oyuncuların kart seçimleri yapılır.
                kacinciTurdayiz++;
            }

            oyun.MasadaKalanKartlariOyuncuyaVer();// Oyunun sonunda masada kalan kartlar son kart yutan oyuncuya verilir.
            oyun.OyunSonuPuanHesapla(oyuncular);// Oyunun sonunda her oyuncunun puanı hesaplanır.
            oyun.PuanaGoreSirala(oyuncular);// Oyuncuları puanlarına göre sıralar.
            oyun.SiralamayiYazdir(oyuncular);// Oyunun kazananını yazdırır.

        }

    }
}
