using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using LiteDB;
using MeresKonyvtar;

namespace SzenzorSzimulacio
{
    // Sallai Martin --
    class SzenzorSzimulacio
    {
        public delegate void AradasEsemenykezelo(string uzenet);
        public static event AradasEsemenykezelo OnAradas;

        static void Main(string[] args)
        {
            Console.WriteLine("Szenzorhálózati Szimuláció");
            Console.WriteLine("Vízszint mérő program");
            Console.CursorVisible = false;

            OnAradas += FigyelmeztetesKiiro;
            SzenzorMeresGeneralas generator = new SzenzorMeresGeneralas();
            List<NapiMeres> hetiAdatok = new List<NapiMeres>();
            string[] napok = { "Hétfő", 
                                "Kedd", 
                                "Szerda", 
                                "Csütörtök", 
                                "Péntek", 
                                "Szombat", 
                                "Vasárnap" };

            //kötelező áradás nap kiválasztása 
            //esemény kezelés, esemény = áradás
            Random rnd = new Random();
            int katasztrofaEsemeny = rnd.Next(0, napok.Length);

            
            for (int i = 0; i < napok.Length; i++)
            {
                string nap = napok[i];
                Console.WriteLine($"\n\nJelenlegi Nap: {nap}");

                //megnézzük hogy garantáltan áradós nap van e
                bool maiNapLegyenAradas = (i == katasztrofaEsemeny);
                List<MeresAdat> aktualisMeresek = generator.Meresgeneralas(maiNapLegyenAradas); //mérés lista generálása


                int kritikusErtekekSzama = aktualisMeresek.Count(x => x.VizszintCm > 400); //ellenőrzés kritikus értékekre
                double atlagSzint = aktualisMeresek.Average(x => x.VizszintCm);
                string ertekekAdat = string.Join(", ", aktualisMeresek.Select(x => x.VizszintCm));

                Console.WriteLine($"A mai mérések: {ertekekAdat}"); //mérés kiiratása

                string statusz;
                if (kritikusErtekekSzama >= 3) //kondició
                {
                    statusz = "Áradás! ";
                    OnAradas?.Invoke($"Áradás Történt! (Kritikus Értékek: {kritikusErtekekSzama}db!)");
                }
                else if (kritikusErtekekSzama >= 2)  //kis figyelmeztetés
                {
                    statusz = "Magas Vízszint Veszélye";
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.ForegroundColor = ConsoleColor.Black;
                    SargaKiiro($"Státusz: {statusz} (Kritikus Értékek: {kritikusErtekekSzama}db)");
                }

                else //normál állapot
                {
                    statusz = "Átlagos vízszint:";
                    Console.WriteLine($"Státusz: {statusz} (Átlag: {atlagSzint:F1} cm)");
                }
                
                hetiAdatok.Add(new NapiMeres
                {
                    Nap = nap,
                    Meresek = aktualisMeresek,
                    Statusz = statusz
                });
                //adatok
            }
            mentes(hetiAdatok);


            Console.WriteLine("\nProgram vége, Gomb nyomásra kiléphet!");
            Console.ReadKey();
        }
        static void FigyelmeztetesKiiro(string uzenet)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Red;
            Console.Write(uzenet);
            //visszaállítjuk a színt
            Console.ResetColor();
            Console.WriteLine("");
        }
    } 
    // Csürke Martin ---



    
}
// Csürke Martin ---
