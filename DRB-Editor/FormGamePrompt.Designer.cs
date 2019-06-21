namespace DRB_Editor
{
    partial class FormGamePrompt
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
            this.btnPtde = new System.Windows.Forms.Button();
            this.btnDsr = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnPtde
            // 
            this.btnPtde.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPtde.Location = new System.Drawing.Point(12, 70);
            this.btnPtde.Name = "btnPtde";
            this.btnPtde.Size = new System.Drawing.Size(100, 23);
            this.btnPtde.TabIndex = 0;
            this.btnPtde.Text = "Prepare to Die";
            this.btnPtde.UseVisualStyleBackColor = true;
            this.btnPtde.Click += new System.EventHandler(this.BtnPtde_Click);
            // 
            // btnDsr
            // 
            this.btnDsr.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDsr.Location = new System.Drawing.Point(118, 70);
            this.btnDsr.Name = "btnDsr";
            this.btnDsr.Size = new System.Drawing.Size(100, 23);
            this.btnDsr.TabIndex = 1;
            this.btnDsr.Text = "Remastered";
            this.btnDsr.UseVisualStyleBackColor = true;
            this.btnDsr.Click += new System.EventHandler(this.BtnDsr_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(206, 58);
            this.label1.TabIndex = 2;
            this.label1.Text = "Is this DRB from Prepare to Die Edition,\r\nor Dark Souls Remastered?";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FormGamePrompt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(230, 105);
            this.ControlBox = false;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnDsr);
            this.Controls.Add(this.btnPtde);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormGamePrompt";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Game Version";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnPtde;
        private System.Windows.Forms.Button btnDsr;
        private System.Windows.Forms.Label label1;
    }
}