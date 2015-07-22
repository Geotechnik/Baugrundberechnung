using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Windows.Forms;
using System.Drawing;

namespace Baugrundberechnung
{
    class Validierung
    {
        /// <summary>
        /// return true if str consists only of number and comma or just numbers
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Form1 refToForm;
        private static bool isdecimalnumber(string str)
        {
            //bool isdec = Regex.IsMatch(str, @"^\d$") && str.Contains(",");
            //bool isnat = Regex.IsMatch(str, @"^\d$");
            //return (isdec || isnat) ;
            //return Regex.IsMatch(str, @"^[0-9,\,]+$");
            bool temp_bool = Regex.IsMatch(str, @"^[0-9]+\,[0-9]+$") || Regex.IsMatch(str, @"^[0-9]+$") || Regex.IsMatch(str, @"^[0-9]+\.[0-9]+$");
            return temp_bool;
        }

        public static void setFormReference(Baugrundberechnung.Form1 parent)
        {
            refToForm = parent;
        }
        /// <summary>
        /// Überprüft ob die Eingabe gültig ist. Wenn nicht färbt es die Entsprechende TextBox Rot. 
        /// wenn alles in ordnung ist wird sie wieder Weiß gefärbt.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="zeichen"></param>
        /// <returns></returns>
        public static bool isValidNumber(string str, string zeichen)
        {

            if (Validierung.isdecimalnumber(str))
            {
                double zahl;
               if (Regex.IsMatch(str, @"^[0-9]+\.[0-9]+$"))
                {
                    str = str.Replace(".", ",");
                }
            

               zahl = Convert.ToDouble(str);//double.Parse(str, CultureInfo.GetCultureInfo("de-DE").NumberFormat,);
               //System.Windows.Forms.MessageBox.Show("Decimalzahl ist: " + zahl);

                switch (zeichen)
                {
                    case "H": if (zahl <= 30 && zahl >= 1)
                        {
                            refToForm.Wasserspiegeldifferenz.BackColor = Color.White;
                            return true;
                        }
                        else
                        {
                            refToForm.Wasserspiegeldifferenz.BackColor = Color.Red;
                            return false;
                        }

                    case "B": if (zahl <= 100 && zahl >= 1)
                        {
                            refToForm.BreiteBaugrube.BackColor = Color.White;
                            return true;
                        }
                        else
                        {
                            refToForm.BreiteBaugrube.BackColor = Color.Red;
                            return false;
                        }

                    case "L": if (zahl <= 333 && zahl >= 1)
                        {
                            refToForm.LängeBaugrube.BackColor = Color.White;
                            return true;
                        }
                        else
                        {
                            refToForm.LängeBaugrube.BackColor = Color.Red;
                            return false;
                        }

                    case "S": if (zahl <= 90 && zahl >= 4)
                        {
                            refToForm.Aquivermächtigkeit.BackColor = Color.White;
                            return true;
                        }
                        else
                        {
                            refToForm.Aquivermächtigkeit.BackColor = Color.Red;
                            return false;
                        }

                    case "γ": if (zahl <= 13 && zahl >= 8)
                        {
                            refToForm.WichteAuftrieb.BackColor = Color.White;
                            return true;
                        }
                        else
                        {
                            refToForm.WichteAuftrieb.BackColor = Color.Red;
                            return false;
                        }

                    case "n": if (zahl <= 2 && zahl >= 1)
                        {
                            refToForm.GlobaleSicherheit.BackColor = Color.White;
                            return true;
                        }
                        else
                        {
                            refToForm.GlobaleSicherheit.BackColor = Color.Red;
                            return false;
                        }

                    default: return false;
                }
            }
            else
            {
                switch (zeichen)
                {
                    case "H":
                        {
                            refToForm.Wasserspiegeldifferenz.BackColor = Color.Red;
                            return false;
                        }

                    case "B":
                        {
                            refToForm.BreiteBaugrube.BackColor = Color.Red;
                            return false;
                        }

                    case "L":
                        {
                            refToForm.LängeBaugrube.BackColor = Color.Red;
                            return false;
                        }

                    case "S":
                        {
                            refToForm.Aquivermächtigkeit.BackColor = Color.Red;
                            return false;
                        }

                    case "γ":
                        {
                            refToForm.WichteAuftrieb.BackColor = Color.Red;
                            return false;
                        }

                    case "n":
                        {
                            refToForm.GlobaleSicherheit.BackColor = Color.Red;
                            return false;
                        }

                    default: return false;
                }
            }
        }
    }
}
