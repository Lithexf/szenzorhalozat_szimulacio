//Fő konzol applikációt: Sallai Martin & Csürke Martin készítette
//dll referencia hozzáadás & bugg fix: Udvardy Balázs
//lite db: Csürke Martin
//bug fixing: Csürke Martin
//fő logika & eseménykezelés: Sallai Martin
//összehangolás: Csürke Martin, Sallai Martin
//dokumentáció: Udvardy Balázs
//team managment: Sallai Martin
//final review: Udvardy Balázs & Csürke Martin & Sallai Martin
//bug fixing 2.0: Sallai Martin

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
        public static event AradasEsemenykezelo OnAradas; //esemenykezeles dekralacio

        static void Main(string[] args)
        {
            Console.WriteLine("Szenzorhálózati Szimuláció");
            Console.WriteLine("Vízszint mérő program");
            Console.CursorVisible = false;
            
            //esemeny kezeles: amikor onaradas bekovetkezik akkor figyelmezteteskiiro futtatas lesz
            OnAradas += FigyelmeztetesKiiro; //esemenykezeles 
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
                    // ?-el ellenőrizzük nem e NULL az esemény, ha nem null akkor elindítjuk az eseményt
                    OnAradas?.Invoke($"Áradás Történt! (Kritikus Értékek: {kritikusErtekekSzama}db!)");
                }
                else if (kritikusErtekekSzama >= 2)  //kis figyelmeztetés display
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
        static void FigyelmeztetesKiiro(string uzenet) //esemeny kezeles utolso resz
        {
            //figyelmeztetes kiiras skin
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Red;
            Console.Write(uzenet);
            //visszaállítjuk a színt
            Console.ResetColor();
            Console.WriteLine("");
        }
        // Csürke Martin ---

        static void SargaKiiro(string uzenet)
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.Write(uzenet);
            //visszaállítjuk a színt
            Console.ResetColor();
            Console.WriteLine("");
        }


        static void mentes(List<NapiMeres> adatok)
        {
            //JSON fájlba mentés
            string jsonString = System.Text.Json.JsonSerializer.Serialize(adatok, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText("meresek.json", jsonString);

            // elmentjük a méréseket szöveges fájlban
            using (StreamWriter sw = new StreamWriter("adatbazis.txt"))
            {
                sw.WriteLine("Nap;SzenzorID;Vizszint;Statusz");
                foreach (var napAdat in adatok)
                {
                    foreach (var meres in napAdat.Meresek)
                    {
                        sw.WriteLine($"{napAdat.Nap};{meres.SzenzorID};{meres.VizszintCm};{napAdat.Statusz}");
                    }
                }
            }

            //elmentjük adatbázisban
            var db = new LiteDatabase("Filename=adatok.db; Connection=shared;");
            var meresek = db.GetCollection<NapiMeres>();
            foreach (var meres in adatok)
            {
                meresek.Insert(meres);
            }
        }

    }

    //get set
    class NapiMeres
    {
        public string Nap { get; set; }
        public List<MeresAdat> Meresek { get; set; }
        public string Statusz { get; set; }
    }
}


// Csürke Martin ---


