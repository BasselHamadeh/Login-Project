using System;
using System.Diagnostics;
using System.Windows;
using Npgsql;

namespace Login_Project
{
    public partial class Login
    {
        public static void TryLogin(Login login, MainWindow main)
        {
            wnd = main;

            string UsernameEmail = login.TextBoxBenutzerEmail.Text;
            string Password = login.TextBoxPasswort.Password;

            string EncryptPassword = Register.EncryptPassword(Password);

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
                    wnd.Close();
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

        public static User FetchUserFromDatabase(string UsernameAndEmail, string encryptedPassword)
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
                        cmd.Parameters.AddWithValue("@UsernameEmail", UsernameAndEmail);
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
                        cmd.CommandText = "INSERT INTO user_login_table (username, email, gruppe, tag, monat, jahr, uhrzeit) VALUES (@Username, @Email, @gruppe, @Tag, @Monat, @Jahr, @Uhrzeit)";
                        cmd.Parameters.AddWithValue("@Username", loggedInUser.Username);
                        cmd.Parameters.AddWithValue("@Email", loggedInUser.Email);
                        cmd.Parameters.AddWithValue("@gruppe", loggedInUser.Sicherheitsgruppe);
                        cmd.Parameters.AddWithValue("@Tag", DateTime.Now.Day);
                        cmd.Parameters.AddWithValue("@Monat", DateTime.Now.Month);
                        cmd.Parameters.AddWithValue("@Jahr", DateTime.Now.Year);
                        cmd.Parameters.AddWithValue("@Uhrzeit", DateTime.Now.ToString("HH:mm"));

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