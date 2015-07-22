using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Baugrundberechnung
{
    /// <summary>
    /// Convertiert die Zahl der Eingabe in eine Kommazahl.
    /// </summary>
    class Convertieren
    {
        public static double convertToDouble(String str)
        {
            double zahl;
              if (Regex.IsMatch(str, @"^[0-9]+\.[0-9]+$"))
              {
                  str = str.Replace(".", ",");
              }
            zahl = Convert.ToDouble(str);
            return zahl;
        }
    }
}
