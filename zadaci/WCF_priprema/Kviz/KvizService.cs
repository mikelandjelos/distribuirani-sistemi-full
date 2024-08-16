using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Kviz
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class KvizService : IKvizService
    {
        static List<Pitanje> pitanja;

        public KvizService()
        {
            pitanja = new List<Pitanje>();
        }

        public void DodajPitanje(Pitanje pitanje)
        {
            pitanja.Add(pitanje);
        }

        public double EvaluirajRezultat(List<int> datiOdgovori)
        {
            double brojTacnihOdgovora = pitanja
                .Zip(datiOdgovori, (pitanje, redniBrojDatogOdgovora) => pitanje.RedniBrojTacnogOdgovora == redniBrojDatogOdgovora)
                .Count(evaluiraniOdgovori => evaluiraniOdgovori == true);
            return brojTacnihOdgovora / pitanja.Count * 100;
        }

        public List<Pitanje> VratiPitanja() => pitanja;

        public void IzmeniPitanje(int redniBroj, Pitanje novoPitanje)
        {
            if (redniBroj < 0 || redniBroj >= pitanja.Count)
                throw new ArgumentException("Pitanje sa datim rednim brojem ne postoji!");

            pitanja[redniBroj] = novoPitanje;
        }
    }
}
