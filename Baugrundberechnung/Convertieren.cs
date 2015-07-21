using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Baugrundberechnung
{
    class Convertieren
    {
        public static double convertToDouble(String zahl)
        {
            double ret;
              if (Regex.IsMatch(zahl, @"^[0-9]+\.[0-9]+$"))
              {
                  zahl = zahl.Replace(".", ",");
              }
            ret = Convert.ToDouble(zahl);//double.Parse(str, CultureInfo.GetCultureInfo("de-DE").NumberFormat,);
            //System.Windows.Forms.MessageBox.Show("Decimalzahl ist: " + zahl);
            return ret;
        }
    }
}
