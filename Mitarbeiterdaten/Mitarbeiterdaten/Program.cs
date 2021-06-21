using System;
using System.Collections.Generic;
using System.IO;

namespace Mitarbeiterdaten
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Mitarbeiter> liste;
            liste = new List<Mitarbeiter>();

            #region Auslesen und Bearbeiten der Daten aus Tabelle
            string[] zeilen = File.ReadAllLines("..\\..\\..\\HRDataset.csv");
            foreach (string zeile in zeilen)
            {
                string[] daten = zeile.Split(';');                
                string nachname = daten[0];
                nachname = nachname.Replace(" ", String.Empty);
                string vorname = daten[1];
                vorname = vorname.Replace(" ", String.Empty);
                int personalnr = int.Parse(daten[2]); 
                string beruf = daten[3];
                string geburtstag = daten[4];
                string geschlecht;
                if (daten[5].Contains("M"))
                {
                    geschlecht = "männlich";
                }
                else if (daten[5].Contains("F"))
                {
                    geschlecht = "weiblich";
                }
                else
                {
                    geschlecht = "divers";
                }
                string familienstand;
                if (daten[6].Contains("Married"))
                {
                    familienstand = "verheiratet";
                }
                else if (daten[6].Contains("Single"))
                {
                    familienstand = "ledig";
                }
                else if (daten[6].Contains("Widowed"))
                {
                    familienstand = "verwitwet";
                }
                else
                {
                    familienstand = "geschieden";
                }

                string einstelldatum = daten[7];
                string abteilung = daten[8];
                abteilung = abteilung.Replace(" ", String.Empty);
                abteilung = abteilung.Replace("\t", String.Empty);
                string manager = daten[9];
                int managerId = -1;
                if (daten[10] != "")
                {
                    managerId = int.Parse(daten[10]);
                }
                geburtstag = NormalesDatum(geburtstag);
                einstelldatum = NormalesDatum(einstelldatum);

                liste.Add(new Mitarbeiter
                {
                    Vorname = vorname,
                    Nachname = nachname,
                    Personalnr = personalnr,
                    Beruf = beruf,
                    Geburtstag = geburtstag,
                    Geschlecht = geschlecht,
                    Familienstand = familienstand,
                    Einstellungsdatum = einstelldatum,
                    Abteilung = abteilung,
                    Manager = manager,
                    ManagerID = managerId
                });
            }
            #endregion


            #region Erstellen der Mobilfunknummern - geschäftlich und privat

            string[] vorwahl = { "171", "170", "160", "175", "151", "172", "152", "159", "177", "157" };
            Random randomLaenge = new Random();
            Random randomZiffern = new Random();
            Random randomVorwahl = new Random();
            foreach (Mitarbeiter item in liste)
            {
                string nummer = "0";
                nummer += vorwahl[randomVorwahl.Next(0, vorwahl.Length)];
                nummer += " ";
                int lenght = randomLaenge.Next(7, 9);
                for (int i = 0; i < lenght; i++)
                {
                    nummer += Convert.ToString(randomZiffern.Next(0,10));
                }
                item.MobileNbrPrivate = nummer;

                nummer = "0";
                nummer += vorwahl[randomVorwahl.Next(0, vorwahl.Length)];
                nummer += " ";
                lenght = randomLaenge.Next(7, 9);
                for (int i = 0; i < lenght; i++)
                {
                    nummer += Convert.ToString(randomZiffern.Next(0, 10));
                }
                item.MobileNbr = nummer;
            }

            string[] vorwahlFestnetz = { "0911", "09128", "09131", "09123", "09134", "09170", "09126"};
            foreach (Mitarbeiter item in liste)
            {
                string nummer = "";
                nummer += vorwahlFestnetz[randomVorwahl.Next(0, vorwahlFestnetz.Length)];
                nummer += " ";
                int lenght = randomLaenge.Next(6,8);
                for (int i = 0; i < lenght; i++)
                {
                    nummer += Convert.ToString(randomZiffern.Next(0, 10));
                }
                item.LandlineNbrPrivate = nummer;

                string nummerPrivat = "0911";
                nummerPrivat += " 98-";
                lenght = 4;
                for (int i = 0; i < lenght; i++)
                {
                    nummerPrivat += Convert.ToString(randomZiffern.Next(0, 10));
                }
                
                item.LandlineNbr = nummerPrivat;

                item.EMail = item.Vorname.ToLower() + "." + item.Nachname.ToLower() + "@mav.de";
            }

            #endregion

            #region Schreiben der formatierten Strings in Textdatei

            string fileName = "..\\..\\..\\Output-Datei.txt";
            FileStream s = new FileStream(fileName, FileMode.Create);
            StreamWriter w = new StreamWriter(s);
            foreach (Mitarbeiter item in liste)
            {
                w.WriteLine(item);
            }            
            w.Close();

            #endregion
        }

        /// <summary>
        /// Normalisieren des Datums auf europäisches Format
        /// </summary>
        /// <param name="zuvor"></param>
        /// <returns></returns>
        public static string NormalesDatum(string zuvor)
        {
            string nachher;
            string[] geteilt = zuvor.Split('/');

            if (geteilt.Length == 1)
            {
                return zuvor;
            }

            nachher = geteilt[1] + "." + geteilt[0] + ".";
            if (Convert.ToInt32(geteilt[2]) < 21)
                nachher += ("20" + geteilt[2]);            
            else if (Convert.ToInt32(geteilt[2]) < 100 )
                nachher += ("19" + geteilt[2]);
            else
                nachher += geteilt[2];

            return nachher;
        }
    }
}
