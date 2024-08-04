namespace DRB_Editor
{
    partial class FormPreview
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
            this.pbxDialog = new System.Windows.Forms.PictureBox();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.btnColor = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbxDialog)).BeginInit();
            this.SuspendLayout();
            // 
            // pbxDialog
            // 
            this.pbxDialog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbxDialog.Location = new System.Drawing.Point(0, 0);
            this.pbxDialog.Name = "pbxDialog";
            this.pbxDialog.Size = new System.Drawing.Size(800, 450);
            this.pbxDialog.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbxDialog.TabIndex = 0;
            this.pbxDialog.TabStop = false;
            // 
            // colorDialog
            // 
            this.colorDialog.AnyColor = true;
            this.colorDialog.Color = System.Drawing.SystemColors.Control;
            // 
            // btnColor
            // 
            this.btnColor.Location = new System.Drawing.Point(12, 12);
            this.btnColor.Name = "btnColor";
            this.btnColor.Size = new System.Drawing.Size(23, 23);
            this.btnColor.TabIndex = 1;
            this.btnColor.TabStop = false;
            this.btnColor.Text = "C";
            this.btnColor.UseVisualStyleBackColor = true;
            this.btnColor.Click += new System.EventHandler(this.Button1_Click);
            // 
            // FormPreview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnColor);
            this.Controls.Add(this.pbxDialog);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "FormPreview";
            this.Text = "Preview";
            ((System.ComponentModel.ISupportInitialize)(this.pbxDialog)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbxDialog;
        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.Button btnColor;
    }
}