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
    public partial class Form3 : Form
    {
        public Label Aquivermächtigkeit = new Label();
        public Label Wasserspiegeldifferenz = new Label();
        public Label Einbindetiefe = new Label();
        private Pen pen_grube;
        private double max;
        public Form3()
        {
            InitializeComponent();
        }
        public void öffnen(String welches, double L, double B, double H, double S, double T, bool ohneBe)
        {
            Graphics gr = Graphics.FromHwnd(Handle);
            if(this.Width-20 < this.Height)
            {
                max = this.Width - 30;
            }
            else
            {
                max = this.Height - 10;
            }
            double lmbd = (H+S)/(max);
            this.Text = welches;
            pen_grube = new Pen(Color.Brown, (float)(2.5));
            gr.DrawLine(pen_grube, new Point((int)(max/2), 0),new Point((int)max/2,(int)((H+T)/lmbd) ));
        }
    }
}
