using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace RegistracijaVozila
{
    // Za razliku od Junskog, isprobavamo drugo resenje - InstanceContextMode.PerCall umesto InstanceContextMode.Single
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class RegistracijaVozilaService : IRegistracijaVozilaService
    {
        // Ne znam da li je u Junskom blanketu bilo potrebno da se pazi na konkurentnost, ali ovde sigurno da,
        // zbog InstanceContextMode.PerCall spojenog sa statickim atributima (koristice ih instance konkurentno)

        static ConcurrentDictionary<string, Vlasnik> vlasnici = new ConcurrentDictionary<string, Vlasnik>();
        static ConcurrentDictionary<string, Vozilo> vozila = new ConcurrentDictionary<string, Vozilo>();

        static ConcurrentBag<Registracija> registracije = new ConcurrentBag<Registracija>();

        public Registracija RegistrujVozilo(Vlasnik vlasnik, Vozilo vozilo, DateTime krajRegistracije)
        {
            if (vlasnik == null) throw new ArgumentNullException($"{nameof(RegistrujVozilo)}.{nameof(vlasnik)}");
            if (vozilo == null) throw new ArgumentNullException($"{nameof(RegistrujVozilo)}.{nameof(vozilo)}");
            if (krajRegistracije == null) throw new ArgumentNullException($"{nameof(RegistrujVozilo)}.{nameof(krajRegistracije)}");
            if (krajRegistracije <= DateTime.Now) throw new ArgumentException("Datum kraja registracije mora biti veci od trenutnog.");

            vlasnici.TryAdd(vlasnik.Jmbg, vlasnik);
            vozila.TryAdd(vozilo.ToString(), vozilo);

            Registracija registracija = new Registracija
            {
                IdVozila = vozilo.ToString(),
                JmbgVlasnika = vlasnik.Jmbg,
                PocetakRegistracije = DateTime.Now,
                KrajRegistracije = krajRegistracije,
            };

            registracije.Add(registracija);

            return registracija;
        }

        public List<Registracija> VratiIstorijuRegistracija(Vozilo vozilo)
        {
            List<Registracija> istorijaRegistracija = new List<Registracija>();

            foreach (var registracija in registracije)
                if (registracija.IdVozila == vozilo.ToString())
                    istorijaRegistracija.Add(registracija);

            return istorijaRegistracija;
        }

        public List<Vozilo> VratiVozila(string jmbgVlasnika)
        {
            List<Vozilo> vozilaVlasnika = new List<Vozilo>();

            foreach (var registracija in registracije)
                if (registracija.JmbgVlasnika == jmbgVlasnika)
                    vozilaVlasnika.Add(vozila[registracija.IdVozila]);

            return vozilaVlasnika;
        }

        public Dictionary<Vozilo, List<Registracija>> VratiVozilaSaRegistracijama()
        {
            Dictionary<Vozilo, List<Registracija>> vozilaSaRegistracijama = new Dictionary<Vozilo, List<Registracija>>();

            foreach (var registracija in registracije)
            {
                if (!vozilaSaRegistracijama.ContainsKey(vozila[registracija.IdVozila]))
                    vozilaSaRegistracijama.Add(vozila[registracija.IdVozila], new List<Registracija>());

                vozilaSaRegistracijama[vozila[registracija.IdVozila]].Add(registracija);
            }

            return vozilaSaRegistracijama;
        }
    }
}
