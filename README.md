# szenzorhalozat_szimulacio @ C# .NET Framework
Félévi beadandó feladat - Szenzorhálózat szimulációja @ C# .NET Framework


Szenzorhálózati Szimuláció.
„Vizszint mérő program” 
-	Vizszint 0 és 500 cm között lehet egész szám 
-	5 db mérési ponton van mérés
-	események: 1 esemény: „Áradás” 5 ből 3 mérő pont 400 cm feletti értéket vesz fel
-	mérési pontok által felvett értékeket Random függvénnyel határozzuk meg

használni kell:
- delegáltat
- osztályokat
- mérések kiirása egy adatbázis fileban (gondolom TXTbe irni)
- mérési adatok kiirása json ba
- DLL használat
- LINQ függvények 3 db különböző legyen benne. 

az alábbi módon működjön a logika:
- 5 db random szám generálása 0 és 500 között
- ha 3 db 400 feletti akkor : Áradás esemény lép életbe (esemény kezelés)
- ha nem akkor: átlagos viz szint
- 5 db mérést szimuálunk, a hét minden napjára, hétfőtől péntekig
- az 5db mérést, számmal, azonositoval, és mért mennyiséggel együtt JSON-be tároljuk el 
