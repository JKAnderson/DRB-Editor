using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DRB_Editor
{
    public partial class FormGamePrompt : Form
    {
        public bool Remastered;

        public FormGamePrompt()
        {
            InitializeComponent();
        }

        private void BtnPtde_Click(object sender, EventArgs e)
        {
            Remastered = false;
            Close();
        }

        private void BtnDsr_Click(object sender, EventArgs e)
        {
            Remastered = true;
            Close();
        }
    }
}
