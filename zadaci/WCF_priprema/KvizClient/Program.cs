using KvizClient.KvizServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace KvizClient
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            KvizServiceClient kvizClient = new KvizServiceClient();
            bool exited = false;

            while (!exited)
            {
                Console.WriteLine(
                    "Izaberite opciju:\n" +
                    "\t[1] - Dodaj pitanje.\n" +
                    "\t[2] - Izmeni pitanje.\n" +
                    "\t[3] - Izlistaj pitanja.\n" +
                    "\t[4] - Kviz.\n" +
                    "\t[5] - Izadji.");

                char opcija = Console.ReadKey().KeyChar;
                Console.WriteLine();

                switch (opcija)
                {
                    case '1':
                        Console.Write("Unesite tekst pitanja: ");
                        string tekstPitanja = Console.ReadLine();

                        List<string> ponudjeniOdgovori = new List<string>();
                        for (int i = 0; i < 3; ++i)
                        {
                            Console.Write($"Unesite {i + 1}. ponudjeni odgovor: ");
                            ponudjeniOdgovori.Add(Console.ReadLine());
                        }

                        Console.Write("Unesite redni broj tacnog odgovora: ");
                        int redniBrojTacnogOdgovora;
                        if (!int.TryParse(Console.ReadLine(), out redniBrojTacnogOdgovora))
                        {
                            Console.WriteLine("Netacno unesen broj!");
                            break;
                        }

                        await kvizClient.DodajPitanjeAsync(new Pitanje
                        {
                            TekstPitanja = tekstPitanja,
                            PonudjeniOdgovori = ponudjeniOdgovori.ToArray(),
                            RedniBrojTacnogOdgovora = redniBrojTacnogOdgovora
                        });

                        Console.WriteLine("Dodali ste novo pitanje.");

                        break;
                    case '2':
                        List<Pitanje> listaPitanja = new List<Pitanje>(await kvizClient.VratiPitanjaAsync());

                        for (int i = 0; i < listaPitanja.Count; ++i)
                            Console.WriteLine($"\n{i + 1}. {listaPitanja[i].TekstPitanja}\n\t" + $"{string.Join("\n\t", listaPitanja[i].PonudjeniOdgovori)}\n\t"
                                + $"Tacan odgovor: {listaPitanja[i].PonudjeniOdgovori[listaPitanja[i].RedniBrojTacnogOdgovora - 1]}");

                        Console.Write("Unesite redni broj pitanja koje zelite da izmenite: ");

                        int redniBrojPitanjaZaZamenu;
                        if (!int.TryParse(Console.ReadLine(), out redniBrojPitanjaZaZamenu))
                        {
                            Console.WriteLine("Netacno unesen broj!");
                            break;
                        }

                        Console.Write("Unesite tekst novog pitanja: ");
                        tekstPitanja = Console.ReadLine();

                        ponudjeniOdgovori = new List<string>();
                        for (int i = 0; i < 3; ++i)
                        {
                            Console.Write($"Unesite {i + 1}. ponudjeni odgovor novog pitanja: ");
                            ponudjeniOdgovori.Add(Console.ReadLine());
                        }

                        Console.Write("Unesite redni broj tacnog odgovora novog pitanja: ");
                        if (!int.TryParse(Console.ReadLine(), out redniBrojTacnogOdgovora))
                        {
                            Console.WriteLine("Netacno unesen broj!");
                            break;
                        }

                        await kvizClient.IzmeniPitanjeAsync(redniBrojPitanjaZaZamenu - 1, new Pitanje
                        {
                            TekstPitanja = tekstPitanja,
                            PonudjeniOdgovori = ponudjeniOdgovori.ToArray(),
                            RedniBrojTacnogOdgovora = redniBrojTacnogOdgovora
                        });
                        break;
                    case '3':
                        listaPitanja = new List<Pitanje>(await kvizClient.VratiPitanjaAsync());

                        for (int i = 0; i < listaPitanja.Count; ++i)
                            Console.WriteLine($"\n{i + 1}. {listaPitanja[i].TekstPitanja}\n\t" + $"{string.Join("\n\t", listaPitanja[i].PonudjeniOdgovori)}\n\t"
                                + $"Tacan odgovor: {listaPitanja[i].PonudjeniOdgovori[listaPitanja[i].RedniBrojTacnogOdgovora - 1]}");
                        break;
                    case '4':
                        listaPitanja = new List<Pitanje>(await kvizClient.VratiPitanjaAsync());
                        List<int> odgovori = new List<int>();
                        for (int i = 0; i < listaPitanja.Count; ++i)
                        {
                            Console.WriteLine($"\n{i + 1}. {listaPitanja[i].TekstPitanja}\n\t" + $"{string.Join("\n\t", listaPitanja[i].PonudjeniOdgovori)}");
                            Console.Write("\n\tVas odgovor: ");
                            int redniBrojOdgovora = -1;
                            int.TryParse(Console.ReadLine(), out redniBrojOdgovora);
                            odgovori.Add(redniBrojOdgovora);
                        }

                        double procenatTacnih = await kvizClient.EvaluirajRezultatAsync(odgovori.ToArray());

                        Console.WriteLine($"Osvojili ste rezultat od {procenatTacnih}%");
                        break;
                    case '5':
                        exited = true;
                        break;
                    default:
                        Console.WriteLine("Opcija nepoznata. Pokusajte ponovo!");
                        break;
                }
            }
        }
    }
}
