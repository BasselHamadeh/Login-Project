using Npgsql;
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

            List<User> benutzerToRemove = BackendRegister.registerUser.Where(u => u.Username == Benutzer).ToList();

            foreach (User u in benutzerToRemove)
            {
                BackendRegister.registerUser.Remove(u);
            }

            admin.ButtonBenutzerInfo.IsHitTestVisible = false;
            admin.ButtonKontoLöschen.IsHitTestVisible = false;
            admin.ButtonAdminErnennung.IsHitTestVisible = false;
            admin.TextBoxNeuerBenutzername.IsEnabled = false;

            BackendRegister.CSVWrite();
            PostgreSQLDelete(Benutzer);

            MessageBox.Show($"Konto \"{Benutzer}\" wurde erfolgreich gelöscht.");
        }

        public static void NewUsername(AdminOverview admin)
        {
            string currentUsername = admin.TextBoxBenutzerEingabeSuche.Text;
            string newUsername = admin.TextBoxNeuerBenutzername.Text.Trim();

            if (string.IsNullOrWhiteSpace(newUsername))
            {
                MessageBox.Show("Bitte geben Sie einen neuen Benutzernamen ein.", "Fehler");
                admin.TextBoxNeuerBenutzername.Text = "";
                admin.TextBoxNeuerBenutzername.Focus();
                return;
            }

            if (newUsername == currentUsername)
            {
                MessageBox.Show("Der neue Benutzername ist identisch mit dem aktuellen Benutzernamen.", "Fehler");
                admin.TextBoxNeuerBenutzername.Text = "";
                admin.TextBoxNeuerBenutzername.Focus();
                return;
            }

            if (BackendRegister.registerUser.Any(u => u.Username == newUsername))
            {
                MessageBox.Show("Der neue Benutzername ist bereits vergeben. Bitte wählen Sie einen anderen Benutzernamen.", "Fehler");
                admin.TextBoxNeuerBenutzername.Text = "";
                admin.TextBoxNeuerBenutzername.Focus();
                return;
            }

            User userToModify = BackendRegister.registerUser.FirstOrDefault(u => u.Username == currentUsername);
            if (userToModify != null)
            {
                userToModify.Username = newUsername;
                UpdateUsernameInDatabase(currentUsername, newUsername);
                BackendRegister.CSVWrite();
                MessageBox.Show($"Benutzername wurde erfolgreich von  \"{currentUsername}\"  zu  \"{newUsername}\"  geändert.", "Erfolg");

                admin.TextBoxBenutzerEingabeSuche.Text = newUsername;
            }
            else
            {
                MessageBox.Show($"Benutzer \"{currentUsername}\" nicht gefunden.", "Fehler");
            }
        }

        public static void AddAdmin(AdminOverview admin)
        {
            string benutzername = admin.TextBoxBenutzerEingabeSuche.Text;

            if (!string.IsNullOrEmpty(benutzername))
            {
                AddAdminToDatabase(benutzername);
                BackendRegister.CSVWrite();

                MessageBox.Show($"{benutzername} wurde zu Administrator ernannt.");
            }
            else
            {
                MessageBox.Show("Bitte geben Sie einen Benutzernamen ein.", "Fehler");
            }
        }

        public static void RemoveAdmin(AdminOverview admin)
        {
            string benutzername = admin.TextBoxBenutzerEingabeSuche.Text;

            if (!string.IsNullOrEmpty(benutzername))
            {
                RemoveAdminFromDatabase(benutzername);
                BackendRegister.CSVWrite();

                MessageBox.Show($"{benutzername} wurde von den Administratoren entfernt.");
            }
            else
            {
                MessageBox.Show("Bitte geben Sie einen Benutzernamen ein.", "Fehler");
            }
        }

        public static bool UserCondition(AdminOverview admin)
        {
            string Benutzer = admin.TextBoxBenutzerEingabeSuche.Text;

            if (string.IsNullOrEmpty(Benutzer))
            {
                admin.labelBenutzerExistiertNicht.Visibility = Visibility.Collapsed;
                return false;
            }

            bool benutzerGefunden = BackendRegister.registerUser.Any(user => user.Username == Benutzer);

            admin.labelBenutzerExistiertNicht.Visibility = benutzerGefunden ? Visibility.Collapsed : Visibility.Visible;

            return benutzerGefunden;
        }

        public static void PasswordAdmin(AdminOverview admin)
        {
            string newAdminPassword = admin.TextBoxPasswortAdmin.Password;

            foreach (User adminUser in BackendRegister.registerUser.Where(user => user.Status == "Administrator"))
            {
                adminUser.Password = BackendRegister.EncryptPassword(newAdminPassword);
            }

            UpdateAdminPasswordInDatabase(newAdminPassword);
            BackendRegister.CSVWrite();

            MessageBox.Show("Admin Passwort wurde geändert.");
        }

        public static void PostgreSQLDelete(string username)
        {
            string connString = "Host=localhost;Port=5432;Username=postgres;Password=Syria2003!;Database=users;";

            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(connString))
                {
                    conn.Open();

                    using (NpgsqlCommand cmd = new NpgsqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = "DELETE FROM user_table WHERE username = @Username";
                        cmd.Parameters.AddWithValue("@Username", username);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fehler bei der PostgreSQL-Verbindung oder Datenbankoperation: " + ex.Message, "Fehler");
            }
        }

        private static void AddAdminToDatabase(string username)
        {
            string connString = "Host=localhost;Port=5432;Username=postgres;Password=Syria2003!;Database=users;";

            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(connString))
                {
                    conn.Open();

                    using (NpgsqlCommand cmd = new NpgsqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = "UPDATE user_table SET status = 'Administrator', sicherheitsgruppe = 'Administratoren' WHERE username = @Username";
                        cmd.Parameters.AddWithValue("@Username", username);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fehler bei der PostgreSQL-Verbindung oder Datenbankoperation: " + ex.Message, "Fehler");
            }
        }

        private static void RemoveAdminFromDatabase(string username)
        {
            string connString = "Host=localhost;Port=5432;Username=postgres;Password=Syria2003!;Database=users;";

            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(connString))
                {
                    conn.Open();

                    using (NpgsqlCommand cmd = new NpgsqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = "UPDATE user_table SET status = 'Benutzer', sicherheitsgruppe = 'Mitarbeiter' WHERE username = @Username";
                        cmd.Parameters.AddWithValue("@Username", username);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fehler bei der PostgreSQL-Verbindung oder Datenbankoperation: " + ex.Message, "Fehler");
            }
        }

        private static void UpdateUsernameInDatabase(string currentUsername, string newUsername)
        {
            string connString = "Host=localhost;Port=5432;Username=postgres;Password=Syria2003!;Database=users;";

            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(connString))
                {
                    conn.Open();

                    using (NpgsqlCommand cmd = new NpgsqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = "UPDATE user_table SET username = @NewUsername WHERE username = @CurrentUsername";
                        cmd.Parameters.AddWithValue("@NewUsername", newUsername);
                        cmd.Parameters.AddWithValue("@CurrentUsername", currentUsername);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fehler bei der PostgreSQL-Verbindung oder Datenbankoperation: " + ex.Message, "Fehler");
            }
        }

        private static void UpdateAdminPasswordInDatabase(string newAdminPassword)
        {
            string connString = "Host=localhost;Port=5432;Username=postgres;Password=Syria2003!;Database=users;";

            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(connString))
                {
                    conn.Open();

                    using (NpgsqlCommand cmd = new NpgsqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = "UPDATE user_table SET password = @NewAdminPassword WHERE status = 'Administrator'";
                        cmd.Parameters.AddWithValue("@NewAdminPassword", BackendRegister.EncryptPassword(newAdminPassword));

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fehler bei der PostgreSQL-Verbindung oder Datenbankoperation: " + ex.Message, "Fehler");
            }
        }

    }
}