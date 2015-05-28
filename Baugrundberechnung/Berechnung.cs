using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baugrundberechnung
{
    /// <summary>
    /// Diese Klasse berechnet die Bemessungsformel für homogenen und isotropen Baugrund
    /// </summary>
    class Berechnung
    {
        /// <summary>
        /// die Methode berechnung berechnet die Formel für die Längsseite, Sitrnseite und Ecke ohne Be.
        /// </summary>
        /// <param name="A"></param>
        /// <param name="U"></param>
        /// <param name="L"></param>
        /// <param name="B"></param>
        /// <param name="H"></param>
        /// <param name="S"></param>
        /// <param name="Y"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        private static double berechnung(double A, double U, double L, double B, double H, double S, double Y, double n)
        {
            double x = 0.32 * A; 
            double y = (1.244 - 0.32 * A) * Math.Exp((-B / H) / (U * (0.541 + 0.395 * (1 - Math.Exp(1 - S / H))) * (1 + (B / L - 0.3) * (3.156 - 1.564 * U))));
            double z = Math.Pow((11 / (Y * 0.902 + 1.078) * (n / (1.3 / 0.95))), Math.Sqrt(2));
            return (x + y) * z;
        }
        /// <summary>
        /// die Methode berechnungeben berechnet die Formel für die seite eben.
        /// </summary>
        /// <param name="A"></param>
        /// <param name="U"></param>
        /// <param name="L"></param>
        /// <param name="B"></param>
        /// <param name="H"></param>
        /// <param name="S"></param>
        /// <param name="Y"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        private static double berechnungeben(double A, double U, double L, double B, double H, double S, double Y, double n)
        {
            double x = 0.32 * A;
            double y = (1.244 - 0.32 * A) * Math.Exp((-B / H) / (U * (0.541 + 0.395 * (1 - Math.Exp(1 - S / H)))));
            double z = Math.Pow((11 / (Y * 0.902 + 1.078) * (n / (1.3 / 0.95))), Math.Sqrt(2));
            return (x + y) * z;
        }
        /// <summary>
        /// Diese Methode ruft berechnung auf und Multipliziert mit be. Zum Richtigen runden auf die 2. Nachkommastelle wird 0.005 addiert.
        /// </summary>
        /// <param name="A"></param>
        /// <param name="U"></param>
        /// <param name="be"></param>
        /// <param name="L"></param>
        /// <param name="B"></param>
        /// <param name="H"></param>
        /// <param name="S"></param>
        /// <param name="Y"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static double berechnungMitBe(double A, double U, double be, double L, double B, double H, double S, double Y, double n)
        {
            return Math.Round((be * berechnung(A, U, L, B, H, S, Y, n) * H) + 0.005, 2);

        }
        /// <summary>
        /// Diese Methode gibt den Wert der berechnung zurück. Zum Richtigen runden auf die 2. Nachkommastelle wird 0.005 addiert
        /// </summary>
        /// <param name="A"></param>
        /// <param name="U"></param>
        /// <param name="L"></param>
        /// <param name="B"></param>
        /// <param name="H"></param>
        /// <param name="S"></param>
        /// <param name="Y"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static double berechnungOhneBe(double A, double U, double L, double B, double H, double S, double Y, double n)
        {
            return Math.Round((berechnung(A, U, L, B, H, S, Y, n) * H) + 0.005, 2);

        }
        /// <summary>
        /// Diese Methode ruft berechnungeben auf und Multipliziert mit be. Zum Richtigen runden auf die 2. Nachkommastelle wird 0.005 addiert.
        /// </summary>
        /// <param name="A"></param>
        /// <param name="U"></param>
        /// <param name="be"></param>
        /// <param name="L"></param>
        /// <param name="B"></param>
        /// <param name="H"></param>
        /// <param name="S"></param>
        /// <param name="Y"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static double berechnungebenMitBe(double A, double U, double be, double L, double B, double H, double S, double Y, double n)
        {
            return Math.Round((be * berechnungeben(A, U, L, B, H, S, Y, n) * H) + 0.005, 2);

        }
        /// <summary>
        /// Diese Methode gibt den Wert der berechnungeben zurück. Zum Richtigen runden auf die 2. Nachkommastelle wird 0.005 addiert
        /// </summary>
        /// <param name="A"></param>
        /// <param name="U"></param>
        /// <param name="L"></param>
        /// <param name="B"></param>
        /// <param name="H"></param>
        /// <param name="S"></param>
        /// <param name="Y"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static double berechnungebenOhneBe(double A, double U, double L, double B, double H, double S, double Y, double n)
        {
            return Math.Round((berechnungeben(A, U, L, B, H, S, Y, n) * H) + 0.005, 2);

        }
    }
}
