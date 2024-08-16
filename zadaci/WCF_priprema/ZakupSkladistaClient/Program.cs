using ZakupSkladistaClient.ZakupSkladistaServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace ZakupSkladistaClient
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            ZakupSkladistaServiceClient client = new ZakupSkladistaServiceClient();

            while (true)
            {
                Console.WriteLine("Izaberite opciju:\n" +
                "\t[1] Zakupi skladiste.\n" +
                "\t[2] Vrati listu svih aktivnih skladista zadatog vlasnika.\n" +
                "\t[3] Vrati listu svih vlasnika aktivnih skladista.\n" +
                "\t[4] Vrati sva skladista sa istorijom zakupa.\n" +
                "\t[5] Izadji.\n");

                char opcija = Console.ReadKey().KeyChar;
                Console.WriteLine();

                switch (opcija)
                {
                    default:
                        Console.WriteLine("Nepoznata opcija, pokusajte ponovo!");
                        break;
                    case '1':
                        Vlasnik vlasnik = new Vlasnik();
                        Console.Write("Unesite ime vlasnika: ");
                        vlasnik.Ime = Console.ReadLine();
                        Console.Write("Unesite prezime vlasnika: ");
                        vlasnik.Prezime = Console.ReadLine();
                        Console.Write("Unesite JMBG vlasnika: ");
                        vlasnik.Jmbg = Console.ReadLine();

                        Skladiste skladiste = new Skladiste();
                        Console.Write("Unesite ID skladista: ");
                        skladiste.Id = Console.ReadLine();
                        Console.Write("Unesite cenu skladista: ");
                        skladiste.Cena = double.Parse(Console.ReadLine());

                        Console.Write("Unesite datum pocetka zakupa [MM/dd/yy hh:mm]: ");
                        DateTime pocetakZakupa = DateTime.Parse(Console.ReadLine());

                        Console.Write("Unesite datum kraja zakupa [MM/dd/yy hh:mm]: ");
                        DateTime krajZakupa = DateTime.Parse(Console.ReadLine());

                        Zakup zakup = await client.ZakupiSkladisteAsync(vlasnik, skladiste, pocetakZakupa, krajZakupa);

                        if (zakup != null)
                            Console.WriteLine("Uspesan zakup!");

                        break;
                    case '2':
                        Console.Write("Unesite JMBG vlasnika: ");
                        string jmbgVlasnika = Console.ReadLine();

                        Skladiste[] aktivnaSkladista = await client.VratiAktivnaSkladistaAsync(jmbgVlasnika);

                        foreach (var s in aktivnaSkladista)
                            Console.WriteLine($"ID skladista: `{s.Id}`, Cena skladista: {s.Cena}");
                        break;
                    case '3':
                        Console.WriteLine("Vlasnici aktivnih skladista:");
                        Vlasnik[] vlasniciAktivnihSkladista = await client.VratiVlasnikeAktivnihSkladistaAsync();

                        foreach (var v in vlasniciAktivnihSkladista)
                            Console.WriteLine($"\tIme: `{v.Ime}`, Prezime: `{v.Prezime}`, `{v.Jmbg}`");
                        break;
                    case '4':
                        Console.WriteLine("Skladista sa istorijom zakupa:");
                        Dictionary<Skladiste, Zakup[]> skladistaSaIstorijomZakupa = await client.VratiSkladistaSaIstorijomZakupaAsync();

                        foreach (var skladisteIZakupi in skladistaSaIstorijomZakupa)
                        {
                            Skladiste s = skladisteIZakupi.Key;
                            Zakup[] zakupi = skladisteIZakupi.Value;

                            Console.WriteLine($"\nID: `{s.Id}`, Cena: {s.Cena}");
                            foreach (var z in zakupi)
                                Console.WriteLine($"\t\tJMBG vlasnika: `{z.JmbgVlasnika}`, ID skladista: `{z.IdSkladista}`, " +
                                    $"Pocetak zakupa: `{z.PocetakZakupa}`, Kraj zakupa: `{z.KrajZakupa}`");
                        }
                        break;
                    case '5':
                        Console.WriteLine("Izlazak.");
                        return;
                }
            }
        }
    }
}
