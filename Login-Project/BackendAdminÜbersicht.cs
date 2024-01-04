using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Login_Project
{
    public class BackendAdminÜbersicht
    {
        public static bool Anzeigen = false;

        public static void UserSearch(AdminOverview admin)
        {
            string Benutzersuche = admin.TextBoxBenutzerEingabeSuche.Text;

            string Usernames = BackendPasswort.ReadDataCSV(Benutzersuche);

            if (!string.IsNullOrEmpty(Usernames))
            {
                Anzeigen = false;
                admin.TextBoxNeuerBenutzername.IsEnabled = true;
                admin.TextBoxBenutzerEingabeSuche.IsEnabled = false;
                admin.ButtonBenutzerInfo.IsHitTestVisible = true;
                admin.TextBoxPasswortAdmin.IsEnabled = false;
                admin.ButtonAdminEntfernung.IsHitTestVisible = false;
                admin.ButtonBenutzerSuche.IsHitTestVisible = false;
                admin.TextBoxNeuerBenutzername.Focus();

                User u = null;

                foreach (User user in BackendRegister.registerUser)
                {
                    if (Benutzersuche == user.Username)
                    {
                        u = user;
                        break;
                    }
                }

                if (Benutzersuche.ToLower() == "admin" || Benutzersuche.ToLower() == "administrator")
                {
                    admin.ButtonAdminEntfernung.IsHitTestVisible = false;
                    admin.ButtonKontoLöschen.IsHitTestVisible = false;
                    admin.ButtonAdminErnennung.IsHitTestVisible = false;
                    admin.TextBoxPasswortAdmin.IsEnabled = true;
                }
                else if (u != null)
                {
                    if (u.Status == "Administrator" || u.Sicherheitsgruppe == "Administratoren")
                    {
                        admin.ButtonAdminEntfernung.IsHitTestVisible = true;
                        admin.ButtonKontoLöschen.IsHitTestVisible = true;
                        admin.ButtonAdminErnennung.IsHitTestVisible = false;
                        admin.TextBoxPasswortAdmin.IsEnabled = true;
                    }
                    else
                    {
                        admin.ButtonAdminEntfernung.IsHitTestVisible = false;
                        admin.ButtonKontoLöschen.IsHitTestVisible = true;
                        admin.ButtonAdminErnennung.IsHitTestVisible = true;
                    }
                }
            }            
        }

        public static void DeleteAccount(AdminOverview admin)
        {
            string Benutzer = admin.TextBoxBenutzerEingabeSuche.Text;

            List<User> benutzerToRemove = new List<User>();

            foreach (User u in BackendRegister.registerUser)
            {
                if (u.Username == admin.TextBoxBenutzerEingabeSuche.Text)
                {
                    benutzerToRemove.Add(u);
                }
            }

            foreach (User u in benutzerToRemove)
            {
                BackendRegister.registerUser.Remove(u);
            }

            admin.ButtonBenutzerInfo.IsHitTestVisible = false;
            admin.ButtonKontoLöschen.IsHitTestVisible = false;
            admin.ButtonAdminErnennung.IsHitTestVisible = false;
            admin.TextBoxNeuerBenutzername.IsEnabled = false;

            BackendRegister.CSVWrite();

            MessageBox.Show($"Konto \"{Benutzer}\" wurde erfolgreich gelöscht.");
        }

        public static void NewUsername(AdminOverview admin)
        {
            string EingabeBenutzer = admin.TextBoxBenutzerEingabeSuche.Text;
            string NeuerBenutzerName = admin.TextBoxNeuerBenutzername.Text;

            if (string.IsNullOrWhiteSpace(NeuerBenutzerName))
            {
                MessageBox.Show("Bitte geben Sie einen neuen Benutzernamen ein.", "Fehler");
                admin.TextBoxNeuerBenutzername.Text = "";
                admin.TextBoxNeuerBenutzername.Focus();
                return;
            }

            if (NeuerBenutzerName == EingabeBenutzer)
            {
                MessageBox.Show("Der neue Benutzername ist identisch mit dem aktuellen Benutzernamen.", "Fehler");
                admin.TextBoxNeuerBenutzername.Text = "";
                admin.TextBoxNeuerBenutzername.Focus();
                return;
            }

            foreach (User u in BackendRegister.registerUser)
            {
                if (u.Username == NeuerBenutzerName)
                {
                    MessageBox.Show("Der neue Benutzername ist bereits vergeben. Bitte wählen Sie einen anderen Benutzernamen.", "Fehler");
                    admin.TextBoxNeuerBenutzername.Text = "";
                    admin.TextBoxNeuerBenutzername.Focus();
                    return;
                }
            }

            foreach (User u in BackendRegister.registerUser)
            {
                if (u.Username == EingabeBenutzer)
                {
                    u.Username = NeuerBenutzerName;
                    BackendRegister.CSVWrite();
                    MessageBox.Show($"Benutzername wurde erfolgreich von  \"{EingabeBenutzer}\"  zu  \"{NeuerBenutzerName}\"  geändert.", "Erfolg");
                }
            }
        }

        public static void AddAdmin(AdminOverview admin)
        {
            string Benutzer = admin.TextBoxBenutzerEingabeSuche.Text;

            foreach (User u in BackendRegister.registerUser)
            {
                if (u.Username == Benutzer)
                {
                    u.Status = "Administrator";
                    u.Sicherheitsgruppe = "Administratoren";
                }
            }

            admin.ButtonAdminErnennung.IsHitTestVisible = false;
            admin.ButtonKontoLöschen.IsHitTestVisible = false;

            BackendRegister.CSVWrite();

            MessageBox.Show($"{Benutzer} wurde zu Administrator ernannt.");
        }

        public static void RemoveAdmin(AdminOverview admin)
        {
            string Benutzer = admin.TextBoxBenutzerEingabeSuche.Text;

            foreach (User u in BackendRegister.registerUser)
            {
                if (u.Username == Benutzer)
                {
                    u.Status = "Benutzer";
                    u.Sicherheitsgruppe = "Mitarbeiter";
                }
                else if (Benutzer.ToLower() == "admin" || Benutzer.ToLower() == "administrator")
                {
                    admin.ButtonAdminEntfernung.IsHitTestVisible = true;
                    admin.ButtonKontoLöschen.IsHitTestVisible = false;
                    admin.ButtonAdminErnennung.IsHitTestVisible = false;
                }
            }

            admin.ButtonAdminEntfernung.IsHitTestVisible = false; 
            admin.ButtonKontoLöschen.IsHitTestVisible = true;
            admin.ButtonAdminErnennung.IsHitTestVisible = true;
            admin.TextBoxPasswortAdmin.IsEnabled = false;

            BackendRegister.CSVWrite();

            MessageBox.Show($"{Benutzer} wurde zu von den Administratoren entfernt.");
        }

        public static bool UserCondition(AdminOverview admin)
        {
            string Benutzer = admin.TextBoxBenutzerEingabeSuche.Text;

            if (string.IsNullOrEmpty(Benutzer))
            {
                admin.labelBenutzerExistiertNicht.Visibility = Visibility.Collapsed;
                return false;
            }

            bool benutzerGefunden = false;

            foreach (User user in BackendRegister.registerUser)
            {
                if (user.Username == Benutzer)
                {
                    benutzerGefunden = true;
                    break;
                }
            }

            if (benutzerGefunden)
            {
                admin.labelBenutzerExistiertNicht.Visibility = Visibility.Collapsed;
            }
            else
            {
                admin.labelBenutzerExistiertNicht.Visibility = Visibility.Visible;
            }

            return benutzerGefunden;
        }

        public static void PasswordAdmin(AdminOverview admin)
        {
            string neuesPasswortAdmin = admin.TextBoxPasswortAdmin.Password;

            foreach (User u in BackendRegister.registerUser)
            {
                if (u.Status == "Administrator")
                {
                    u.Password = BackendRegister.EncryptPassword(neuesPasswortAdmin);
                    BackendRegister.CSVWrite();
                }
            }

            MessageBox.Show("Admin Passwort wurde geändert.");
        }
    }
}
