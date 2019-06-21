using SoulsFormats;
using System;
using System.IO;
using System.Media;
using System.Reflection;
using System.Windows.Forms;

namespace DRB_Editor
{
    public partial class FormMain : Form
    {
        private static Properties.Settings Settings = Properties.Settings.Default;

        private DRB Drb;
        private string DrbPath;
        private bool Dsr;

        private FormTextures TextureForm;

        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            Text = $"DRB Editor {Application.ProductVersion}";
            Location = Settings.WindowLocation;
            Size = Settings.WindowSize;
            if (Settings.WindowMaximized)
                WindowState = FormWindowState.Maximized;

            DrbPath = Settings.DRBPath;
            Dsr = Settings.Remastered;
            OpenDRB(Settings.DRBPath, Dsr, true);
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.WindowMaximized = WindowState == FormWindowState.Maximized;
            if (WindowState == FormWindowState.Normal)
            {
                Settings.WindowLocation = Location;
                Settings.WindowSize = Size;
            }
            else
            {
                Settings.WindowLocation = RestoreBounds.Location;
                Settings.WindowSize = RestoreBounds.Size;
            }

            Settings.DRBPath = DrbPath;
            Settings.Remastered = Dsr;

            TextureForm?.Close();
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ofdDrb.FileName = DrbPath;
            if (ofdDrb.ShowDialog() == DialogResult.OK)
            {
                var prompt = new FormGamePrompt();
                prompt.ShowDialog();
                OpenDRB(ofdDrb.FileName, prompt.Remastered);
            }
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                SFUtil.Backup(DrbPath);
                Drb.Write(DrbPath);
                SystemSounds.Asterisk.Play();
            }
            catch (Exception ex)
            {
                ShowError($"Failed to save DRB:\n{DrbPath}\n\n{ex}");
            }
        }

        private void RestoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string bak = DrbPath + ".bak";
            if (!File.Exists(bak))
            {
                ShowError($"Backup not found:\n{bak}");
            }
            else
            {
                try
                {
                    File.Copy(bak, DrbPath, true);
                    SystemSounds.Asterisk.Play();
                    OpenDRB(DrbPath, Dsr);
                }
                catch (Exception ex)
                {
                    ShowError($"Failed to restore backup:\n{bak}\n\n{ex}");
                }
            }
        }

        private void TexturesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (texturesToolStripMenuItem.Checked)
            {
                TextureForm = new FormTextures();
                TextureForm.SetTextures(Drb.Textures);
                TextureForm.FormClosed += TextureForm_FormClosed;
                TextureForm.Show(this);
            }
            else
            {
                TextureForm.Close();
                TextureForm = null;
            }
        }

        private void TextureForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            texturesToolStripMenuItem.Checked = false;
        }

        private void OpenDRB(string path, bool dsr, bool silent = false)
        {
            try
            {
                Drb = DRB.Read(path, dsr);
                DrbPath = path;
                Dsr = dsr;

                statusStripLblPath.Text = $"[{(dsr ? "DSR" : "PTDE")}] {path}";
                lbxDlgs.Enabled = Drb.Dlgs.Count > 0;
                lbxDlgs.ClearSelected();
                lbxDlgs.DisplayMember = "Name";
                lbxDlgos.ClearSelected();
                lbxDlgos.DisplayMember = "Name";
                lbxDlgs.DataSource = Drb.Dlgs;
                saveToolStripMenuItem.Enabled = true;
                texturesToolStripMenuItem.Enabled = true;
                TextureForm?.SetTextures(Drb.Textures);
            }
            catch (Exception ex)
            {
                ShowError($"Failed to open DRB:\n{path}\n\n{ex}", silent);
            }
        }

        private void LbxDlgs_SelectedValueChanged(object sender, EventArgs e)
        {
            var dlg = (DRB.Dlg)lbxDlgs.SelectedItem;
            if (dlg == null)
            {
                pgdDlg.Enabled = false;
                pgdDlg.SelectedObject = null;
                pgdDlgShape.Enabled = false;
                pgdDlgShape.SelectedObject = null;
                lbxDlgos.Enabled = false;
                lbxDlgos.DataSource = null;
            }
            else
            {
                pgdDlg.Enabled = true;
                pgdDlg.SelectedObject = dlg;
                foreach (object item in GetAllGridItems(pgdDlg))
                {
                    var gridItem = (GridItem)item;
                    if (gridItem.PropertyDescriptor.Name == "Unk30")
                        gridItem.Expanded = true;
                }
                pgdDlgShape.Enabled = true;
                pgdDlgShape.SelectedObject = dlg.Shape;
                lbxDlgos.Enabled = dlg.Dlgos.Count > 0;
                lbxDlgos.DataSource = dlg.Dlgos;
            }
        }

        private static GridItemCollection GetAllGridItems(PropertyGrid grid)
        {
            object view = grid.GetType().GetField("gridView", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(grid);
            return (GridItemCollection)view.GetType().InvokeMember("GetAllGridEntries", BindingFlags.InvokeMethod | BindingFlags.NonPublic | BindingFlags.Instance, null, view, null);
        }

        private void LbxDlgos_SelectedValueChanged(object sender, EventArgs e)
        {
            var dlgo = (DRB.Dlgo)lbxDlgos.SelectedItem;
            if (dlgo == null)
            {
                pgdDlgo.Enabled = false;
                pgdDlgo.SelectedObject = null;
                pgdDlgoShape.Enabled = false;
                pgdDlgoShape.SelectedObject = null;
            }
            else
            {
                pgdDlgo.Enabled = true;
                pgdDlgo.SelectedObject = dlgo;
                pgdDlgoShape.Enabled = true;
                pgdDlgoShape.SelectedObject = dlgo.Shape;
            }
        }

        private static void ShowError(string message, bool silent = false)
        {
            if (!silent)
                MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
