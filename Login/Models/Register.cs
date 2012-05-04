using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using DataAnnotationsExtensions;

namespace Login.Models
{
    public class Register
    {
        [Required]
        public string vorname { get; set; }

        [Required]
        public string nachname { get; set; }

        [Required]
        [Email]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }

        [Required]
        public string studiengang { get; set; }

        [Required]
        public string fachsemester { get; set; }

        [Required]
        public string strasse { get; set; }

        [Required]
        public string hausnummer { get; set; }

        [Required]
        public string wohnort { get; set; }

        [Required]
        public int plz { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string passwort { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Passwort")]
        public string confirmPasswort { get; set; }

        public int rechte { get; set; }

        public bool freischaltung { get; set; }

        [Required]
        public int matrikelnummer { get; set; }

        [Required]
        public string institut { get; set; }

        [Required]
        public int stellvertreter { get; set; }
    }
}