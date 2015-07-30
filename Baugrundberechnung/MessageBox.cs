using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Baugrundberechnung
{
    /// <summary>
    /// Die Form erzeugt eine eigene MessageBox mit einer Checkbox enthalten
    /// </summary>
    public partial class MessageBox : Form
    { 
        /// <summary>
        /// Da der Text über der Checkbox entweder 2 Nachkommastellen oder 3 Nachkommastellen enthalten kann wird es hier gesetzt.
        /// </summary>
        /// <param name="Text"></param>
        public MessageBox(String Text)
        {
            InitializeComponent();
            label1.Text = Text;
            this.TopMost = true;
            this.Activate();
            this.Focus();
            this.Enabled = true;
           
        }
        /// <summary>
        /// Die Methode überprüft ob die Checkbox vorm beenden ausgewählt ist und setzt in Form1 aufAlleUebernehmen true/false.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            Form1.aufAlleUebernehmen = checkBox1.Checked;
            this.Close();
        }
    }
}
