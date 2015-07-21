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
            bool temp_bool = Regex.IsMatch(str, @"^[0-9]+\,[0-9]+$") || Regex.IsMatch(str, @"^[0-9]+$");//|| Regex.IsMatch(str, @"^[0-9]+\.[0-9]+$")
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
              /*  if (Regex.IsMatch(str, @"^[0-9]+\.[0-9]+$"))
                {
                    str = str.Replace(".", ",");
                }
               * */

               zahl = Convert.ToDouble(str);//double.Parse(str, CultureInfo.GetCultureInfo("de-DE").NumberFormat,);
               //System.Windows.Forms.MessageBox.Show("Decimalzahl ist: " + zahl);

                switch (zeichen)
                {
                    case "H": if (zahl <= 30 && zahl >= 1)
                        {
                            refToForm.textBox3.BackColor = Color.White;
                            return true;
                        }
                        else
                        {
                            refToForm.textBox3.BackColor = Color.Red;
                            return false;
                        }

                    case "B": if (zahl <= 100 && zahl >= 1)
                        {
                            refToForm.textBox2.BackColor = Color.White;
                            return true;
                        }
                        else
                        {
                            refToForm.textBox2.BackColor = Color.Red;
                            return false;
                        }

                    case "L": if (zahl <= 333 && zahl >= 1)
                        {
                            refToForm.textBox1.BackColor = Color.White;
                            return true;
                        }
                        else
                        {
                            refToForm.textBox1.BackColor = Color.Red;
                            return false;
                        }

                    case "S": if (zahl <= 90 && zahl >= 4)
                        {
                            refToForm.textBox6.BackColor = Color.White;
                            return true;
                        }
                        else
                        {
                            refToForm.textBox6.BackColor = Color.Red;
                            return false;
                        }

                    case "γ": if (zahl <= 13 && zahl >= 8)
                        {
                            refToForm.textBox5.BackColor = Color.White;
                            return true;
                        }
                        else
                        {
                            refToForm.textBox5.BackColor = Color.Red;
                            return false;
                        }

                    case "n": if (zahl <= 2 && zahl >= 1)
                        {
                            refToForm.textBox4.BackColor = Color.White;
                            return true;
                        }
                        else
                        {
                            refToForm.textBox4.BackColor = Color.Red;
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
                            refToForm.textBox3.BackColor = Color.Red;
                            return false;
                        }

                    case "B":
                        {
                            refToForm.textBox2.BackColor = Color.Red;
                            return false;
                        }

                    case "L":
                        {
                            refToForm.textBox1.BackColor = Color.Red;
                            return false;
                        }

                    case "S":
                        {
                            refToForm.textBox6.BackColor = Color.Red;
                            return false;
                        }

                    case "γ":
                        {
                            refToForm.textBox5.BackColor = Color.Red;
                            return false;
                        }

                    case "n":
                        {
                            refToForm.textBox4.BackColor = Color.Red;
                            return false;
                        }

                    default: return false;
                }
            }
        }
    }
}
