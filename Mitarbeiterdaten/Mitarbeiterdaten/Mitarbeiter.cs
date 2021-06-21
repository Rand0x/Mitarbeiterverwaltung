using System;
using System.Collections.Generic;
using System.Text;

namespace Mitarbeiterdaten
{
    class Mitarbeiter
    {
        public string Vorname { get; set; }
        public string Nachname { get; set; }
        public string EMail { get; set; }
        public int Personalnr { get; set; }
        public string Beruf { get; set; }
        public string Geburtstag { get; set; }
        public string Geschlecht { get; set; }
        public string Familienstand { get; set; }
        public string Einstellungsdatum { get; set; }
        public string Abteilung { get; set; }
        public string Manager { get; set; }
        public int ManagerID { get; set; }
        public string MobileNbr { get; set; }
        public string MobileNbrPrivate { get; set; }
        public string LandlineNbr { get; set; }
        public string LandlineNbrPrivate { get; set; }


        public override string ToString()
        {
            return ($"items.Add(new User() {{ FirstName = \"{Vorname}\", LastName = \"{Nachname}\", PersNr = {Personalnr}, EMail = \"{EMail}\"," +
                $" MobileNbr = \"{MobileNbr}\", MobileNbrPrivate = \"{MobileNbrPrivate}\", LandlineNbr = \"{LandlineNbr}\", LandlineNbrPrivate = \"{LandlineNbrPrivate}\", " +
                $" Job = \"{Beruf}\",Birthday = \"{Geburtstag}\",Sex = \"{Geschlecht}\",MaritalDesc = \"{Familienstand}\"," +
                $" HireDate = \"{Einstellungsdatum}\", Department = \"{Abteilung}\",Manager = \"{Manager}\"}});");
        }

    }  
}
