namespace NetworkGUI.Forms
{
    partial class NDStatsForm
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.m = new System.Windows.Forms.TextBox();
            this.g2Label2 = new System.Windows.Forms.Label();
            this.g2Label1 = new System.Windows.Forms.Label();
            this.alpha = new System.Windows.Forms.TextBox();
            this.genButton = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.m);
            this.groupBox2.Controls.Add(this.g2Label2);
            this.groupBox2.Controls.Add(this.g2Label1);
            this.groupBox2.Controls.Add(this.alpha);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(413, 115);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Parameters";
            // 
            // m
            // 
            this.m.Location = new System.Drawing.Point(89, 63);
            this.m.Name = "m";
            this.m.Size = new System.Drawing.Size(117, 27);
            this.m.TabIndex = 3;
            this.m.TextChanged += new System.EventHandler(this.m_TextChanged);
            // 
            // g2Label2
            // 
            this.g2Label2.AutoSize = true;
            this.g2Label2.Location = new System.Drawing.Point(42, 67);
            this.g2Label2.Name = "g2Label2";
            this.g2Label2.Size = new System.Drawing.Size(28, 20);
            this.g2Label2.TabIndex = 2;
            this.g2Label2.Text = "M:";
            // 
            // g2Label1
            // 
            this.g2Label1.AutoSize = true;
            this.g2Label1.Location = new System.Drawing.Point(14, 33);
            this.g2Label1.Name = "g2Label1";
            this.g2Label1.Size = new System.Drawing.Size(56, 20);
            this.g2Label1.TabIndex = 1;
            this.g2Label1.Text = "Alpha:";
            // 
            // alpha
            // 
            this.alpha.Location = new System.Drawing.Point(89, 26);
            this.alpha.Name = "alpha";
            this.alpha.Size = new System.Drawing.Size(118, 27);
            this.alpha.TabIndex = 0;
            this.alpha.TextChanged += new System.EventHandler(this.alpha_TextChanged);
            // 
            // genButton
            // 
            this.genButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.genButton.Location = new System.Drawing.Point(124, 146);
            this.genButton.Name = "genButton";
            this.genButton.Size = new System.Drawing.Size(173, 46);
            this.genButton.TabIndex = 6;
            this.genButton.Text = "Generate NDS ";
            this.genButton.UseVisualStyleBackColor = true;
            this.genButton.Click += new System.EventHandler(this.genButton_Click);
            // 
            // NDStatsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(437, 199);
            this.Controls.Add(this.genButton);
            this.Controls.Add(this.groupBox2);
            this.Name = "NDStatsForm";
            this.Text = "NDStatsForm";
            this.Load += new System.EventHandler(this.NDStatsForm_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox m;
        private System.Windows.Forms.Label g2Label2;
        private System.Windows.Forms.Label g2Label1;
        private System.Windows.Forms.TextBox alpha;
        private System.Windows.Forms.Button genButton;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}