namespace DRB_Editor
{
    partial class FormTextures
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgvTextures = new System.Windows.Forms.DataGridView();
            this.dgvTexturesNameCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvTexturesPathCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTextures)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvTextures
            // 
            this.dgvTextures.AllowUserToResizeColumns = false;
            this.dgvTextures.AllowUserToResizeRows = false;
            this.dgvTextures.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTextures.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvTexturesNameCol,
            this.dgvTexturesPathCol});
            this.dgvTextures.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTextures.Location = new System.Drawing.Point(0, 0);
            this.dgvTextures.Name = "dgvTextures";
            this.dgvTextures.Size = new System.Drawing.Size(565, 450);
            this.dgvTextures.TabIndex = 0;
            this.dgvTextures.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.DgvTextures_RowPostPaint);
            // 
            // dgvTexturesNameCol
            // 
            this.dgvTexturesNameCol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dgvTexturesNameCol.DataPropertyName = "Name";
            this.dgvTexturesNameCol.HeaderText = "Name";
            this.dgvTexturesNameCol.Name = "dgvTexturesNameCol";
            this.dgvTexturesNameCol.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgvTexturesNameCol.Width = 41;
            // 
            // dgvTexturesPathCol
            // 
            this.dgvTexturesPathCol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgvTexturesPathCol.DataPropertyName = "Path";
            this.dgvTexturesPathCol.HeaderText = "Path";
            this.dgvTexturesPathCol.Name = "dgvTexturesPathCol";
            this.dgvTexturesPathCol.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // FormTextures
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(565, 450);
            this.Controls.Add(this.dgvTextures);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "FormTextures";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Textures";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormTextures_FormClosing);
            this.Load += new System.EventHandler(this.FormTextures_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTextures)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvTextures;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvTexturesNameCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvTexturesPathCol;
    }
}