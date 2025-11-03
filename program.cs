using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using SzenzorAdatokDLL;

namespace VizszintMeroProgram
{
class Program
{
    private static List<MeresAdat> osszesitettMeresek = new List<MeresAdat>();
    private static int aradasokSzama = 0;

    static void Main(string[] args)
    {
        Console.WriteLine("=== VÍZSZINT MÉRŐ SZENZORHÁLÓZAT ===");
        Console.WriteLine();

        // Szenzor hálózat létrehozása
        SzenzorHalozat halozat = new SzenzorHalozat();

        // Áradás esemény feliratkozás (delegált használata)
        halozat.AradasEsemeny += Halozat_AradasEsemeny;

        // 10 mérési kör szimulálása
        for (int i = 1; i <= 10; i++)
        {
            Console.WriteLine($"\n--- {i}. MÉRÉSI KÖR ---");
            Console.WriteLine($"Időpont: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            Console.WriteLine();

            halozat.UjMeresKor();

            // Mérések megjelenítése
            foreach (var szenzor in halozat.GetSzenzorok())
            {
                Console.WriteLine(szenzor.ToString());

                // Mérés mentése a listába
                osszesitettMeresek.Add(new MeresAdat
                {
                    SzenzorId = szenzor.Id,
                    SzenzorNev = szenzor.Nev,
                    Vizszint = szenzor.Vizszint,
                    MeresIdeje = szenzor.MeresIdeje,
                    MeresKor = i
                });
            }

            // Várakozás a következő mérési körig
            System.Threading.Thread.Sleep(1000);
        }

        // Adatok mentése idet fájlokba

    } //*-

    // Áradás esemény kezelő (delegált használata)
    //
    //*
    ///

  }
}