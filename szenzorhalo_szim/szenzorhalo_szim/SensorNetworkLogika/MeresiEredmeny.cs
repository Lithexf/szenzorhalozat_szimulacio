using System;
using System.Collections.Generic;

namespace SensorNetworkLogika
{
    // Publikus osztály, hogy más projektek (a DLL-t használók) is elérjék.
    public class MeasurementReading
    {
        // Automatikus property: A mérés időbélyege.
        public DateTime Timestamp { get; set; }

        // Automatikus property: Az 5 szenzor által mért értékek listája.
        public List<int> SensorValues { get; set; }

        // Automatikus property: Ha valamilyen esemény kiváltódott (pl. "Áradás"),
        // annak a nevét tárolja. Lehet null (?), ha épp nem történt semmi.
        public string? EventTriggered { get; set; }

        // Konstruktor az objektum könnyű létrehozásához.
        public MeasurementReading(List<int> values)
        {
            this.Timestamp = DateTime.Now; // Beállítjuk az aktuális időt
            this.SensorValues = values;
            this.EventTriggered = null; // Alapértelmezetten nincs esemény
        }
    }
}
