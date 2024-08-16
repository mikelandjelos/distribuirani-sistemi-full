using RegistracijaVozilaClient.RegistracijaVozilaServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistracijaVozilaClient
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            RegistracijaVozilaServiceClient client = new RegistracijaVozilaServiceClient();

            while (true)
            {
                Console.WriteLine("Izaberite opciju:\n" +
                    "\t[1] - Registruj vozilo.\n" +
                    "\t[2] - Vrati listu vozila za vlasnika.\n" +
                    "\t[3] - Vrati istoriju registracija za dato vozilo.\n" +
                    "\t[4] - Vrati listu svih vozila sa registracijama.\n" +
                    "\t[5] - Izadji.");

                char opcija = Console.ReadKey().KeyChar;
                Console.WriteLine();

                switch (opcija)
                {
                    default: Console.WriteLine("Nepoznata opcija! Pokusajte ponovo."); break;
                    case '1':
                        Vlasnik vlasnik = new Vlasnik();
                        Console.Write("Unesite ime: ");
                        vlasnik.Ime = Console.ReadLine();
                        Console.Write("Unesite prezime: ");
                        vlasnik.Prezime = Console.ReadLine();
                        Console.Write("Unesite JMBG: ");
                        vlasnik.Jmbg = Console.ReadLine();

                        Vozilo vozilo = new Vozilo();
                        Console.Write("Unesite marku: ");
                        vozilo.Marka = Console.ReadLine();
                        Console.Write("Unesite model: ");
                        vozilo.Model = Console.ReadLine();
                        Console.Write("Unesite boju: ");
                        vozilo.Boja = Console.ReadLine();

                        Console.Write("Unesite datum kraja registracije [MM/dd/yyyy hh/mm]: ");
                        DateTime krajRegistracije = DateTime.Parse(Console.ReadLine());

                        Registracija registracija = await client.RegistrujVoziloAsync(vlasnik, vozilo, krajRegistracije);

                        Console.WriteLine("Uspesno registrovano vozilo!");
                        break;
                    case '2':
                        Console.Write("Unesite JMBG vlasnika: ");
                        string jmbgVlasnika = Console.ReadLine();
                        Vozilo[] vozila = await client.VratiVozilaAsync(jmbgVlasnika);

                        foreach (var v in vozila)
                            Console.WriteLine($"Marka: `{v.Marka}`, Model: `{v.Model}`, Boja: `{v.Boja}`");
                        break;
                    case '3':
                        vozilo = new Vozilo();
                        Console.WriteLine("Unesite parametre zeljenog vozila.");
                        Console.Write("Marka: ");
                        vozilo.Marka = Console.ReadLine();
                        Console.Write("Model: ");
                        vozilo.Model = Console.ReadLine();
                        Console.Write("Boja: ");
                        vozilo.Boja = Console.ReadLine();

                        Registracija[] istorijaRegistracija = await client.VratiIstorijuRegistracijaAsync(vozilo);

                        foreach (var r in istorijaRegistracija)
                            Console.WriteLine($"JMBG vlasnika: `{r.JmbgVlasnika}`, Vozilo: `{r.IdVozila}`," +
                                $"PocetakRegistracije: `{r.PocetakRegistracije}`, KrajRegistracije: `${r.KrajRegistracije}`");
                        break;
                    case '4':
                        Dictionary<Vozilo, Registracija[]> vozilaSaRegistracijama = await client.VratiVozilaSaRegistracijamaAsync();

                        foreach (var voziloSaRegistracijama in vozilaSaRegistracijama)
                        {
                            Vozilo v = voziloSaRegistracijama.Key;
                            Registracija[] registracije = voziloSaRegistracijama.Value;

                            Console.WriteLine($"\tMarka: `{v.Marka}`, Model: `{v.Model}`, Boja: `{v.Boja}`");

                            foreach (var r in registracije)
                                Console.WriteLine($"\t\tJMBG vlasnika: `{r.JmbgVlasnika}`, Vozilo: `{r.IdVozila}`, " +
                                $"PocetakRegistracije: `{r.PocetakRegistracije}`, KrajRegistracije: `${r.KrajRegistracije}`");
                        }
                        break;
                    case '5':
                        Console.WriteLine("Izlazak...");
                        return;
                }
            }
        }
    }
}
