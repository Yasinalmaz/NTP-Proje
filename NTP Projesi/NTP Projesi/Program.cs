using System;

// NESNEYE YÖNELİK PROGRAMLAMA PROJESİ
//YASİN ALMAZ 231041015 - ŞÜKRÜ FINDIK 2310410153 

namespace OtelSistemi
{
    // Soyut Oda sınıfı
    public abstract class Oda
    {
        public string OdaTipi { get; set; }
        public bool DoluMu { get; set; }
        public string OdaNumarasi { get; set; }

        public Oda(string odaTipi, string odaNumarasi)
        {
            OdaTipi = odaTipi;
            OdaNumarasi = odaNumarasi;
            DoluMu = false;
        }

        public virtual void RezervasyonYap()
        {
            if (DoluMu)
            {
                Console.WriteLine($"{OdaTipi} oda dolu.");
            }
            else
            {
                DoluMu = true;
                Console.WriteLine($"{OdaTipi} oda rezervasyonu başarılı.");
            }
        }

        public void OdaBilgisiGoster()
        {
            Console.WriteLine($"Oda Tipi: {OdaTipi}, Oda Numarası: {OdaNumarasi}, Durum: {(DoluMu ? "Dolu" : "Boş")}");
        }
    }

    // Standart Oda sınıfı
    public class StandartOda : Oda
    {
        public StandartOda(string odaNumarasi) : base("Standart", odaNumarasi) { }

        public override void RezervasyonYap()
        {
            base.RezervasyonYap();
        }
    }

    // Suit Oda sınıfı
    public class SuitOda : Oda
    {
        public SuitOda(string odaNumarasi) : base("Suit", odaNumarasi) { }

        public override void RezervasyonYap()
        {
            base.RezervasyonYap();
        }
    }

    // Misafir sınıfı
    public class Misafir
    {
        public string Isim { get; set; }
        public string Soyisim { get; set; }
        public string TCNo { get; set; }
        public string PasaportNo { get; set; }
        public string OdemeSekli { get; set; }  // "Kredi Kartı" veya "Nakit"

        public Misafir(string isim, string soyisim)
        {
            Isim = isim;
            Soyisim = soyisim;
        }

        // Türk vatandaşı için TC numarasını doğrulama
        public bool TCNoDogrula(string tcNo)
        {
            if (tcNo.Length == 11 && long.TryParse(tcNo, out _))
            {
                TCNo = tcNo;
                return true;
            }
            return false;
        }

        // Yabancı için pasaport numarasını doğrulama
        public bool PasaportNoDogrula(string pasaportNo)
        {
            PasaportNo = pasaportNo;
            return true;
        }

        // Ödeme türünü al
        public void OdemeSekliSor()
        {
            Console.WriteLine("Ödeme türünü seçin: (Kredi Kartı/Nakit)");
            OdemeSekli = Console.ReadLine();

            if (OdemeSekli.ToLower() == "kredi kartı")
            {
                Console.WriteLine("Kredi Kartı Numarasını giriniz (16 haneli sayılar):");
                string kartNumarasi = Console.ReadLine();
                if (kartNumarasi.Length == 16 && long.TryParse(kartNumarasi, out _))
                {
                    Console.WriteLine("Ödeme kredi kartıyla alınacaktır.");
                }
                else
                {
                    Console.WriteLine("Geçersiz kart numarası.");
                }
            }
            else if (OdemeSekli.ToLower() == "nakit")
            {
                Console.WriteLine("Nakit ödeme alınacaktır.");
            }
            else
            {
                Console.WriteLine("Geçersiz ödeme türü.");
            }
        }
    }

    // Otel sınıfı
    public class Otel
    {
        private Oda[] odalar;

        public Otel()
        {
            odalar = new Oda[5];
            odalar[0] = new StandartOda("101");
            odalar[1] = new StandartOda("102");
            odalar[2] = new StandartOda("103");
            odalar[3] = new SuitOda("201");
            odalar[4] = new SuitOda("202");
        }

        // Oda rezervasyonu
        public void RezervasyonYap(Misafir misafir, int odaNumarasi)
        {
            if (odaNumarasi >= 101 && odaNumarasi <= 103) // Standart odalar
            {
                Oda oda = odalar[odaNumarasi - 101];
                oda.RezervasyonYap();
            }
            else if (odaNumarasi >= 201 && odaNumarasi <= 202) // Suit odalar
            {
                Oda oda = odalar[odaNumarasi - 101];
                oda.RezervasyonYap();
            }
            else
            {
                Console.WriteLine("Geçersiz oda numarası.");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Otel otel = new Otel();

            Console.WriteLine("Misafir adı:");
            string isim = Console.ReadLine();
            Console.WriteLine("Misafir soyadı:");
            string soyisim = Console.ReadLine();

            Misafir misafir = new Misafir(isim, soyisim);

            Console.WriteLine("Türk vatandaşı mı? (Evet/Hayır)");
            string turkVatandasi = Console.ReadLine();

            if (turkVatandasi.ToLower() == "evet")
            {
                Console.WriteLine("TC Kimlik numarasını giriniz:");
                string tcNo = Console.ReadLine();
                if (misafir.TCNoDogrula(tcNo))
                {
                    Console.WriteLine("TC Kimlik numarası doğrulandı.");
                }
                else
                {
                    Console.WriteLine("Geçersiz TC Kimlik numarası.");
                }
            }
            else
            {
                Console.WriteLine("Pasaport numarasını giriniz:");
                string pasaportNo = Console.ReadLine();
                if (misafir.PasaportNoDogrula(pasaportNo))
                {
                    Console.WriteLine("Pasaport numarası doğrulandı.");
                }
            }

            misafir.OdemeSekliSor();

            // Rezervasyon yapılacak oda seçimi
            Console.WriteLine("Rezervasyon yapılacak oda numarasını giriniz (101-103 Standart, 201-202 Suit):");
            int odaNumarasi = Convert.ToInt32(Console.ReadLine());

            otel.RezervasyonYap(misafir, odaNumarasi);
        }
    }
}

