using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Chat
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class ChatService : IChatService
    {
        const string WILDCARD = "SVI";
        Dictionary<string, IChatServiceCallback> aktivniKlijenti = new Dictionary<string, IChatServiceCallback>();
        Dictionary<string, string> kredencijali = new Dictionary<string, string>();
        Dictionary<string, List<Poruka>> istorijaPrimljenihPoruka = new Dictionary<string, List<Poruka>>();

        IChatServiceCallback Callback => OperationContext.Current.GetCallbackChannel<IChatServiceCallback>();

        public void Registracija(string nadimak, string sifra)
        {
            if (nadimak == null)
                throw new ArgumentNullException("Morate proslediti nadimak!");
            if (sifra == null)
                throw new ArgumentNullException("Morate proslediti sifru!");
            if (nadimak == WILDCARD)
                throw new ArgumentException($"Prosledjeni nadimak ne sme imati vrednost `{WILDCARD}`!");
            if (kredencijali.ContainsKey(nadimak))
            {
                Callback.RegistracijaEvent($"Korisnik sa nadimkom `{nadimak}` vec postoji. " +
                    "Ukoliko ste to vi, izaberite opciju za prijavu!");
                return;
            }

            kredencijali.Add(nadimak, sifra);
            Callback.RegistracijaEvent($"Uspesna registracija, nadimak: `{nadimak}`");
        }

        public void PrijavaOdjava(string nadimak, string sifra)
        {
            if (nadimak == null)
                throw new ArgumentNullException("Morate proslediti nadimak!");
            if (sifra == null)
                throw new ArgumentNullException("Morate proslediti sifru!");
            if (nadimak == WILDCARD)
                throw new ArgumentException($"Prosledjeni nadimak ne sme imati vrednost `{WILDCARD}`!");
            if (!kredencijali.ContainsKey(nadimak))
            {
                Callback.PrijavaOdjavaEvent("Korisnik sa datim nadimkom ne postoji. Morate se registrovati pre prijave!");
                return;
            }
            if (kredencijali[nadimak] != sifra)
            {
                Callback.PrijavaOdjavaEvent("Pogresna sifra. Pokusajte ponovo!");
                return;
            }

            if (aktivniKlijenti.ContainsKey(nadimak))
            {
                aktivniKlijenti.Remove(nadimak);
                Callback.PrijavaOdjavaEvent($"Uspesna odjava `{nadimak}`.");
            }
            else
            {
                Callback.PrijavaOdjavaEvent($"Uspesna prijava `{nadimak}`.");
                aktivniKlijenti.Add(nadimak, Callback);
            }
        }

        public void Posalji(Poruka poruka)
        {
            if (poruka == null)
                throw new ArgumentNullException("Morate proslediti poruku!");
            if (poruka.Primalac == null)
                throw new ArgumentNullException("Morate proslediti nadimak primaoca (ili niz `{WILDCARD}` da bi poslali svima)!");
            if (poruka.Primalac == WILDCARD)
            {
                foreach (var kvPrimalacCallback in aktivniKlijenti)
                {
                    string nadimakPrimaoca = kvPrimalacCallback.Key;
                    IChatServiceCallback chatServiceCallback = kvPrimalacCallback.Value;

                    if (!istorijaPrimljenihPoruka.ContainsKey(nadimakPrimaoca))
                        istorijaPrimljenihPoruka.Add(nadimakPrimaoca, new List<Poruka>());

                    istorijaPrimljenihPoruka[nadimakPrimaoca].Add(poruka);

                    chatServiceCallback.PoslataPorukaEvent(poruka);
                }
            }
            else
            {
                if (!istorijaPrimljenihPoruka.ContainsKey(poruka.Primalac))
                    istorijaPrimljenihPoruka.Add(poruka.Primalac, new List<Poruka>());

                istorijaPrimljenihPoruka[poruka.Primalac].Add(poruka);

                aktivniKlijenti[poruka.Primalac].PoslataPorukaEvent(poruka);
            }
        }

        public void IstorijaPrimljenihPoruka(string nadimak, DateTime pocetak, DateTime kraj)
        {
            if (pocetak == null)
                throw new ArgumentNullException("Morate proslediti datum pocetka vremenskog perioda za koji hocete da vidite poruke!");
            if (pocetak == null)
                throw new ArgumentNullException("Morate proslediti datum kraja vremenskog perioda za koji hocete da vidite poruke!");
            if (pocetak >= kraj)
                throw new ArgumentException("Datum pocetka mora biti manji od datuma kraja!");

            List<Poruka> istorijaPoruka;
            istorijaPrimljenihPoruka.TryGetValue(nadimak, out istorijaPoruka);
            Callback.ZatrazenaIstorijaPrimljenihPorukaEvent(
                istorijaPoruka.Where(poruka => poruka.VremeSlanja >= pocetak && poruka.VremeSlanja <= kraj).ToList()
                ?? new List<Poruka>());
        }
    }

}
