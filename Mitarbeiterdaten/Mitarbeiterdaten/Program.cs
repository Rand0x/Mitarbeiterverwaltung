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
                string nachname = daten[0].Replace(" ", String.Empty);
                string vorname = daten[1].Replace(" ", String.Empty);
                int personalnr = int.Parse(daten[2]); 
                string beruf = daten[3];
                string geschlecht = daten[5].Trim();

                liste.Add(new Mitarbeiter
                {
                    Vorname = vorname,
                    Nachname = nachname,
                    Personalnr = personalnr,
                    Geburtstag = "GETDATE()",
                    Einstellungsdatum = "GETDATE()",
                    Beruf = beruf,
                    Geschlecht = geschlecht,
                    StundenProWoche = 40,
                    Überstunden = 0,
                    Lohn = 3000,
                    Urlaub = 30,
                    Kündigungsfrist = 30,
                    Adresse = -1
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
                var nummer = "0";
                nummer += vorwahl[randomVorwahl.Next(0, vorwahl.Length)];
                nummer += " ";
                var lenght = randomLaenge.Next(7, 9);
                for (int i = 0; i < lenght; i++)
                {
                    nummer += Convert.ToString(randomZiffern.Next(0, 10));
                }
                item.MobileNbr = nummer;
                item.Abteilung = randomZiffern.Next(0, 3);
                item.Steuerklasse = randomZiffern.Next(1, 6);
            }

            //string[] vorwahlFestnetz = { "0911", "09128", "09131", "09123", "09134", "09170", "09126"};
            foreach (Mitarbeiter item in liste)
            {
                //    string nummer = "";
                //    nummer += vorwahlFestnetz[randomVorwahl.Next(0, vorwahlFestnetz.Length)];
                //    nummer += " ";
                //    int lenght = randomLaenge.Next(6,8);
                //    for (int i = 0; i < lenght; i++)
                //    {
                //        nummer += Convert.ToString(randomZiffern.Next(0, 10));
                //    }
                //    item.LandlineNbrPrivate = nummer;

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
