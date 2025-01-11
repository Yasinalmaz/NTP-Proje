using System;
using System.Collections.Generic;  //İki nesnenin karşılaştırılması için kullanılan bir yöntem
using System.Net;
using System.Net.Mail;


namespace RestaurantOrder
{
    // Menü öğelerinin temel sınıfı
    public abstract class MenuItem //Soyut MenuItem sınıfı açılır daha sonra override ile Miras alımında kullanılarak ezilir.
    {
        public string Ad { get; set; }   //Sipariş edilecek ürünler için main ad ve fiyat değişkenleri tanımlanır.
        public decimal Fiyat { get; set; }         //Kapsülleme kullanılır.

        public MenuItem(string ad, decimal fiyat)
        {
            Ad = ad;
            Fiyat = fiyat;
        }

        public abstract void Goster();  //MenuItem kullanılmak üzere Goster() metodunda toplanır.
    }

    // Menü kategorileri için türemiş sınıflar
    public class Baslangic : MenuItem   //Baslangıç classı MenıItemden miras alarak türetilir.
    {
        public Baslangic(string ad, decimal fiyat) : base(ad, fiyat) { }  //base (alt sınıftaki üyeler tarafından geçersiz kılınan (veya gizlenen) temel sınıftaki üyelere erişmek için kullanılır.)
        public override void Goster() => Console.WriteLine($"Başlangıç: {Ad} - Fiyat: {Fiyat} TL");
    }

    public class Corba : MenuItem
    {
        public Corba(string ad, decimal fiyat) : base(ad, fiyat) { }
        public override void Goster() => Console.WriteLine($"Çorba: {Ad} - Fiyat: {Fiyat} TL");
    }

    public class AnaYemek : MenuItem
    {
        public AnaYemek(string ad, decimal fiyat) : base(ad, fiyat) { }
        public override void Goster() => Console.WriteLine($"Ana Yemek: {Ad} - Fiyat: {Fiyat} TL");
    }

    public class AraYemek : MenuItem
    {
        public AraYemek(string ad, decimal fiyat) : base(ad, fiyat) { }
        public override void Goster() => Console.WriteLine($"Ara Yemek: {Ad} - Fiyat: {Fiyat} TL");
    }

    public class Icecek : MenuItem
    {
        public Icecek(string ad, decimal fiyat) : base(ad, fiyat) { }
        public override void Goster() => Console.WriteLine($"İçecek: {Ad} - Fiyat: {Fiyat} TL");
    }

    public class Tatli : MenuItem
    {
        public Tatli(string ad, decimal fiyat) : base(ad, fiyat) { }
        public override void Goster() => Console.WriteLine($"Tatlı: {Ad} - Fiyat: {Fiyat} TL");
    }

    // Sipariş sınıfı
    public class Siparis
    {  //Siparişe ürünler eklemek ve toplam fiyatı hesaplamak üzere metotlar oluşturulur
        public List<MenuItem> Urunler { get; private set; } = new List<MenuItem>();

        // Siparişe ürün ekleme
        public void UrunEkle(MenuItem urun)
        {
            Urunler.Add(urun);
        }

        // Toplam fiyat hesaplama
        public decimal ToplamFiyat()
        {
            decimal toplam = 0;
            foreach (var urun in Urunler)  //Urun ekleme devam ettiği sürece toplam da buna bağlı artar.
            {
                toplam += urun.Fiyat;
            }
            return toplam;
        }

        // Sipariş özeti gösterme
        public void SiparisGoster()  //Sipariş edilenler gösterilir ve devamında toplam fiyat belirtilir.
        {
            Console.WriteLine("\nSiparişiniz:\n");
            foreach (var urun in Urunler)
            {
                urun.Goster();
            }
            Console.WriteLine($"\nToplam Fiyat: {ToplamFiyat()} TL");
        }
        public string SiparisOzeti()
        {
            string ozeti = "\nSiparişiniz:\n";
            foreach (var urun in Urunler)
            {
                ozeti += $"{urun.Ad} - Fiyat: {urun.Fiyat} TL\n";
            }
            ozeti += $"\nToplam Fiyat: {ToplamFiyat()} TL";
            return ozeti;
        }
    }

    // Ödeme sınıfı
    public class Odeme
    {  //Misafirin isteği üzerine ödeme yöntemi seçilir.
        private string odemeYontemi;

        // Ödeme yöntemi ile ödeme sınıfı
        public Odeme(string odemeYontemi)  //this ilgili nesnenin referansını belirtmek için kullanılır
        {
            this.odemeYontemi = odemeYontemi;
        }

        // Ödemeyi işleme kart veya nakit olmalı aksi takdirde geçersiz kılar.
        public void OdemeIslemi(decimal toplamTutar)
        {
            if (odemeYontemi.ToLower() == "kart")
            {
                KartlaOdeme();
            }
            else if (odemeYontemi.ToLower() == "nakit")
            {
                NakitleOdeme();
            }
            else
            {
                Console.WriteLine("Geçersiz ödeme yöntemi.");
            }
        }

        // Kartla ödeme işleme (doğrulama)
        private void KartlaOdeme()
        {
            string kartNumarasi;
            Console.Write("Kart numarasını girin (16 haneli): ");
            while (true)
            {
                kartNumarasi = Console.ReadLine();  //16 hane ve sadece sayılardan oluşacak şekilde aksi takdirde geçersiz kılar ve doğru olana kadar girdi ister
                if (kartNumarasi.Length == 16 && long.TryParse(kartNumarasi, out _))
                {
                    Console.WriteLine("Ödemeniz başarıyla tamamlanmıştır.");
                    break;
                }
                else
                {
                    Console.WriteLine("Geçersiz kart numarası. Lütfen tekrar deneyin.");
                }
            }
        }

        // Nakit ödeme işleme
        private void NakitleOdeme() //Nakit ödeme istenirse bu mesajı bildirir.
        {
            Console.WriteLine("Ödemenizi kuryeye yapabilirsiniz.");
        }
    }

    class Program
    {
        static void Main(string[] args) //Kategori oluşturmak üzere kategori atamaları yapılır
        {
            // Farklı kategoriler için örnek menü öğeleri
            var baslangiclar = new List<Baslangic>
            {
                new Baslangic("Cacık", 50),
                new Baslangic("Sigara Böreği",45 ),
                new Baslangic("Kabak Tarator",65 ),
                new Baslangic("Humus",75 ),
                new Baslangic("Haydari",40 ),
                new Baslangic("Muhammara",80 ),
            };

            var corbalar = new List<Corba>
            {
                new Corba("Yayla", 70),
                new Corba("Kırmızı Mercimek", 65),
                new Corba("Kelle Paça",100 ),
                new Corba("Pazı",75 ),
                new Corba("Tarhana",65 ),
                new Corba("İşkembe",95 ),
                new Corba("Kremalı Mantar",85 )

            };

            var anayemekler = new List<AnaYemek>
            {
                new AnaYemek("Izgara Tavuk Göğsü", 240),
                new AnaYemek("Kuzu Tandır", 290),
                new AnaYemek("Tekirdağ Köftesi",270 ),
                new AnaYemek("İskender", 275),
                new AnaYemek("Mantarlı Tavuk Sote", 195),
                new AnaYemek("Pirinç Pilav",120 ),
                new AnaYemek("Karnabahar Shots",160 ),
                new AnaYemek("Sultan Kebabı",300 ),
                new AnaYemek("Taco",125 )

            };

            var arayemekler = new List<AraYemek>
            {
                new AraYemek("Patates Kızartması", 75),
                new AraYemek("Zeytinyağlı Enginar", 80),
                new AraYemek("Patates kroket",95 ),
                new AraYemek("Mantar graten",100 ),
                new AraYemek("Kabak kızartması",90 ),
                new AraYemek("Kalamar tava",150 ),
                new AraYemek("Patlıcan sarma", 85)

            };

            var icecekler = new List<Icecek>
            {
                new Icecek("Ayran", 30),
                new Icecek("Kola", 45),
                new Icecek("Şalgam", 40),
                new Icecek("Meyve suyu", 35),
                new Icecek("Gazoz", 25),
                new Icecek("Soğuk çay", 40),
                new Icecek("Su", 15),
                new Icecek("Çay", 25),
                new Icecek("Kahve", 60),
                new Icecek("Limonata", 40)
            };

            var tatlilar = new List<Tatli>
            {
                new Tatli("Baklava", 75),
                new Tatli("Sütlaç", 60),
                new Tatli("Keşkül", 70),
                new Tatli("Kadayıf", 85),
                new Tatli("Elmalı turta", 65),
                new Tatli("Dondurma", 70),
                new Tatli("Magnolya", 80),
                new Tatli("Profiterol", 90),
                new Tatli("Tiramisu", 85)
            };

            // Yeni bir sipariş oluşturuluyor
            Siparis musteriSiparisi = new Siparis(); //Sipariş metodunu kullanarak muşteriSiparişi oluşturur 

            // Ana menü döngüsü
            while (true)
            {
                // Kategoriler menüsünü göster
                Console.WriteLine("Menü Kategorileri:");
                Console.WriteLine("1. Başlangıçlar");
                Console.WriteLine("2. Çorbalar");
                Console.WriteLine("3. Ana Yemekler");
                Console.WriteLine("4. Ara Yemekler");
                Console.WriteLine("5. İçecekler");
                Console.WriteLine("6. Tatlılar");
                Console.WriteLine("7. Siparişi Bitir");
                Console.WriteLine("8. Ana Menüye Dön");
                Console.Write("Bir kategori seçin (1-8): ");
                string kategoriSecimi = Console.ReadLine();

                if (kategoriSecimi == "7")
                {
                    break; // Döngüden çık ve ödemeye geç
                }

                // "Ana Menüye Dön" veya geçersiz seçim
                if (kategoriSecimi == "8")
                {
                    continue; // Ana menüye dön
                }

                // Seçilen kategoriye ait menüyü göster ve sipariş al
                switch (kategoriSecimi)
                {
                    case "1":
                        MenuGoster(baslangiclar);
                        SiparisAl(baslangiclar, musteriSiparisi);
                        break;
                    case "2":
                        MenuGoster(corbalar);
                        SiparisAl(corbalar, musteriSiparisi);
                        break;
                    case "3":
                        MenuGoster(anayemekler);
                        SiparisAl(anayemekler, musteriSiparisi);
                        break;
                    case "4":
                        MenuGoster(arayemekler);
                        SiparisAl(arayemekler, musteriSiparisi);
                        break;
                    case "5":
                        MenuGoster(icecekler);
                        SiparisAl(icecekler, musteriSiparisi);
                        break;
                    case "6":
                        MenuGoster(tatlilar);
                        SiparisAl(tatlilar, musteriSiparisi);
                        break;
                    default:
                        Console.WriteLine("Geçersiz seçim! Lütfen tekrar deneyin.");
                        break;
                }

                // Kullanıcıya devem etme, ana menüye dönme veya siparişi bitirme seçeneği sunulur.
                Console.WriteLine("\nSipariş eklemeye devam etmek için 'devam' yazın, ana menüye dönmek için 'menü' yazın, siparişi bitirmek için 'bitir' yazın.");
                string islemSecimi = Console.ReadLine();

                if (islemSecimi.ToLower() == "bitir")
                {
                    break; // Döngüden çık ve ödemeye geç
                }
                else if (islemSecimi.ToLower() == "menü")
                {
                    continue; // Ana menüye dön
                }
                else if (islemSecimi.ToLower() != "devam")
                {
                    Console.WriteLine("Geçersiz seçim! Menüye dönülüyor.");
                }
            }

            // Sipariş özetini göster
            musteriSiparisi.SiparisGoster();

            // Ödeme yöntemini sor kart nakit durumuna göre aksiyon alınır.
            Console.Write("\nÖdeme Yöntemi Seçin (kart/nakit): ");
            string odemeYontemi = Console.ReadLine();

            // Ödeme işlemini başlat
            Odeme odeme = new Odeme(odemeYontemi);
            odeme.OdemeIslemi(musteriSiparisi.ToplamFiyat());

            // Kullanıcıdan e-posta adresini al
            Console.Write("\nE-posta adresinizi girin: ");
            string email = Console.ReadLine();

            // Sipariş özeti e-posta ile gönder
            SendEmail(email, musteriSiparisi.SiparisOzeti());
        }

        // Seçilen kategorinin menüsünü göster
        static void MenuGoster<T>(List<T> menuOgeleri) where T : MenuItem
        {             // where T(Bu kısım, tür kısıtlamasıdır. Burada T türünün, MenuItem sınıfından türemiş bir sınıf olması gerektiği belirtilmiştir.)
            Console.WriteLine("\nMenü:");
            foreach (var urun in menuOgeleri)
            {
                urun.Goster();
            }
        }

        // Sipariş almak
        static void SiparisAl<T>(List<T> menuOgeleri, Siparis siparis) where T : MenuItem
        {
            Console.WriteLine("\nSipariş vermek için yemek ismini yazın (çıkmak için 'bitir' yazın): ");
            string girdi;
            while ((girdi = Console.ReadLine()) != "bitir")
            {
                var urun = menuOgeleri.Find(i => i.Ad.Equals(girdi, StringComparison.OrdinalIgnoreCase));
                if (urun != null)
                {
                    siparis.UrunEkle(urun);
                    Console.WriteLine($"{girdi} siparişiniz eklendi.");
                }
                else
                {
                    Console.WriteLine("Geçersiz yemek adı. Lütfen geçerli bir yemek ismi girin.");
                }
            }
        }
        static void SendEmail(string toEmail, string body)
        {
            try
            {
                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("youremail@gmail.com", "yourpassword"),
                    EnableSsl = true,
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress("youremail@gmail.com"),
                    Subject = "Sipariş Özeti",
                    Body = body,
                    IsBodyHtml = false,
                };
                mailMessage.To.Add(toEmail);

                smtpClient.Send(mailMessage);
                Console.WriteLine("\nSipariş özetiniz e-posta adresinize gönderildi!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("E-posta gönderilirken hata oluştu: " + ex.Message);
            }
        }
    }
}
