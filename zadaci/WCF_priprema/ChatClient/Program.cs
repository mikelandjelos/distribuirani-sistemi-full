using ChatClient.ChatServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ChatClient
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            using (var chatServiceProxy = new ChatServiceClient(new InstanceContext(new ChatServiceCallback())))
            {
                bool aktivnaSesija = true;
                while (aktivnaSesija)
                {
                    string nadimak = null, sifra = null;
                    while (nadimak == null || sifra == null)
                    {
                        Console.WriteLine("Izaberite opciju:\n" +
                            "\t[1] - Registracija.\n" +
                            "\t[2] - Prijava.\n" +
                            "\t[3] - Izlaz.");

                        char opcija = Console.ReadKey().KeyChar;
                        Console.WriteLine();

                        switch (opcija)
                        {
                            default: Console.WriteLine("Nepoznata opcija. Pokusajte ponovo!"); break;
                            case '1':
                                Console.Write("Unesite nadimak: ");
                                nadimak = Console.ReadLine();
                                Console.Write("Unesite sifru: ");
                                sifra = Console.ReadLine();

                                await chatServiceProxy.RegistracijaAsync(nadimak, sifra); // Ovo ce automatski da okine ispis.
                                nadimak = null;
                                sifra = null;
                                break;
                            case '2':
                                Console.Write("Unesite nadimak: ");
                                nadimak = Console.ReadLine();
                                Console.Write("Unesite sifru: ");
                                sifra = Console.ReadLine();

                                await chatServiceProxy.PrijavaOdjavaAsync(nadimak, sifra); // Kao i ovo.
                                break;
                            case '3':
                                Console.WriteLine("Izlaz...");
                                await chatServiceProxy.PrijavaOdjavaAsync(nadimak, sifra);
                                aktivnaSesija = false;
                                break;
                        }
                    }

                    while (nadimak != null && sifra != null)
                    {
                        Console.WriteLine("Izaberite opciju:\n" +
                            "\t[1] - Slanje poruke.\n" +
                            "\t[2] - Istorija primljenih poruka.\n" +
                            "\t[3] - Odjava.\n" +
                            "\t[4] - Izlaz.");

                        char opcija = Console.ReadKey().KeyChar;
                        Console.WriteLine();

                        switch (opcija)
                        {
                            default: Console.WriteLine("Nepoznata opcija. Pokusajte ponovo!"); break;
                            case '1':
                                Console.Write($"Kome zelite da posaljete poruku (unesite nadimak ili `SVI`): ");
                                string destinacija = Console.ReadLine();
                                Console.Write($"Unesite sadrzaj poruke: ");
                                string sadrzaj = Console.ReadLine();

                                await chatServiceProxy.PosaljiAsync(new Poruka
                                {
                                    Posiljalac = nadimak,
                                    Primalac = destinacija,
                                    VremeSlanja = DateTime.Now,
                                    Sadrzaj = sadrzaj,
                                });
                                break;
                            case '2':
                                Console.Write("Unesite datum pocetka vremenskog perioda za koji zelite da vidite istoriju poruka [MM/dd/yyyy hh:mm]: ");
                                DateTime pocetak = DateTime.Parse(Console.ReadLine());
                                Console.Write("Unesite datum kraja vremenskog perioda za koji zelite da vidite istoriju poruka [MM/dd/yyyy hh:mm]: ");
                                DateTime kraj = DateTime.Parse(Console.ReadLine());

                                await chatServiceProxy.IstorijaPrimljenihPorukaAsync(nadimak, pocetak, kraj);
                                break;
                            case '3':
                                await chatServiceProxy.PrijavaOdjavaAsync(nadimak, sifra);
                                nadimak = null;
                                sifra = null;
                                break;
                            case '4':
                                Console.WriteLine("Izlaz...");
                                await chatServiceProxy.PrijavaOdjavaAsync(nadimak, sifra);
                                aktivnaSesija = false;
                                break;
                        }
                    }
                }
            }
        }
    }
    internal class ChatServiceCallback : IChatServiceCallback
    {
        public void PoslataPorukaEvent(Poruka poruka)
        {
            Console.WriteLine($"Primili ste poruku. Posiljalac: `{poruka.Posiljalac}`, Primalac: `{poruka.Primalac}`\n" +
                $"[slanje: {poruka.VremeSlanja}, prijem: {DateTime.Now}] Sadrzaj: `{poruka.Sadrzaj}`");
        }

        // Nije bilo potrebe za `PrijavaOdjavaEvent` i `RegistracijaEvent` - mogao je samo jedan genericki, ali nebitno je.
        public void PrijavaOdjavaEvent(string povratneInformacije)
        {
            Console.WriteLine(povratneInformacije);
        }

        public void RegistracijaEvent(string povratneInformacije)
        {
            Console.WriteLine(povratneInformacije);
        }

        public void ZatrazenaIstorijaPrimljenihPorukaEvent(Poruka[] istorijaPoruka)
        {
            foreach (Poruka poruka in istorijaPoruka)
            {
                Console.WriteLine($"Posiljalac: `{poruka.Posiljalac}`, Primalac: `{poruka.Primalac}`\n" +
                    $"[slanje: {poruka.VremeSlanja}, prijem: {DateTime.Now}] Sadrzaj: `{poruka.Sadrzaj}`");
            }
        }
    }
}
