﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Login_Project
{
    public class BackendPasswort
    {
        public static MainWindow wnd;

        public static void ResetPassword(ForgottenPassword passwordReset, MainWindow main)
        {
            wnd = main;

            string Username = passwordReset.TextBoxBenutzerEingabe.Text;
            string newPassword = passwordReset.TextBoxPasswort.Password;
            string newPasswordAgain = passwordReset.TextBoxPasswortBestätigung.Password;

            bool arePasswordsValid = newPassword == newPasswordAgain;

            if (!arePasswordsValid)
            {
                MessageBox.Show("Passwörter stimmen nicht überein.", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                passwordReset.TextBoxPasswort.Clear();
                passwordReset.TextBoxPasswortBestätigung.Clear();
                passwordReset.TextBoxPasswort.Focus();
                return;
            }

            bool areConditionsMet = PasswordCondition(passwordReset);

            if (!areConditionsMet)
            {
                MessageBox.Show("Fehler bei der Passwortrücksetzung. Bitte überprüfen Sie die eingegebenen Daten.", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                passwordReset.TextBoxPasswort.Clear();
                passwordReset.TextBoxPasswortBestätigung.Clear();
                passwordReset.TextBoxPasswort.Focus();
            }
            else
            {
                bool isUsernameFound = false;

                foreach (User u in BackendRegister.registerUser)
                {
                    if (u.Username == Username)
                    {
                        u.Password = BackendRegister.EncryptPassword(newPassword);
                        isUsernameFound = true;
                        BackendRegister.CSVWrite();
                        break;
                    }
                }

                if (isUsernameFound)
                {
                    MessageBox.Show("Passwort erfolgreich zurückgesetzt.");
                    wnd.content.Content = new Login(wnd);
                    wnd.login.TextBoxBenutzerEmail.Text = passwordReset.TextBoxBenutzerEingabe.Text;
                    BackendRegister.CSVWrite();
                }
            }
        }

        public static bool UserCondition(ForgottenPassword passwd)
        {
            string Benutzer = passwd.TextBoxBenutzerEingabe.Text;

            if (string.IsNullOrEmpty(Benutzer))
            {
                passwd.labelBenutzerExistiertNicht.Visibility = Visibility.Collapsed;
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
                passwd.labelBenutzerExistiertNicht.Visibility = Visibility.Collapsed;
            }
            else
            {
                passwd.labelBenutzerExistiertNicht.Visibility = Visibility.Visible;
            }

            return benutzerGefunden;
        }

        public static void ButtonSearchEnabled(ForgottenPassword passwd)
        {
            string pwd1 = passwd.TextBoxPasswort.Password;
            string pwd2 = passwd.TextBoxPasswortBestätigung.Password;

            bool areTextboxesFilled = !string.IsNullOrEmpty(pwd1) &&
                                      !string.IsNullOrEmpty(pwd2);


            bool isBenutzerUnique = UserCondition(passwd);

            bool doPasswordsMatch = pwd1 == pwd2;

            passwd.ButtonPasswort.IsEnabled = areTextboxesFilled && doPasswordsMatch;

            passwd.ButtonBenutzerEmailSuchen.IsHitTestVisible = isBenutzerUnique;
        }

        public static bool PasswordCondition(ForgottenPassword passwordReset)
        {
            string Passwort = passwordReset.TextBoxPasswort.Password;
            string PasswordBestätigung = passwordReset.TextBoxPasswortBestätigung.Password;
            string Sonderzeichen = "!$@#%^&+=";

            passwordReset.ButtonPasswort.IsEnabled = !string.IsNullOrEmpty(passwordReset.TextBoxPasswort.Password)
                                                  && !string.IsNullOrEmpty(passwordReset.TextBoxPasswortBestätigung.Password);

            if (Passwort != PasswordBestätigung)
            {
                passwordReset.labelPasswortÜbereinstimmung.Foreground = Brushes.Red;
            }
            else
            {
                passwordReset.labelPasswortÜbereinstimmung.Visibility = Visibility.Collapsed;
            }

            bool hasUppercase = Passwort.Any(char.IsUpper);
            bool hasLowercase = Passwort.Any(char.IsLower);
            bool hasDigit = Passwort.Any(char.IsDigit);
            bool isLengthValid = Passwort.Length >= 6 && Passwort.Length <= 20;
            bool hasSpecialCharacter = Passwort.Any(Sonderzeichen.Contains);

            if (hasUppercase)
            {
                passwordReset.labelGroßbuchstaben.Foreground = Brushes.Black;
            }
            else
            {
                passwordReset.labelGroßbuchstaben.Foreground = Brushes.Red;
            }

            if (hasLowercase)
            {
                passwordReset.labelKleinbuchstaben.Foreground = Brushes.Black;
            }
            else
            {
                passwordReset.labelKleinbuchstaben.Foreground = Brushes.Red;
            }

            if (hasDigit)
            {
                passwordReset.labelZahlen.Foreground = Brushes.Black;
            }
            else
            {
                passwordReset.labelZahlen.Foreground = Brushes.Red;
            }

            if (isLengthValid)
            {
                passwordReset.labelLänge.Foreground = Brushes.Black;
            }
            else
            {
                passwordReset.labelLänge.Foreground = Brushes.Red;
            }

            if (hasSpecialCharacter)
            {
                passwordReset.labelSonderzeichen.Foreground = Brushes.Black;
            }
            else
            {
                passwordReset.labelSonderzeichen.Foreground = Brushes.Red;
            }

            return hasUppercase && hasLowercase && hasDigit && isLengthValid && hasSpecialCharacter;
        }

        public static string ReadDataCSV(string inputUsernameOrEmail)
        {
            string encryptedPassword = null;
            string decryptedPassword = null;

            string csvFilePath = "C:\\Login-Project\\benutzerdaten.csv";

            try
            {
                using (StreamReader reader = new StreamReader(csvFilePath))
                {
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        if (line == null)
                            continue;

                        string[] fields = line.Split(',');
                        if (fields.Length < 3)
                            continue;

                        string username = fields[0];
                        string email = fields[1];

                        if (username == inputUsernameOrEmail || email == inputUsernameOrEmail)
                        {
                            encryptedPassword = fields[2];
                            break;
                        }
                    }
                }

                if (!string.IsNullOrEmpty(encryptedPassword))
                {
                    decryptedPassword = DecryptPassword(encryptedPassword);
                }

                return decryptedPassword;
            }
            catch (IOException)
            {
                return null;
            }
        }

        public static string DecryptPassword(string encryptedPassword)
        {
            return encryptedPassword;
        }
    }
}
