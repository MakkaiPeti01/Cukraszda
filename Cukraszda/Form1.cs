using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cukraszda
{
    public partial class FoForm : Form
    {
        public FoForm()
        {
            InitializeComponent();
            lbl_LegdragabbSuti.Text = "Legdrágább süteményünk";
            lbl_Legolcsobb.Text = "Legolcsóbb süteményünk";
            lblTipus.Text = "Süti tipusa:";
        }
    }
}
