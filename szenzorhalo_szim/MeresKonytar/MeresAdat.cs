//DLL-t készítette: Udvardy Balázs


using System;
using System.Collections.Generic;
namespace MeresKonyvtar
{

    // Létrehoztunk egy  Mérés adatokat tároló osztályt
    public class MeresAdat
    {
        // be állítjuk a get set metódusokat a tulajdonságokhoz
        public int SzenzorID { get; set; }
        public int VizszintCm { get; set; }
    }

    // Szenzor adatokat generáló osztály Math.Random segítségével
    public class SzenzorMeresGeneralas 
    {
        // Szenzor adatokat generáló osztály Math.Random segítségével
        private Random rnd = new Random();

        // Generál 5 szenzor mérési adatot egy listában visszaadva
        public List<MeresAdat> Meresgeneralas(bool GarantaltAradas = false)
        {
            List<MeresAdat> meresek = new List<MeresAdat>();

            for (int i = 1; i <= 5; i++)
            {
                // Véletlenszerű vízszint generálása
                int vizszint;

                // Ha garantált áradást kértünk, az első 3 szenzor (az 5-ből) biztosan 400 fölé megy
                if (GarantaltAradas && i <= 3)
                {
                    vizszint = rnd.Next(401, 501); // 401 - 500 közötti "vészhelyzeti" érték
                }
                else
                {
                    vizszint = rnd.Next(0, 501); // Normál  működés
                }
                // Mérési adatok hozzáadása a listához
                meresek.Add(new MeresAdat
                {
                    // Szenzor azonosítót az aktuális ciklusindex alapján állítjuk be
                    SzenzorID = i,
                    // Egyszerű hozzárendelés a véletlenszerűen generált vízszinthez
                    VizszintCm = vizszint
                });
            }
            // Visszatérünk a generált mérési adatokkal
            return meresek;
        }
    }
}

//DLL-t készítette: Udvardy Balázs
