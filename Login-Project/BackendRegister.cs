using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Npgsql;

namespace Login_Project
{
    public class BackendRegister
    {
        public static MainWindow wnd;

        public static List<User> registerUser = new List<User>();

        public static bool UserIsUnique = true;

        public static void TryRegister(Register register, MainWindow main)
        {
            wnd = main;

            string User = register.TextBoxBenutzer.Text;
            string Email = register.TextBoxEmail.Text;
            string Password = register.TextBoxPasswort.Password;
            string PasswordAgain = register.TextBoxPasswortBestätigung.Password;

            foreach (User user in registerUser)
            {
                if (user.Username == User)
                {
                    UserIsUnique = false;
                    MessageBox.Show("Der Benutzer existiert schon.", "Fehler");
                    register.TextBoxBenutzer.Clear();
                    register.TextBoxBenutzer.Focus();
                }
                else if (user.Email == Email)
                {
                    UserIsUnique = false;
                    MessageBox.Show("Die Email-Adresse existiert schon.", "Fehler");
                    register.TextBoxEmail.Clear();
                    register.TextBoxEmail.Focus();
                }
            }

            bool arePasswordsValid = Password == PasswordAgain;

            if (!arePasswordsValid)
            {
                MessageBox.Show("Passwörter stimmen nicht überein.", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                register.TextBoxPasswort.Clear();
                register.TextBoxPasswortBestätigung.Clear();
                register.TextBoxPasswort.Focus();
                return;
            }

            bool areConditionsMet = PasswordCondition(register);

            if (!areConditionsMet)
            {
                MessageBox.Show("Fehler bei der Passwortrücksetzung. Bitte überprüfen Sie die eingegebenen Daten.", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                register.TextBoxPasswort.Clear();
                register.TextBoxPasswortBestätigung.Clear();
                register.TextBoxPasswort.Focus();
            }

            if (!UserValid(User))
            {
                MessageBox.Show("Ungültiger Benutzername.", "Fehler");
                register.TextBoxBenutzer.Clear();
                register.TextBoxBenutzer.Focus();
                return;
            }

            if (!EmailValid(Email))
            {
                MessageBox.Show("Ungültige Email-Adresse.", "Fehler");
                register.TextBoxEmail.Clear();
                register.TextBoxEmail.Focus();
                return;
            }

            User newUser = new User
            {
                Username = User,
                Email = Email,
                Password = EncryptPassword(Password),
                Status = "Benutzer",
                Sicherheitsgruppe = "Mitarbeiter"
            };


            if (UserIsUnique && PasswordCondition(register))
            {
                MessageBox.Show("Registrierung erfolgreich.");
                registerUser.Add(newUser);
                PostgreSQLWrite();

                wnd.content.Content = new Login(wnd);
                wnd.login.TextBoxBenutzerEmail.Text = register.TextBoxBenutzer.Text;
            }
        }

        public static bool UserValid(string input)
        {
            string usernamePattern = "^[a-zA-Z0-9]+$";
            return Regex.IsMatch(input, usernamePattern);
        }

        public static bool EmailValid(string email)
        {
            string pattern = @"^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,})+$";
            return Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase);
        }

        public static string EncryptPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] hashedBytes = sha256.ComputeHash(passwordBytes);

                return Convert.ToBase64String(hashedBytes);
            }
        }

        public static bool PasswordCondition(Register register)
        {
            string Passwort = register.TextBoxPasswort.Password;
            string PasswordBestätigung = register.TextBoxPasswortBestätigung.Password;
            string Sonderzeichen = "!$@#%^&+=";

            register.ButtonRegister.IsEnabled = !string.IsNullOrEmpty(register.TextBoxPasswort.Password)
                                             && !string.IsNullOrEmpty(register.TextBoxPasswortBestätigung.Password);

            if (Passwort != PasswordBestätigung)
            {
                register.labelPasswortÜbereinstimmung.Foreground = Brushes.Red;
            }
            else if (PasswordBestätigung != Passwort)
            {
                register.labelPasswortÜbereinstimmung.Foreground = Brushes.Red;
            }
            else
            {
                register.labelPasswortÜbereinstimmung.Visibility = Visibility.Collapsed;
            }

            bool hasUppercase = Passwort.Any(char.IsUpper);
            bool hasLowercase = Passwort.Any(char.IsLower);
            bool hasDigit = Passwort.Any(char.IsDigit);
            bool isLengthValid = Passwort.Length >= 6 && Passwort.Length <= 20;
            bool hasSpecialCharacter = Passwort.Any(Sonderzeichen.Contains);

            if (hasUppercase)
            {
                register.labelGroßbuchstaben.Foreground = Brushes.Black;
            }
            else
            {
                register.labelGroßbuchstaben.Foreground = Brushes.Red;
            }

            if (hasLowercase)
            {
                register.labelKleinbuchstaben.Foreground = Brushes.Black;
            }
            else
            {
                register.labelKleinbuchstaben.Foreground = Brushes.Red;
            }

            if (hasDigit)
            {
                register.labelZahlen.Foreground = Brushes.Black;
            }
            else
            {
                register.labelZahlen.Foreground = Brushes.Red;
            }

            if (isLengthValid)
            {
                register.labelLänge.Foreground = Brushes.Black;
            }
            else
            {
                register.labelLänge.Foreground = Brushes.Red;
            }

            if (hasSpecialCharacter)
            {
                register.labelSonderzeichen.Foreground = Brushes.Black;
            }
            else
            {
                register.labelSonderzeichen.Foreground = Brushes.Red;
            }

            return hasUppercase && hasLowercase && hasDigit && isLengthValid && hasSpecialCharacter;
        }

        public static bool UserCondition(Register register)
        {
            string Benutzer = register.TextBoxBenutzer.Text;

            User foundUser = BackendAdminÜbersicht.SearchUserInDatabase(Benutzer);

            if (foundUser != null)
            {
                if (register.labelBenutzerExistiert != null)
                {
                    register.labelBenutzerExistiert.Visibility = Visibility.Visible;
                }
                return false;
            }
            else
            {
                if (register.labelBenutzerExistiert != null)
                {
                    register.labelBenutzerExistiert.Visibility = Visibility.Collapsed;
                }
                return true;
            }
        }

        public static bool EmailCondition(Register register)
        {
            string emailAdresse = register.TextBoxEmail.Text;

            if (!emailAdresse.Contains("@"))
            {
                if (register.labelAT != null)
                {
                    register.labelAT.Foreground = Brushes.Red;
                }
                return false;
            }
            else
            {
                if (register.labelAT != null)
                {
                    register.labelAT.Foreground = Brushes.Black;
                }
            }

            if (!emailAdresse.Contains("."))
            {
                if (register.labelPunkt != null)
                {
                    register.labelPunkt.Foreground = Brushes.Red;
                }
                return false;
            }
            else
            {
                if (register.labelPunkt != null)
                {
                    register.labelPunkt.Foreground = Brushes.Black;
                }
            }

            User foundEmail = SearchEmailInDatabase(emailAdresse);

            if (foundEmail != null)
            {
                if (register.labelEmailExistiert != null)
                {
                    register.labelEmailExistiert.Visibility = Visibility.Visible;
                }
                return false;
            }
            else
            {
                if (register.labelEmailExistiert != null)
                {
                    register.labelEmailExistiert.Visibility = Visibility.Collapsed;
                }
                return true;
            }
        }

        public static void ButtonRegsiterEnabled(Register register)
        {
            string Benutzer = register.TextBoxBenutzer.Text;
            string Email = register.TextBoxEmail.Text;
            string pwd1 = register.TextBoxPasswort.Password;
            string pwd2 = register.TextBoxPasswortBestätigung.Password;

            bool areTextboxesFilled = !string.IsNullOrEmpty(Benutzer) &&
                                      !string.IsNullOrEmpty(Email) &&
                                      !string.IsNullOrEmpty(pwd1) &&
                                      !string.IsNullOrEmpty(pwd2);

            bool isBenutzerUnique = UserCondition(register);
            bool isEmailUnique = EmailCondition(register);

            bool doPasswordsMatch = pwd1 == pwd2;

            register.ButtonRegister.IsEnabled = areTextboxesFilled && isBenutzerUnique && isEmailUnique && doPasswordsMatch;
        }

        public static void PostgreSQLWrite()
        {
            string connString = "Host=localhost;Port=5432;Username=postgres;Password=Syria2003!;Database=user-database;";

            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(connString))
                {
                    conn.Open();

                    if (registerUser != null)
                    {
                        foreach (User u in registerUser)
                        {
                            if (!UserExistsInDatabase(u.Username, conn))
                            {
                                InsertUserIntoDatabase(u, conn);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fehler bei der PostgreSQL-Verbindung oder Datenbankoperation: " + ex.Message, "Fehler");
            }
        }

        private static bool UserExistsInDatabase(string username, NpgsqlConnection conn)
        {
            using (NpgsqlCommand cmd = new NpgsqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandText = "SELECT COUNT(*) FROM user_table WHERE username = @Username";
                cmd.Parameters.AddWithValue("@Username", username);

                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0;
            }
        }

        private static void InsertUserIntoDatabase(User user, NpgsqlConnection conn)
        {
            using (NpgsqlCommand cmd = new NpgsqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO user_table (username, email, status, sicherheitsgruppe, password) " +
                                  "VALUES (@Username, @Email, @Status, @Sicherheitsgruppe, @Password)";

                cmd.Parameters.AddWithValue("@Username", user.Username);
                cmd.Parameters.AddWithValue("@Email", user.Email);
                cmd.Parameters.AddWithValue("@Status", user.Status);
                cmd.Parameters.AddWithValue("@Sicherheitsgruppe", user.Sicherheitsgruppe);
                cmd.Parameters.AddWithValue("@Password", user.Password);

                cmd.ExecuteNonQuery();
            }
        }

        public static User SearchEmailInDatabase(string email)
        {
            User foundEmail = null;

            string connString = "Host=localhost;Port=5432;Username=postgres;Password=Syria2003!;Database=user-database;";

            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(connString))
                {
                    conn.Open();

                    string query = "SELECT * FROM user_table WHERE email = @Email";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Email", email);

                        using (NpgsqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                foundEmail = new User();
                                foundEmail.Username = reader["username"].ToString();
                                foundEmail.Status = reader["status"].ToString();
                                foundEmail.Sicherheitsgruppe = reader["sicherheitsgruppe"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fehler bei der PostgreSQL-Verbindung oder Datenbankoperation: " + ex.Message, "Fehler");
            }

            return foundEmail;
        }
    }
}

public class User
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string Status { get; set; }
    public string Sicherheitsgruppe { get; set; }
}