using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;
using System.Globalization;
using System.IO;
using System.Windows.Controls;
using System.Collections;

namespace Login_Project
{
    public class BackendAdministrator
    {
        public static string csvDatei = "C:\\Users/Fujitsu/Desktop/XML-Validator-Frontend/public/ressources/benutzerdaten.csv";

        public static void AdminLogin(Administrator admin)
        {
            List<User> sortedUsers = BackendRegister.registerUser.OrderBy(user =>
            {
                if (user.Status == "Administrator" || user.Sicherheitsgruppe == "Administratoren")
                {
                    return 0;
                }
                return 1;

            }).ToList();

            foreach (User user in sortedUsers)
            {
                TextBlock textBlockBenutzer = new TextBlock();
                TextBlock textBlockEmail = new TextBlock();
                TextBlock textBlockStatus = new TextBlock();
                TextBlock textBlockGruppe = new TextBlock();
                TextBlock textBlockPasswort = new TextBlock();

                textBlockBenutzer.Text = $"{user.Username}";
                textBlockEmail.Text = $" {user.Email}";
                textBlockStatus.Text = $"{user.Status}";
                textBlockGruppe.Text = $"{user.Sicherheitsgruppe}";
                textBlockPasswort.Text = $"{user.Password}";

                admin.ListBoxBenutzer.Items.Add(textBlockBenutzer);
                admin.ListBoxBenutzer.Items.Add(textBlockEmail);
                admin.ListBoxBenutzer.Items.Add(textBlockStatus);
                admin.ListBoxBenutzer.Items.Add(textBlockGruppe);
                admin.ListBoxBenutzer.Items.Add(textBlockPasswort);

                if (user.Status == "Administrator" || user.Sicherheitsgruppe == "Administratoren")
                {
                    textBlockBenutzer.FontWeight = FontWeights.Bold;
                    textBlockEmail.FontWeight = FontWeights.Bold;
                    textBlockStatus.FontWeight = FontWeights.Bold;
                    textBlockGruppe.FontWeight = FontWeights.Bold;
                    textBlockPasswort.FontWeight = FontWeights.Bold;
                }

                if (sortedUsers.IndexOf(user) < sortedUsers.Count - 1)
                {
                    Border horizontalStrich = new Border();
                    horizontalStrich.BorderThickness = new Thickness(0, 3, 730, 3);
                    horizontalStrich.BorderBrush = Brushes.Black;

                    admin.ListBoxBenutzer.Items.Add(horizontalStrich);
                }
            }

        }
    }
}
