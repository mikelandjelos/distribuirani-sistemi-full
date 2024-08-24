using RegistracijaParcelaClient.RegistracijaParcelaServiceReference;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistracijaParcelaClient
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Guid clientGuid = Guid.NewGuid();
            RegistracijaParcelaServiceClient proxy = new RegistracijaParcelaServiceClient();

            Console.WriteLine($"Primljeno:\n\t\"{await proxy.EchoTestAsync(clientGuid.ToString())}\"");

            Console.WriteLine("Pritisnite ENTER za nastavak...");
            Console.ReadKey();
            Console.Clear();

            Console.WriteLine("Registrovanje parcela u toku...");

            string sandra = "Sandra Miljkovic";
            Parcela sandrinaPrvaParcela = new Parcela
            {
                ImeVlasnika = sandra,
                Koordinate = new Tuple<decimal, decimal>[]
                {
                    Tuple.Create(1.1m * 100, 2.2m * 100),
                    Tuple.Create(3.3m * 100, 4.4m * 100),
                    Tuple.Create(5.5m * 100, 7.7m * 100)
                }
            };

            await proxy.RegistrujParceluAsync(sandrinaPrvaParcela, sandra);

            Parcela sandrinaDrugaParcela = new Parcela
            {
                ImeVlasnika = sandra,
                Koordinate = new Tuple<decimal, decimal>[]
                {
                    Tuple.Create(7.7m * 100, 8.8m * 100),
                    Tuple.Create(9.9m * 100, 10.10m * 100),
                    Tuple.Create(11.11m * 100, 12.12m * 100)
                }
            };

            await proxy.RegistrujParceluAsync(sandrinaDrugaParcela, sandra);

            string mihajlo = "Mihajlo Madic";
            Parcela mihajlovaPrvaParcela = new Parcela
            {
                ImeVlasnika = mihajlo,
                Koordinate = new Tuple<decimal, decimal>[]
                {
                    Tuple.Create(7.7m * 100, 8.8m * 100),
                    Tuple.Create(9.9m * 100, 10.10m * 100),
                    Tuple.Create(14.14m * 100, 15.15m * 100)
                }
            };

            await proxy.RegistrujParceluAsync(mihajlovaPrvaParcela, mihajlo);

            Console.WriteLine("Pritisnite ENTER za nastavak...");
            Console.ReadKey();

            Console.WriteLine($"\t\tSVE PARCELE KORISNIKA `{sandra}`");
            foreach (var parcelaSaPovrsinom in await proxy.VratiParceleSaPovrsinomAsync(sandra))
            {
                (Parcela parcela, decimal povrsina) = parcelaSaPovrsinom;
                Console.WriteLine($"Vlasnik: `{parcela.ImeVlasnika}`;" +
                    $"\nKoordinate: [\n\t{string.Join(",\n\t", parcela.Koordinate.Select(koordinate => $"({koordinate.Item1}, {koordinate.Item2})"))}\n];" +
                    $"\nPovrsina: `{povrsina}`m2");
            }

            Console.WriteLine($"\t\tSVE PARCELE KORISNIKA `{mihajlo}`");
            foreach (var parcelaSaPovrsinom in await proxy.VratiParceleSaPovrsinomAsync(mihajlo))
            {
                (Parcela parcela, decimal povrsina) = parcelaSaPovrsinom;
                Console.WriteLine($"Vlasnik: `{parcela.ImeVlasnika}`;" +
                    $"\nKoordinate: [\n\t{string.Join(",\n\t", parcela.Koordinate.Select(koordinate => $"({koordinate.Item1}, {koordinate.Item2})"))}\n];" +
                    $"\nPovrsina: `{povrsina}`m2");
            }

            Console.WriteLine("Pritisnite ENTER za nastavak...");
            Console.ReadKey();
            Console.Clear();

            Console.WriteLine("\t\tSVE PARCELE");
            foreach (var parcela in await proxy.VratiSveParceleAsync())
            {
                Console.WriteLine($"Vlasnik: `{parcela.ImeVlasnika}`;" +
                    $"\nKoordinate: [\n\t{string.Join(",\n\t", parcela.Koordinate.Select(koordinate => $"({koordinate.Item1}, {koordinate.Item2})"))}\n];");
            }

            Console.WriteLine("Pritisnite ENTER za nastavak...");
            Console.ReadKey();
            Console.Clear();

            decimal ari = 100;
            Console.WriteLine($"\t\tSVE PARCELE VECE OD {ari} ARA");
            foreach (var parcela in await proxy.VratiVeceParceleAsync(ari))
            {
                Console.WriteLine($"Vlasnik: `{parcela.ImeVlasnika}`;" +
                    $"\nKoordinate: [\n\t{string.Join(",\n\t", parcela.Koordinate.Select(koordinate => $"({koordinate.Item1}, {koordinate.Item2})"))}\n];");
            }

            ari = 150;
            Console.WriteLine($"\t\tSVE PARCELE VECE OD {ari} ARA");
            foreach (var parcela in await proxy.VratiVeceParceleAsync(ari))
            {
                Console.WriteLine($"Vlasnik: `{parcela.ImeVlasnika}`;" +
                    $"\nKoordinate: [\n\t{string.Join(",\n\t", parcela.Koordinate.Select(koordinate => $"({koordinate.Item1}, {koordinate.Item2})"))}\n];");
            }

            ari = 200;
            Console.WriteLine($"\t\tSVE PARCELE VECE OD {ari} ARA");
            foreach (var parcela in await proxy.VratiVeceParceleAsync(ari))
            {
                Console.WriteLine($"Vlasnik: `{parcela.ImeVlasnika}`;" +
                    $"\nKoordinate: [\n\t{string.Join(",\n\t", parcela.Koordinate.Select(koordinate => $"({koordinate.Item1}, {koordinate.Item2})"))}\n];");
            }

            Console.WriteLine("Pritisnite ENTER za nastavak...");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
