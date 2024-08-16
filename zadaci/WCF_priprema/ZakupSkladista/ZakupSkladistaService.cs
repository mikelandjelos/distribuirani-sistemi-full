using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Configuration;
using System.Text;

namespace ZakupSkladista
{
    // Kada se koristi InstanceContextMode.Single, mislim da nije potrebno voditi racuna o thread-safety-u
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class ZakupSkladistaService : IZakupSkladistaService
    {
        ConcurrentDictionary<string, Vlasnik> vlasnici = new ConcurrentDictionary<string, Vlasnik>();
        ConcurrentDictionary<string, Skladiste> skladista = new ConcurrentDictionary<string, Skladiste>();
        ConcurrentBag<Zakup> istorijaZakupa = new ConcurrentBag<Zakup>();

        public Zakup ZakupiSkladiste(Vlasnik vlasnik, Skladiste skladiste, DateTime pocetakZakupa, DateTime krajZakupa)
        {
            if (vlasnik == null) throw new ArgumentNullException();
            if (skladiste == null) throw new ArgumentNullException();
            if (pocetakZakupa >= krajZakupa)
                throw new ArgumentException("Datum pocetka zakupa mora biti manji od datuma kraja zakupa!");

            vlasnici.TryAdd(vlasnik.Jmbg, vlasnik);
            skladista.TryAdd(skladiste.Id, skladiste);

            Zakup noviZakup = new Zakup
            {
                JmbgVlasnika = vlasnik.Jmbg,
                IdSkladista = skladiste.Id,
                PocetakZakupa = pocetakZakupa,
                KrajZakupa = krajZakupa
            };

            if (istorijaZakupa.Any(zakup =>
                    zakup.IdSkladista == skladiste.Id &&
                    zakup.PocetakZakupa >= pocetakZakupa &&
                    zakup.KrajZakupa <= krajZakupa))
                throw new ArgumentException("Termin za dato skladiste vec zauzet!");

            istorijaZakupa.Add(noviZakup);
            return noviZakup;
        }

        public List<Skladiste> VratiAktivnaSkladista(string jmbgVlasnika)
        {
            List<Skladiste> aktivnaSkladista = new List<Skladiste>();

            foreach (var zakup in istorijaZakupa)
                if (zakup.JmbgVlasnika == jmbgVlasnika && zakup.PocetakZakupa < DateTime.Now && zakup.KrajZakupa > DateTime.Now)
                    aktivnaSkladista.Add(skladista[zakup.IdSkladista]);

            return aktivnaSkladista;
        }

        public Dictionary<Skladiste, List<Zakup>> VratiSkladistaSaIstorijomZakupa()
        {
            Dictionary<Skladiste, List<Zakup>> skladistaSaIstorijomZakupa = new Dictionary<Skladiste, List<Zakup>>();
            foreach (var zakup in istorijaZakupa)
            {
                if (!skladistaSaIstorijomZakupa.ContainsKey(skladista[zakup.IdSkladista]))
                    skladistaSaIstorijomZakupa[skladista[zakup.IdSkladista]] = new List<Zakup>();
                skladistaSaIstorijomZakupa[skladista[zakup.IdSkladista]].Add(zakup);
            }
            return skladistaSaIstorijomZakupa;
        }

        public List<Vlasnik> VratiVlasnikeAktivnihSkladista()
        {
            List<Vlasnik> vlasniciAktivnihZakupa = new List<Vlasnik>();

            foreach (var zakup in istorijaZakupa)
                if (zakup.PocetakZakupa < DateTime.Now && zakup.KrajZakupa > DateTime.Now)
                    vlasniciAktivnihZakupa.Add(vlasnici[zakup.JmbgVlasnika]);

            return vlasniciAktivnihZakupa;
        }
    }
}
