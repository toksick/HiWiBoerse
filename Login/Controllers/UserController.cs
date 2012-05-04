using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Login.Models;
using System.Web.Security;
using System.Data.SqlClient;

namespace Login.Controllers
{
    public class UserController : Controller
    {
        Benutzer user;
        DBManager DB = DBManager.getInstanz();


        /**
         * Default View /User
         */ 
        public ActionResult Index()
        {
            return View();
        }


        /**
         * öffnet Registrierung View
         */
        public ActionResult Register()
        {
            var model = new Register();
            return View(model);
        }


        /** 
         * fügt einen Benutzer der Datenbank hinzu und startet eine Session
         * öffne Default Startseite
         * kann nur von eingeloggten Benutzern aufgerufen werden
         */
        [HttpPost]
        public ActionResult Register(Register model)
        {
            string passwort = FormsAuthentication.HashPasswordForStoringInConfigFile(model.passwort, "SHA1");
            DB.aendern("INSERT INTO " +
                            "Benutzer " +
                                "( vorname, nachname, email, studiengang, fachsemester, strasse, hausnummer, wohnort, plz, passwort, rechte, freischaltung, matrikelnummer, institut, stellvertreter) " +
                            "VALUES " +
                                "(" +
                                    "'" + model.vorname + "', '" + model.nachname + "', '" + model.email + "', '" + model.studiengang + "', " + model.fachsemester + ", '" + model.strasse + "', '" + model.hausnummer + "', '" + model.wohnort + "', " +
                                    model.plz + ", '" + passwort + "', 0, 1, " + model.matrikelnummer + ", '" + model.institut + "', 12)");

            return RedirectToAction("Index");

        }


        /**
         * Konto Daten des Bentuzers aus Datenbank lesen und diese an die Kontoview übergeben
         */
        [Authorize]
        public ActionResult Konto()
        {
            this.user = new Benutzer();
            user.email = HttpContext.User.Identity.Name;

            string query = "SELECT vorname, nachname, strasse, hausnummer, plz, wohnort, matrikelnummer, studiengang, fachsemester FROM Benutzer WHERE email='" + user.email + "'";
            SqlDataReader reader = DB.auslesen(query);
            reader.Read();
            user.vorname = reader.GetValue(0).ToString();
            user.nachname = reader.GetValue(1).ToString();
            user.strasse = reader.GetValue(2).ToString();
            user.hausnummer = reader.GetValue(3).ToString();
            user.plz = reader.GetInt32(4);
            user.wohnort = reader.GetValue(5).ToString();
            user.matrikelnummer = reader.GetInt32(6);
            user.studiengang = reader.GetValue(7).ToString();
            user.fachsemester = reader.GetValue(8).ToString();

            reader.Close();

            return View(user);
        }


        //Konto EditFormular anzeigen
        //Pre: User muss eingeloggt sein
        [Authorize]
        public ActionResult KontoEdit()
        {
            this.user = new Benutzer();
            user.email = HttpContext.User.Identity.Name;

            string query = "SELECT vorname, nachname, strasse, hausnummer, plz, wohnort, matrikelnummer, studiengang, fachsemester FROM Benutzer WHERE email='" + user.email + "'";
            SqlDataReader reader = DB.auslesen(query);
            reader.Read();
            user.vorname = reader.GetValue(0).ToString();
            user.nachname = reader.GetValue(1).ToString();
            user.strasse = reader.GetValue(2).ToString();
            user.hausnummer = reader.GetValue(3).ToString();
            user.plz = reader.GetInt32(4);
            user.wohnort = reader.GetValue(5).ToString();
            user.matrikelnummer = reader.GetInt32(6);
            user.studiengang = reader.GetValue(7).ToString();
            user.fachsemester = reader.GetValue(8).ToString();

            reader.Close();

            return View(user);
        }

        //Bearbeite das UserKonto
        [HttpPost]
        [Authorize]
        public ActionResult KontoEdit(Benutzer user)
        {
            user.email = HttpContext.User.Identity.Name;

            string query = "UPDATE Benutzer SET " +
                                "vorname='" + user.vorname + "', " +
                                "nachname='" + user.nachname + "', " +
                                "strasse='" + user.strasse + "', " +
                                "hausnummer='" + user.hausnummer + "', " +
                                "plz='" + user.plz + "', " +
                                "wohnort='" + user.wohnort + "', " +
                                "matrikelnummer='" + user.matrikelnummer + "', " +
                                "studiengang='" + user.studiengang + "', " +
                                "fachsemester='" + user.fachsemester + "' " +
                            "WHERE email='" + user.email + "'";

            DB.aendern(query);

            return RedirectToAction("Konto");
        }

        //Login Methode 
        [HttpPost]
        public ActionResult Login(Login.Models.Login user)
        {
            string password = FormsAuthentication.HashPasswordForStoringInConfigFile(user.Passwort, "SHA1");

            

            if (ModelState.IsValid) //Model Valedierung ist korrekt (Email Format + Passwort)
            {
                string query = "SELECT passwort FROM Benutzer WHERE email='" + user.Email + "'";
                SqlDataReader reader = DB.auslesen(query);
                reader.Read();
                string pw = reader.GetValue(0).ToString();

                if (password == pw)
                {
                    FormsAuthentication.SetAuthCookie(user.Email, false); //Auth-Cookie wird gesetzt, ab jetzt ist man Eingeloggt: False bedeutet: Wenn der Browser geschlossen wird so existiert das cookie auch nicht mehr

                    reader.Close();
                    return RedirectToAction("index", "User");
                }
                else
                { // falsches passwort

                }
                reader.Close();
            }
            else
            {
                ModelState.AddModelError("", "Falsche eingabe");
            }
            return View("index");
        }


        //Logout Methode
        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();//Auth-Cookie wird gelöscht
            return RedirectToAction("Index", "User");
        }


        /**
         * speichert den übergebenen Benutzer in der Datenbank
         */
        private bool SaveUserToDB(Register user)
        {
            string query = "INSERT INTO " +
                                "Benutzer " +
                                    "(" +
                                        "vorname, " +
                                        "nachname, " +
                                        "email, " +
                                        "studiengang, " +
                                        "fachsemester, " +
                                        "strasse, " +
                                        "hausnummer, " +
                                        "wohnort, " +
                                        "plz, " +
                                        "passwort, " +
                                        "rechte, " +
                                        "freischaltung, " +
                                        "matrikelnummer, " +
                                        "institut, " +
                                        "stellvertreter" +
                                    ") " +
                                "VALUES " +
                                    "(" +
                                        "'" + user.vorname + "', " +
                                        "'" + user.nachname + "', " +
                                        "'" + user.email + "', " +
                                        "'" + user.studiengang + "', " +
                                        user.fachsemester + ", " +
                                        "'" + user.strasse + "', " +
                                        "'" + user.hausnummer + "', " +
                                        "'" + user.wohnort + "', " +
                                        user.plz + ", " +
                                        "'" + user.passwort + "', " +
                                        user.rechte + ", " +
                                        user.freischaltung + ", " +
                                        user.matrikelnummer + ", " +
                                        "'" + user.institut + "', " +
                                        user.stellvertreter +
                                    ")";
            
            DB.aendern(query);
            return true;
        }

        private bool GetUserByEmail(string email)
        {
            return true;
        }
    }
}
