using SoulsFormats;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace DRB_Editor
{
    public partial class FormTextures : Form
    {
        private static readonly Properties.Settings Settings = Properties.Settings.Default;

        public FormTextures()
        {
            InitializeComponent();
            dgvTextures.AutoGenerateColumns = false;
        }

        private void FormTextures_Load(object sender, EventArgs e)
        {
            Location = Settings.TextureFormLocation;
            Size = Settings.TextureFormSize;
        }

        private void FormTextures_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                Settings.TextureFormLocation = Location;
                Settings.TextureFormSize = Size;
            }
            else
            {
                Settings.TextureFormLocation = RestoreBounds.Location;
                Settings.TextureFormSize = RestoreBounds.Size;
            }
        }

        public void SetTextures(List<DRB.Texture> textures)
        {
            dgvTextures.DataSource = new BindingList<DRB.Texture>(textures);
        }

        // https://stackoverflow.com/a/12840794
        private void DgvTextures_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            string index = $"{e.RowIndex}";
            var centerFormat = new StringFormat()
            {
                Alignment = StringAlignment.Far,
                LineAlignment = StringAlignment.Center
            };
            var headerBounds = new Rectangle(e.RowBounds.Left + 5, e.RowBounds.Top + 5, dgvTextures.RowHeadersWidth - 10, e.RowBounds.Height - 10);
            e.Graphics.DrawString(index, Font, SystemBrushes.ControlText, headerBounds, centerFormat);
        }
    }
}
