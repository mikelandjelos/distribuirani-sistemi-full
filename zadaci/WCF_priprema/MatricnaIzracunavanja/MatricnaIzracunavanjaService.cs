using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.ServiceModel;
using System.Text;

namespace MatricnaIzracunavanja
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class MatricnaIzracunavanjaService : IMatricnaIzracunavanjaService
    {
        Matrica matrica;

        public Rezultat Postavi(Matrica matrica)
        {
            if (matrica == null)
                return new Rezultat
                {
                    Matrica = null,
                    Uspeh = false,
                    Poruka = $"Nevalidna vrednost matrice: (null)",
                };
            this.matrica = matrica;
            return new Rezultat
            {
                Matrica = matrica,
                Uspeh = true,
                Poruka = "Matrica uspesno postavljena.",
            };
        }

        public Rezultat PreuzmiMatricu()
        {
            if (matrica == null)
                return new Rezultat
                {
                    Matrica = null,
                    Uspeh = false,
                    Poruka = "Matrica nije postavljena!",
                };
            return new Rezultat
            {
                Matrica = matrica,
                Uspeh = true,
                Poruka = "Uspesno preuzimanje matrice.",
            };
        }

        public Rezultat Sabiranje(Matrica matrica)
        {
            if (this.matrica == null)
                return new Rezultat
                {
                    Matrica = null,
                    Uspeh = false,
                    Poruka = "Serverska instanca matrice nije postavljena!",
                };
            if (matrica == null)
                return new Rezultat
                {
                    Matrica = null,
                    Uspeh = false,
                    Poruka = "Prosledjena nevalidna vrednost drugog operanda: (null)",
                };
            if (this.matrica.BrojVrsta != matrica.BrojVrsta)
                return new Rezultat
                {
                    Matrica = null,
                    Uspeh = false,
                    Poruka = $"Prve dimenzije matrica nisu kompatibilne ({this.matrica.BrojVrsta} != {matrica.BrojVrsta})!",
                };
            if (this.matrica.BrojKolona != matrica.BrojKolona)
                return new Rezultat
                {
                    Matrica = null,
                    Uspeh = false,
                    Poruka = $"Druge dimenzije matrica nisu kompatibilne ({this.matrica.BrojKolona}  !=  {matrica.BrojKolona})!",
                };

            for (int i = 0; i < this.matrica.BrojVrsta * this.matrica.BrojKolona; ++i)
                this.matrica.Elementi[i] += matrica.Elementi[i];

            return new Rezultat
            {
                Matrica = this.matrica,
                Uspeh = true,
                Poruka = "Uspesno sabrane matrice",
            };
        }

        public Rezultat MnozenjeSkalarom(int skalar)
        {
            if (matrica == null)
                return new Rezultat
                {
                    Matrica = null,
                    Uspeh = false,
                    Poruka = "Serverska instanca matrice nije postavljena!",
                };

            for (int i = 0; i < matrica.BrojVrsta * this.matrica.BrojKolona; ++i)
                matrica.Elementi[i] *= skalar;

            return new Rezultat
            {
                Matrica = matrica,
                Uspeh = true,
                Poruka = "Matrica uspesno pomnozena skalarom!",
            };
        }

        public Rezultat MnozenjeMatricom(Matrica matrica)
        {
            if (this.matrica == null)
                return new Rezultat
                {
                    Matrica = null,
                    Uspeh = false,
                    Poruka = "Serverska instanca matrice nije postavljena!",
                };
            if (matrica == null)
                return new Rezultat
                {
                    Matrica = null,
                    Uspeh = false,
                    Poruka = "Prosledjena nevalidna vrednost drugog operanda: (null)",
                };
            if (this.matrica.BrojKolona != matrica.BrojVrsta)
                return new Rezultat
                {
                    Matrica = null,
                    Uspeh = false,
                    Poruka = $"Matrice nisu kompatibilne za operaciju mnozenja ({this.matrica.BrojKolona} != {matrica.BrojVrsta})!",
                };

            Matrica rezultat = new Matrica
            {
                Elementi = new int[this.matrica.BrojVrsta * matrica.BrojKolona],
                BrojVrsta = this.matrica.BrojVrsta,
                BrojKolona = matrica.BrojKolona,
            };

            for (int i = 0; i < this.matrica.BrojVrsta; ++i)
                for (int j = 0; j < matrica.BrojKolona; ++j)
                {
                    rezultat.Elementi[i * rezultat.BrojKolona + j] = 0;
                    for (int k = 0; k < this.matrica.BrojKolona; ++k)
                        rezultat.Elementi[i * rezultat.BrojKolona + j] +=
                            this.matrica.Elementi[i * this.matrica.BrojKolona + k] * matrica.Elementi[k * matrica.BrojKolona + j];
                }

            matrica = rezultat;

            return new Rezultat
            {
                Matrica = rezultat,
                Uspeh = true,
                Poruka = "Uspesno pomnozene matrice!",
            };
        }

        public Rezultat Transponuj()
        {
            if (matrica == null)
                return new Rezultat
                {
                    Matrica = null,
                    Uspeh = false,
                    Poruka = "Serverska instanca matrice nije postavljena!",
                };
            if (matrica.BrojVrsta != matrica.BrojKolona)
                return new Rezultat
                {
                    Matrica = null,
                    Uspeh = false,
                    Poruka = $"Nije moguce transponovati matricu dimenzija ({matrica.BrojVrsta}, {matrica.BrojKolona}) bez zauzimanja dodatne memorije!",
                };

            int pom;

            for (int i = 0; i < matrica.BrojVrsta; ++i)
                for (int j = i + 1; j < matrica.BrojKolona; ++j)
                {
                    pom = matrica.Elementi[i * matrica.BrojKolona + j];
                    matrica.Elementi[i * matrica.BrojKolona + j] = matrica.Elementi[j * matrica.BrojKolona + i];
                    matrica.Elementi[j * matrica.BrojKolona + i] = pom;
                }

            pom = matrica.BrojVrsta;
            matrica.BrojVrsta = matrica.BrojKolona;
            matrica.BrojKolona = pom;

            return new Rezultat
            {
                Matrica = matrica,
                Uspeh = true,
                Poruka = "Uspesno transponovanje!",
            };
        }
    }
}
