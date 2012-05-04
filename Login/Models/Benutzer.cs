//test

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Login.Models
{
    public class Benutzer
    {
        public string vorname { get; set; }
        public string nachname { get; set; }

        public string email { get; set; }
        public string passwort { get; set; }

        public int matrikelnummer { get; set; }
        public string studiengang { get; set; }
        public string fachsemester { get; set; }

        public string strasse { get; set; }
        public string hausnummer { get; set; }

        public int plz { get; set; }
        public string wohnort { get; set; }
        

        public int rechte { get; set; }
        public bool freischaltung { get; set; }

        public string institut { get; set; }
        public int stellvertreter { get; set; }
    }
}