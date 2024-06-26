﻿using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Login_Project
{
    public class AdminUebersicht
    {
        public static bool Anzeigen = false;

        public static void UserSearch(AdminOverview admin)
        {
            string Benutzersuche = admin.TextBoxBenutzerEingabeSuche.Text;

            User foundUser = SearchUserInDatabase(Benutzersuche);

            if (foundUser != null)
            {
                Anzeigen = false;
                admin.TextBoxNeuerBenutzername.IsEnabled = true;
                admin.TextBoxBenutzerEingabeSuche.IsEnabled = false;
                admin.ButtonBenutzerInfo.IsHitTestVisible = true;
                admin.TextBoxPasswortAdmin.IsEnabled = false;
                admin.ButtonAdminEntfernung.IsHitTestVisible = false;
                admin.ButtonBenutzerSuche.IsHitTestVisible = false;
                admin.TextBoxNeuerBenutzername.Focus();

                if (foundUser.Status == "Administrator" || foundUser.Sicherheitsgruppe == "Administratoren")
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

        public static void DeleteAccount(AdminOverview admin)
        {
            string Benutzer = admin.TextBoxBenutzerEingabeSuche.Text;

            List<User> benutzerToRemove = Register.registerUser.Where(u => u.Username == Benutzer).ToList();

            foreach (User u in benutzerToRemove)
            {
                Register.registerUser.Remove(u);
            }

            admin.ButtonBenutzerInfo.IsHitTestVisible = false;
            admin.ButtonKontoLöschen.IsHitTestVisible = false;
            admin.ButtonAdminErnennung.IsHitTestVisible = false;
            admin.TextBoxNeuerBenutzername.IsEnabled = false;

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

            if (SearchUserInDatabase(newUsername) != null)
            {
                MessageBox.Show("Der neue Benutzername ist bereits vergeben. Bitte wählen Sie einen anderen Benutzernamen.", "Fehler");
                admin.TextBoxNeuerBenutzername.Text = "";
                admin.TextBoxNeuerBenutzername.Focus();
                return;
            }

            User userToModify = SearchUserInDatabase(currentUsername);

            if (userToModify != null)
            {
                userToModify.Username = newUsername;
                UpdateUsernameInDatabase(currentUsername, newUsername);
                MessageBox.Show($"Benutzername wurde erfolgreich von  \"{currentUsername}\"  zu  \"{newUsername}\"  geändert.", "Erfolg");

                admin.TextBoxBenutzerEingabeSuche.Text = newUsername;
            }
        }

        public static void AddAdmin(AdminOverview admin)
        {
            try
            {
                string benutzername = admin.TextBoxBenutzerEingabeSuche.Text;

                if (!string.IsNullOrEmpty(benutzername))
                {
                    AddAdminToDatabase(benutzername);

                    MessageBox.Show($"{benutzername} wurde zu Administrator ernannt.");
                }
                else
                {
                    MessageBox.Show("Bitte geben Sie einen Benutzernamen ein.", "Fehler");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fehler beim Hinzufügen des Admins: {ex.Message}", "Fehler");
            }
        }

        public static void RemoveAdmin(AdminOverview admin)
        {
            try
            {
                string benutzername = admin.TextBoxBenutzerEingabeSuche.Text;

                if (!string.IsNullOrEmpty(benutzername))
                {
                    Console.WriteLine($"RemoveAdmin: {benutzername}");
                    RemoveAdminFromDatabase(benutzername);

                    MessageBox.Show($"{benutzername} wurde von den Administratoren entfernt.");
                }
                else
                {
                    MessageBox.Show("Bitte geben Sie einen Benutzernamen ein.", "Fehler");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fehler beim Entfernen des Admins: {ex.Message}", "Fehler");
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

            User foundUser = SearchUserInDatabase(Benutzer);
            bool benutzerGefunden = foundUser != null;

            admin.labelBenutzerExistiertNicht.Visibility = benutzerGefunden ? Visibility.Collapsed : Visibility.Visible;

            return benutzerGefunden;
        }

        public static void PasswordAdmin(AdminOverview admin)
        {
            string newAdminPassword = admin.TextBoxPasswortAdmin.Password;

            foreach (User adminUser in Register.registerUser.Where(user => user.Status == "Administrator"))
            {
                adminUser.Password = Register.EncryptPassword(newAdminPassword);
            }

            UpdateAdminPasswordInDatabase(newAdminPassword);

            MessageBox.Show("Admin Passwort wurde geändert.");
        }

        public static void PostgreSQLDelete(string username)
        {
            string connString = "Host=localhost;Port=5432;Username=postgres;Password=Syria2003!;Database=XML-Validator-DB;";

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
            string connString = "Host=localhost;Port=5432;Username=postgres;Password=Syria2003!;Database=XML-Validator-DB;";

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

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            Console.WriteLine($"Benutzer {username} wurde erfolgreich zu Administrator ernannt.");

                            User adminUser = Register.registerUser.FirstOrDefault(u => u.Username == username);
                            if (adminUser != null)
                            {
                                adminUser.Status = "Administrator";
                                adminUser.Sicherheitsgruppe = "Administratoren";
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Benutzer {username} konnte nicht zu Administrator ernannt werden. Kein Datensatz gefunden.");
                        }
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
            string connString = "Host=localhost;Port=5432;Username=postgres;Password=Syria2003!;Database=XML-Validator-DB;";

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

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            Console.WriteLine($"Benutzer {username} wurde erfolgreich von den Administratoren entfernt.");

                            User adminUser = Register.registerUser.FirstOrDefault(u => u.Username == username);
                            if (adminUser != null)
                            {
                                adminUser.Status = "Benutzer";
                                adminUser.Sicherheitsgruppe = "Mitarbeiter";
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Benutzer {username} konnte nicht von den Administratoren entfernt werden. Kein Datensatz gefunden.");
                        }
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
            string connString = "Host=localhost;Port=5432;Username=postgres;Password=Syria2003!;Database=XML-Validator-DB;";

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

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            Console.WriteLine($"Benutzer {currentUsername} wurde erfolgreich zu {newUsername} aktualisiert.");
                        }
                        else
                        {
                            Console.WriteLine($"Benutzer {currentUsername} konnte nicht aktualisiert werden. Kein Datensatz gefunden.");
                        }
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
            string connString = "Host=localhost;Port=5432;Username=postgres;Password=Syria2003!;Database=XML-Validator-DB;";

            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(connString))
                {
                    conn.Open();

                    using (NpgsqlCommand cmd = new NpgsqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = "UPDATE user_table SET password = @NewAdminPassword WHERE status = 'Administrator'";
                        cmd.Parameters.AddWithValue("@NewAdminPassword", Register.EncryptPassword(newAdminPassword));

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fehler bei der PostgreSQL-Verbindung oder Datenbankoperation: " + ex.Message, "Fehler");
            }
        }

        public static User SearchUserInDatabase(string username)
        {
            User foundUser = null;

            string connString = "Host=localhost;Port=5432;Username=postgres;Password=Syria2003!;Database=XML-Validator-DB;";

            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(connString))
                {
                    conn.Open();

                    string query = "SELECT * FROM user_table WHERE username = @Username";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Username", username);

                        using (NpgsqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                foundUser = new User();
                                foundUser.Username = reader["username"].ToString();
                                foundUser.Status = reader["status"].ToString();
                                foundUser.Sicherheitsgruppe = reader["sicherheitsgruppe"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fehler bei der PostgreSQL-Verbindung oder Datenbankoperation: " + ex.Message, "Fehler");
            }

            return foundUser;
        }
    }
}