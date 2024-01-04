using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Login_Project
{
    public class BackendBenutzerinfo
    {
        public static string LabelEmail = "";
        public static string LabelPasswort = "";
        public static string LabelGruppe = "";
        public static string LabelStatus = "";

        public static void UserInfo(AdminOverview admin, UserInformation benutzerinfo)
        {
            string benutzername = admin.TextBoxBenutzerEingabeSuche.Text;

            foreach (User u in BackendRegister.registerUser)
            {
                if (u.Username == benutzername)
                {
                    if (u.Status == "Benutzer")
                    {
                        LabelEmail = u.Email;
                        LabelPasswort = u.Password;
                        LabelGruppe = "Mitarbeiter";
                        LabelStatus = u.Status;
                    }
                    else
                    {
                        LabelEmail = u.Email;
                        LabelPasswort = u.Password;
                        LabelGruppe = "Administratoren";
                        LabelStatus = u.Status;
                    }
                }
            }

            benutzerinfo.labelBenutzernameA.Content = benutzername;
            benutzerinfo.labelEmailA.Content = LabelEmail;
            benutzerinfo.labelPasswortA.Content = LabelPasswort;
            benutzerinfo.labelGruppeA.Content = LabelGruppe;
            benutzerinfo.labelStatusA.Content = LabelStatus;
        }
    }
}
