using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Login_Project
{
    public class BackendLogin
    {
        public static MainWindow wnd;

        public static string csvDatei = "C:\\Users/Fujitsu/Desktop/XML-Validator-Frontend/public/ressources/benutzerdaten.csv";

        public static void TryLogin(Login login, MainWindow main)
        {
            wnd = main;

            string UsernameEmail = login.TextBoxBenutzerEmail.Text;
            string Password = login.TextBoxPasswort.Password;

            string EncryptPassword = BackendRegister.EncryptPassword(Password);

            bool isLoginSuccessful = false;

            User u = new User();

            foreach (User user in BackendRegister.registerUser)
            {
                if ((user.Username == UsernameEmail || user.Email == UsernameEmail) && user.Password == EncryptPassword)
                {
                    u = user;
                    isLoginSuccessful = true;
                }
            }

            if (isLoginSuccessful && u.Status == "Benutzer")
            {
                try
                {
                    string WPF = "C:\\Desktop/Desktop/bin/Debug/Desktop.exe";

                    Process.Start(new ProcessStartInfo(WPF));
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Fehler beim Öffnen der Website: " + ex.Message);
                }

            }
            else if (isLoginSuccessful && u.Status == "Administrator" && u.Sicherheitsgruppe == "Administratoren")
            {
                wnd.content.Content = new Administrator(wnd);
            }
            else
            {
                MessageBox.Show("Falscher Benutzername oder falsches Passwort");
                login.TextBoxPasswort.Clear();
                login.TextBoxPasswort.Focus();
            }
        }

        public static void ReadCSV()
        {
            string csvDatei = "C:\\Users/Fujitsu/Desktop/XML-Validator-Frontend/public/ressources/benutzerdaten.csv";
            string[] line = File.ReadAllLines(csvDatei);

            try
            {
                if (String.IsNullOrEmpty(line[0]))
                {
                    return;
                }
            }
            catch (Exception)
            {
                if (line == null)
                {
                    return;
                }
            }

            while (BackendRegister.registerUser.Count > 0)
            {
                BackendRegister.registerUser.RemoveAt(0);
            }

            foreach (string user in line)
            {
                string[] ar = user.Split(',');
                User u = new User();

                u.Username = ar[0];
                u.Email = ar[1];
                u.Status = ar[2];
                u.Sicherheitsgruppe = ar[3];
                u.Password = ar[4];
                BackendRegister.registerUser.Add(u);
            }
        }
    }
}
