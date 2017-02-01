﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adressbuch
{
    // Diese Klasse stellt das Datenmodell für das Adressbuch
    // und notwendige Methoden für die Datenverarbeitung
    // zur Verfügung
    class Model
    {
        // Objektvariable für Zugriff auf Liste
        private List<Person> personen;


        public Model()
        {
            // Leere Liste erstellen
            personen = new List<Person>();

            // Datensätze aus adressbuch.txt lesen,
            // Person-Objekte erstellen und
            // der Liste hinzufügen

            leseAdressbuchDatei();
        }

        public List<Person> suchePersonen(string wert)
        {
            // leere Ergebnisliste erstellen
            List<Person> ergebnis = new List<Person>();

            foreach (Person p in personen)
            {
                if (p.Vorname.Contains(wert) ||
                    p.Name.Contains(wert) ||
                    p.Plz.Contains(wert)
                   )
                {
                    Person newPerson = new Person(p.Vorname,
                                                  p.Name,
                                                  p.Plz,
                                                  p.Geburtstag
                                                 );
                    ergebnis.Add(newPerson);
                }
            }

            return ergebnis;

        }

        // Liest die Datei adressbuch.txt und erstellt Person-Objekte
        private bool leseAdressbuchDatei()
        {
            // Hiermit könnte Erfolg oder Misserfolg der
            // Methode zurückgemeldet werden
            // Besser wäre, bei Misserfolg eine Ausnahme zu werfen
            bool rc = true;

            // automatische Freigabe der Ressource mittels using
            using (StreamReader sr = new StreamReader(@"adressbuch.txt"))
            {
                string zeile;
                // Lesen bis Dateiende, Zeile für Zeile
                while ( ( zeile = sr.ReadLine() ) != null )
                {
                    // Person-Objekt erstellen anhand gelesener Zeile
                    Person p = convertString2Person(zeile);

                    // Person-Objekt in die Liste einfügen
                    personen.Add(p);
                }
            }

            return rc;
        }

        private Person convertString2Person(string _p)
        {
            char[] separator = { ';' };
            string[] daten = _p.Split(separator);

            // Geburtsdatum umformen, um ein DateTime-Objekt
            // zu erstellen
            char[] trenner = { '.' };
            string[] geburtsdatum = daten[3].Split(trenner);

            int tag = Convert.ToInt32(geburtsdatum[0]);
            int monat = Convert.ToInt32(geburtsdatum[1]);
            int jahr = Convert.ToInt32(geburtsdatum[2]);

            DateTime datum = new DateTime(jahr, monat, tag);

            // Person-Objekt erstellen und der Liste hinzufügen
            Person p = new Person(daten[0], daten[1], daten[2], datum);

            return p;
        }

        private string convertPerson2String(Person _p)
        {
            string person="";

            // Hier wird ein Person-Objekt in den String umgeformt

            return person;
        }


        // Schreibt die Person-Objekte in die Datei adressbuch.txt
        private bool schreibeAdressbuchDatei()
        {
            bool rc = false;

            return rc;
        }

        // Dient nur zu Testzwecken
        // Zeigt das Adressbuch auf der Server-Konsole
        private void zeigeAdressbuch()
        {
            foreach (Person p in personen)
            {
                Console.WriteLine(p.Vorname + " : " + p.Name + " : " + p.Plz + " : " + p.Geburtstag.ToShortDateString());
            }
        }
    }
}
