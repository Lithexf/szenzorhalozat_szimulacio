using System;
using System.Collections.Generic;
using System.Linq; // Szükséges a LINQ függvények (Count, Where, Average) használatához!
using SzenzorHalozatLogika;

namespace SzenzorHalozatLogika
{
    public class ElemzoMotor
    {
        // Konstansok definiálása a "magic numbers" (varázsszámok) elkerülésére.
        // Ezek az értékek a feladatkiírásból származnak.
        public const int ARVIZ_KUSZOB = 400; // 400 cm feletti érték számít
        public const int SZUKSEGES_SZENZOROK_ARVIZHOZ = 3; // 3 szenzornak kell ezt elérnie

        // Metódus, ami legenerálja a szenzorok véletlenszerű értékeit.
        public List<int> SzenzorErtekekGeneralasa(int szenzorSzam, int min, int max, Random veletlen)
        {
            // Létrehozunk egy üres listát az értékek tárolására.
            var ertekek = new List<int>();

            // Egy ciklussal annyi értéket generálunk, ahány szenzor van (szenzorSzam).
            for (int i = 0; i < szenzorSzam; i++)
            {
                // A veletlen.Next(min, max + 1) generál egy egész számot
                // a [min, max] tartományban (a 'max' felső határ exkluzív, 
                // ezért kell a max + 1, hogy az 500-at is belevegye).
                ertekek.Add(veletlen.Next(min, max + 1));
            }
            return ertekek; // Visszaadjuk a generált listát.
        }

        // Ez a metódus ellenőrzi az "Áradás" esemény feltételét.
        public bool ArvizHelyzetEllenorzese(List<int> ertekek)
        {
            // **LINQ (1. típus): Count()**
            // A LINQ segítségével megszámoljuk, hogy az 'ertekek' listában
            // hány olyan elem (e) van, amelyre igaz, hogy e > ARVIZ_KUSZOB (400).
            int magasErtekuSzenzorokSzama = ertekek.Count(e => e > ARVIZ_KUSZOB);

            // Visszaadunk egy logikai értéket: igaz, ha a magas értékű 
            // szenzorok száma eléri (>=) a szükséges küszöböt (3).
            return magasErtekuSzenzorokSzama >= SZUKSEGES_SZENZOROK_ARVIZHOZ;
        }

        // Ez a metódus visszaadja a veszélyes (küszöb feletti) szenzorok értékeit.
        public List<int> MagasKockazatuSzenzorokLekerdezese(List<int> ertekek)
        {
            // **LINQ (2. típus): Where() és ToList()**
            // A LINQ 'Where' metódusával kiválogatjuk az összes olyan
            // elemet (e) a listából, amely nagyobb, mint a küszöb (400).
            // A 'ToList()' alakítja az eredményt vissza List<int> formátumra.
            return ertekek.Where(e => e > ARVIZ_KUSZOB).ToList();
        }

        // Ez a metódus kiszámolja az összes szenzor átlagos vízszintjét.
        public double AtlagSzintLekerdezese(List<int> ertekek)
        {
            // **LINQ (3. típus): Average()**
            // A LINQ 'Average' metódusa kiszámolja a listában lévő
            // numerikus értékek matematikai átlagát.
            return ertekek.Average();
        }
    }
}