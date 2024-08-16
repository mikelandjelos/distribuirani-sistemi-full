using MatricnaIzracunavanjaClient.MatricnaIzracunavanjaServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatricnaIzracunavanjaClient
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            MatricnaIzracunavanjaServiceClient client = new MatricnaIzracunavanjaServiceClient();

            while (true)
            {
                Console.WriteLine("Izaberite opciju:\n" +
                    "\t[1] - Postavi matricu.\n" +
                    "\t[2] - Preuzmi matricu.\n" +
                    "\t[3] - Saberi matrice.\n" +
                    "\t[4] - Pomnozi matricu skalarom.\n" +
                    "\t[5] - Pomnozi matrice.\n" +
                    "\t[6] - Transponuj matricu.\n" +
                    "\t[7] - Izlaz.");

                char opcija = Console.ReadKey().KeyChar;
                Console.WriteLine();

                switch (opcija)
                {
                    default: Console.WriteLine("Nepoznata opcija!"); break;
                    case '1':
                        Console.WriteLine("Unesite dimenzije matrice.");
                        Console.Write("Broj vrsta: ");
                        int brojVrsta = int.Parse(Console.ReadLine());
                        Console.Write("Broj kolona: ");
                        int brojKolona = int.Parse(Console.ReadLine());

                        Matrica matrica = new Matrica
                        {
                            BrojVrsta = brojVrsta,
                            BrojKolona = brojKolona,
                            Elementi = new int[brojVrsta * brojKolona]
                        };

                        Console.WriteLine("Unesite elementa matrice:");
                        for (int i = 0; i < matrica.BrojVrsta; ++i)
                            for (int j = 0; j < matrica.BrojKolona; ++j)
                            {
                                Console.Write($"[{i}, {j}]: ");
                                matrica.Elementi[i * matrica.BrojKolona + j] = int.Parse(Console.ReadLine());
                            }

                        Rezultat rezultat = await client.PostaviAsync(matrica);

                        StampanjeRezultata(rezultat);
                        break;
                    case '2':
                        rezultat = await client.PreuzmiMatricuAsync();

                        StampanjeRezultata(rezultat);
                        break;
                    case '3':
                        Console.WriteLine("Unesite dimenzije matrice.");
                        Console.Write("Broj vrsta: ");
                        brojVrsta = int.Parse(Console.ReadLine());
                        Console.Write("Broj kolona: ");
                        brojKolona = int.Parse(Console.ReadLine());

                        matrica = new Matrica
                        {
                            BrojVrsta = brojVrsta,
                            BrojKolona = brojKolona,
                            Elementi = new int[brojVrsta * brojKolona]
                        };

                        Console.WriteLine("Unesite elementa matrice:");
                        for (int i = 0; i < matrica.BrojVrsta; ++i)
                            for (int j = 0; j < matrica.BrojKolona; ++j)
                            {
                                Console.Write($"[{i}, {j}]: ");
                                matrica.Elementi[i * matrica.BrojKolona + j] = int.Parse(Console.ReadLine());
                            }

                        rezultat = await client.SabiranjeAsync(matrica);

                        StampanjeRezultata(rezultat);
                        break;
                    case '4':
                        Console.Write("Unesite skalar: ");
                        int skalar = int.Parse(Console.ReadLine());

                        rezultat = await client.MnozenjeSkalaromAsync(skalar);

                        StampanjeRezultata(rezultat);
                        break;
                    case '5':
                        Console.WriteLine("Unesite dimenzije matrice.");
                        Console.Write("Broj vrsta: ");
                        brojVrsta = int.Parse(Console.ReadLine());
                        Console.Write("Broj kolona: ");
                        brojKolona = int.Parse(Console.ReadLine());

                        matrica = new Matrica
                        {
                            BrojVrsta = brojVrsta,
                            BrojKolona = brojKolona,
                            Elementi = new int[brojVrsta * brojKolona]
                        };

                        Console.WriteLine("Unesite elementa matrice:");
                        for (int i = 0; i < matrica.BrojVrsta; ++i)
                            for (int j = 0; j < matrica.BrojKolona; ++j)
                            {
                                Console.Write($"[{i}, {j}]: ");
                                matrica.Elementi[i * matrica.BrojKolona + j] = int.Parse(Console.ReadLine());
                            }

                        rezultat = await client.MnozenjeMatricomAsync(matrica);

                        StampanjeRezultata(rezultat);
                        break;
                    case '6':
                        rezultat = await client.TransponujAsync();

                        StampanjeRezultata(rezultat);
                        break;
                    case '7':
                        Console.WriteLine("Izlazak...");
                        return;
                }
            }

        }

        public static void StampanjeMatrice(Matrica matrica)
        {
            for (int i = 0; i < matrica.BrojVrsta; ++i)
            {
                for (int j = 0; j < matrica.BrojKolona; ++j)
                    Console.Write($"{matrica.Elementi[i * matrica.BrojKolona + j]} ");
                Console.WriteLine();
            }
        }
        public static void StampanjeRezultata(Rezultat rezultat)
        {
            Console.WriteLine($"Uspeh: {rezultat.Uspeh}");
            Console.WriteLine($"Poruka: {rezultat.Poruka}");
            if (rezultat.Matrica != null)
            {
                Console.WriteLine($"Dimenzije: ({rezultat.Matrica.BrojVrsta}, {rezultat.Matrica.BrojKolona})");
                StampanjeMatrice(rezultat.Matrica);
            }
        }
    }
}
