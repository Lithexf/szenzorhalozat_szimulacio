using System;
using System.Collections.Generic;

namespace MeresKonyvtar
{

    public class MeresAdat
    {
        public int SzenzorID { get; set; }
        public int VizszintCm { get; set; }
    }

    public class SzenzorMeresGeneralas
    {
        private Random rnd = new Random();

        // Generálunk 5 darab mérési adatot és visszatérünk egy listával
        public List<MeresAdat> Meresgeneralas(bool GarantaltAradas = false)
        {
            List<MeresAdat> meresek = new List<MeresAdat>();

            for (int i = 1; i <= 5; i++)
            {
                int vizszint;

                // Ha garantált áradást kérünk, akkor 400 feletti értékeket generálunk különben 0-tól 500-ig
                if (GarantaltAradas)
                {
                    vizszint = rnd.Next(401, 501);
                }
                else
                {
                    vizszint = rnd.Next(0, 501);
                }
                meresek.Add(new MeresAdat
                {
                    SzenzorID = i,
                    VizszintCm = vizszint
                });
            }
            // Visszatérünk a generált mérési adatok listájával
            return meresek;
        }
    }
}