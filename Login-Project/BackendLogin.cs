using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Npgsql;

namespace Login_Project
{
    public class BackendLogin
    {
        public static MainWindow wnd;

        public static void TryLogin(Login login, MainWindow main)
        {
            wnd = main;

            string UsernameEmail = login.TextBoxBenutzerEmail.Text;
            string Password = login.TextBoxPasswort.Password;

            string EncryptPassword = BackendRegister.EncryptPassword(Password);

            bool isLoginSuccessful = false;

            User u = new User();

            u = FetchUserFromDatabase(UsernameEmail, EncryptPassword);

            if (u != null)
            {
                isLoginSuccessful = true;
                UpdateUserInDatabase(u);
            }

            if (isLoginSuccessful && u.Status == "Benutzer")
            {
                try
                {
                    Process.Start(new ProcessStartInfo("http://localhost:3000"));
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Fehler beim Öffnen des Browsers: " + ex.Message);
                }
            }
            else if (isLoginSuccessful && u.Status == "Administrator" && u.Sicherheitsgruppe == "Administratoren")
            {
                try
                {
                    Process.Start(new ProcessStartInfo("http://localhost:3000"));

                    wnd.content.Content = new AdminOverview(wnd);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Fehler beim Öffnen des Browsers: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Falscher Benutzername oder falsches Passwort");
                login.TextBoxPasswort.Clear();
                login.TextBoxPasswort.Focus();
            }
        }

        public static User FetchUserFromDatabase(string usernameEmail, string encryptedPassword)
        {
            User user = null;

            string connString = "Host=localhost;Port=5432;Username=postgres;Password=Syria2003!;Database=user-database;";

            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(connString))
                {
                    conn.Open();

                    using (NpgsqlCommand cmd = new NpgsqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = "SELECT * FROM user_table WHERE (username = @UsernameEmail OR email = @UsernameEmail) AND password = @EncryptedPassword";
                        cmd.Parameters.AddWithValue("@UsernameEmail", usernameEmail);
                        cmd.Parameters.AddWithValue("@EncryptedPassword", encryptedPassword);

                        using (NpgsqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                user = new User();
                                user.Username = reader["username"].ToString();
                                user.Email = reader["email"].ToString();
                                user.Status = reader["status"].ToString();
                                user.Sicherheitsgruppe = reader["sicherheitsgruppe"].ToString();
                                user.Password = reader["password"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fehler bei der PostgreSQL-Verbindung oder Datenbankoperation: " + ex.Message, "Fehler");
            }

            return user;
        }

        public static void UpdateUserInDatabase(User loggedInUser)
        {
            string connString = "Host=localhost;Port=5432;Username=postgres;Password=Syria2003!;Database=user-database;";

            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(connString))
                {
                    conn.Open();

                    using (NpgsqlCommand cmd = new NpgsqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = "INSERT INTO user_login_table (username, email, status, gruppe, tag, monat, jahr, uhrzeit) VALUES (@Username, @Email, @Status, @SecurityGroup, @Tag, @Monat, @Jahr, @Uhrzeit)";
                        cmd.Parameters.AddWithValue("@Username", loggedInUser.Username);
                        cmd.Parameters.AddWithValue("@Email", loggedInUser.Email);
                        cmd.Parameters.AddWithValue("@Status", loggedInUser.Status);
                        cmd.Parameters.AddWithValue("@SecurityGroup", loggedInUser.Sicherheitsgruppe);
                        cmd.Parameters.AddWithValue("@Tag", DateTime.Now.Day);
                        cmd.Parameters.AddWithValue("@Monat", DateTime.Now.Month);
                        cmd.Parameters.AddWithValue("@Jahr", DateTime.Now.Year);
                        cmd.Parameters.AddWithValue("@Uhrzeit", DateTime.Now.TimeOfDay);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fehler bei der PostgreSQL-Verbindung oder Datenbankoperation: " + ex.Message, "Fehler");
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