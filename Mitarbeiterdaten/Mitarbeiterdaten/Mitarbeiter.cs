using System;
using System.Collections.Generic;
using System.Text;

namespace Mitarbeiterdaten
{
    class Mitarbeiter
    {
        public int Personalnr { get; set; }
        public string Vorname { get; set; }
        public string Nachname { get; set; }
        public string Geburtstag { get; set; }
        public string Geschlecht { get; set; }
        public string EMail { get; set; }
        public string MobileNbr { get; set; }
        public int Abteilung { get; set; }
        public string Beruf { get; set; }
        public string Einstellungsdatum { get; set; }
        public int StundenProWoche { get; set; }
        public int Überstunden { get; set; }
        public double Lohn { get; set; }
        public int Urlaub { get; set; }
        public int Kündigungsfrist { get; set; }
        public int Adresse { get; set; }
        public int Bank { get; set; }
        public int Steuerklasse { get; set; }

        //public string Familienstand { get; set; }
        //public string Manager { get; set; }
        //public int ManagerID { get; set; }
        //public string MobileNbrPrivate { get; set; }
        //public string LandlineNbr { get; set; }
        //public string LandlineNbrPrivate { get; set; }


        public override string ToString()
        {
            return ($", ({Personalnr}, N'{Vorname}', N'{Nachname}', {Geburtstag}, N'{Geschlecht}', N'{EMail}', N'{MobileNbr}', {Abteilung}, N'{Beruf}', {Einstellungsdatum}, {StundenProWoche}, {Überstunden}, {Lohn}, {Urlaub}, {Kündigungsfrist}, {Adresse}, {Steuerklasse}, -1)");

            //return ($"items.Add(new User() {{ FirstName = \"{Vorname}\", LastName = \"{Nachname}\", PersNr = {Personalnr}, EMail = \"{EMail}\"," +
            //    $" MobileNbr = \"{MobileNbr}\", MobileNbrPrivate = \"{MobileNbrPrivate}\", LandlineNbr = \"{LandlineNbr}\", LandlineNbrPrivate = \"{LandlineNbrPrivate}\", " +
            //    $" Job = \"{Beruf}\",Birthday = \"{Geburtstag}\",Sex = \"{Geschlecht}\",MaritalDesc = \"{Familienstand}\"," +
            //    $" HireDate = \"{Einstellungsdatum}\", Department = \"{Abteilung}\",Manager = \"{Manager}\"}});");
        }

    }  
}
