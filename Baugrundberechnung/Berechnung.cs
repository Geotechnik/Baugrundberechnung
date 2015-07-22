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
        /// <param name="raeumlicheAnströmung"></param>
        /// <param name="einflussUmfeld"></param>
        /// <param name="laenge"></param>
        /// <param name="breite"></param>
        /// <param name="wasserspiegeldifferenz"></param>
        /// <param name="aquifermaechtigkeit"></param>
        /// <param name="wichteAuftrieb"></param>
        /// <param name="globaleSicherheit"></param>
        /// <returns></returns>
        private static double berechnung(double raeumlicheAnströmung, double einflussUmfeld, double laenge, double breite, double wasserspiegeldifferenz, double aquifermaechtigkeit, double wichteAuftrieb, double globaleSicherheit)
        {
            double x = 0.32 * raeumlicheAnströmung;
            double y = (1.244 - 0.32 * raeumlicheAnströmung) * Math.Exp((-breite / wasserspiegeldifferenz) / (einflussUmfeld * (0.541 + 0.395 * (1 - Math.Exp(1 - aquifermaechtigkeit / wasserspiegeldifferenz))) * (1 + (breite / laenge - 0.3) * (3.156 - 1.564 * einflussUmfeld))));
            double z = Math.Pow((11 / (wichteAuftrieb * 0.902 + 1.078) * (globaleSicherheit / (1.3 / 0.95))), Math.Sqrt(2));
            return (x + y) * z;
        }
        /// <summary>
        /// die Methode berechnungeben berechnet die Formel für die seite eben.
        /// </summary>
        /// <param name="raeumlicheAnströmung"></param>
        /// <param name="einflussUmfeld"></param>
        /// <param name="laenge"></param>
        /// <param name="breite"></param>
        /// <param name="wasserspiegeldifferenz"></param>
        /// <param name="aquifermaechtigkeit"></param>
        /// <param name="wichteAuftrieb"></param>
        /// <param name="globaleSicherheit"></param>
        /// <returns></returns>
        private static double berechnungeben(double raeumlicheAnströmung, double einflussUmfeld, double laenge, double breite, double wasserspiegeldifferenz, double aquifermaechtigkeit, double wichteAuftrieb, double globaleSicherheit)
        {
            double x = 0.32 * raeumlicheAnströmung;
            double y = (1.244 - 0.32 * raeumlicheAnströmung) * Math.Exp((-breite / wasserspiegeldifferenz) / (einflussUmfeld * (0.541 + 0.395 * (1 - Math.Exp(1 - aquifermaechtigkeit / wasserspiegeldifferenz)))));
            double z = Math.Pow((11 / (wichteAuftrieb * 0.902 + 1.078) * (globaleSicherheit / (1.3 / 0.95))), Math.Sqrt(2));
            return (x + y) * z;
        }
        /// <summary>
        /// Diese Methode ruft berechnung auf und Multipliziert mit be. Zum Richtigen runden auf die 2. Nachkommastelle wird 0.005 addiert.
        /// </summary>
        /// <param name="raeumlicheAnströmung"></param>
        /// <param name="einflussUmfeld"></param>
        /// <param name="be"></param>
        /// <param name="laenge"></param>
        /// <param name="breite"></param>
        /// <param name="wasserspiegeldifferenz"></param>
        /// <param name="aquifermaechtigkeit"></param>
        /// <param name="wichteAuftrieb"></param>
        /// <param name="globaleSicherheit"></param>
        /// <returns></returns>
        public static double berechnungMitBe(double raeumlicheAnströmung, double einflussUmfeld, double be, double laenge, double breite, double wasserspiegeldifferenz, double aquifermaechtigkeit, double wichteAuftrieb, double globaleSicherheit)
        {
            return Math.Round((be * berechnung(raeumlicheAnströmung, einflussUmfeld, laenge, breite, wasserspiegeldifferenz, aquifermaechtigkeit, wichteAuftrieb, globaleSicherheit) * wasserspiegeldifferenz) + 0.005, 2);

        }
        /// <summary>
        /// Diese Methode gibt den Wert der berechnung zurück. Zum Richtigen runden auf die 2. Nachkommastelle wird 0.005 addiert
        /// </summary>
        /// <param name="raeumlicheAnströmung"></param>
        /// <param name="einflussUmfeld"></param>
        /// <param name="laenge"></param>
        /// <param name="breite"></param>
        /// <param name="wasserspiegeldifferenz"></param>
        /// <param name="aquifermaechtigkeit"></param>
        /// <param name="wichteAuftrieb"></param>
        /// <param name="globaleSicherheit"></param>
        /// <returns></returns>
        public static double berechnungOhneBe(double raeumlicheAnströmung, double einflussUmfeld, double laenge, double breite, double wasserspiegeldifferenz, double aquifermaechtigkeit, double wichteAuftrieb, double globaleSicherheit)
        {
            return Math.Round((berechnung(raeumlicheAnströmung, einflussUmfeld, laenge, breite, wasserspiegeldifferenz, aquifermaechtigkeit, wichteAuftrieb, globaleSicherheit) * wasserspiegeldifferenz) + 0.005, 2);

        }
        /// <summary>
        /// Diese Methode ruft berechnungeben auf und Multipliziert mit be. Zum Richtigen runden auf die 2. Nachkommastelle wird 0.005 addiert.
        /// </summary>
        /// <param name="raeumlicheAnströmung"></param>
        /// <param name="einflussUmfeld"></param>
        /// <param name="be"></param>
        /// <param name="laenge"></param>
        /// <param name="breite"></param>
        /// <param name="wasserspiegeldifferenz"></param>
        /// <param name="aquifermaechtigkeit"></param>
        /// <param name="wichteAuftrieb"></param>
        /// <param name="globaleSicherheit"></param>
        /// <returns></returns>
        public static double berechnungebenMitBe(double raeumlicheAnströmung, double einflussUmfeld, double be, double laenge, double breite, double wasserspiegeldifferenz, double aquifermaechtigkeit, double wichteAuftrieb, double globaleSicherheit)
        {
            return Math.Round((be * berechnungeben(raeumlicheAnströmung, einflussUmfeld, laenge, breite, wasserspiegeldifferenz, aquifermaechtigkeit, wichteAuftrieb, globaleSicherheit) * wasserspiegeldifferenz) + 0.005, 2);

        }
        /// <summary>
        /// Diese Methode gibt den Wert der berechnungeben zurück. Zum Richtigen runden auf die 2. Nachkommastelle wird 0.005 addiert
        /// </summary>
        /// <param name="raeumlicheAnströmung"></param>
        /// <param name="einflussUmfeld"></param>
        /// <param name="laenge"></param>
        /// <param name="breite"></param>
        /// <param name="wasserspiegeldifferenz"></param>
        /// <param name="aquifermaechtigkeit"></param>
        /// <param name="wichteAuftrieb"></param>
        /// <param name="globaleSicherheit"></param>
        /// <returns></returns>
        public static double berechnungebenOhneBe(double raeumlicheAnströmung, double einflussUmfeld, double laenge, double breite, double wasserspiegeldifferenz, double aquifermaechtigkeit, double wichteAuftrieb, double globaleSicherheit)
        {
            return Math.Round((berechnungeben(raeumlicheAnströmung, einflussUmfeld, laenge, breite, wasserspiegeldifferenz, aquifermaechtigkeit, wichteAuftrieb, globaleSicherheit) * wasserspiegeldifferenz) + 0.005, 2);

        }
    }
}
