namespace NetworkGUI
{
    partial class ConfigModelForm
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
            this.GroupBox2 = new System.Windows.Forms.GroupBox();
            this.g2RadioButton2 = new System.Windows.Forms.RadioButton();
            this.g2RadioButton1 = new System.Windows.Forms.RadioButton();
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.g1RadioButton1 = new System.Windows.Forms.RadioButton();
            this.g1RadioButton2 = new System.Windows.Forms.RadioButton();
            this.browseButton = new System.Windows.Forms.Button();
            this.numTextBox = new System.Windows.Forms.TextBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.inputBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.genButton = new System.Windows.Forms.Button();
            this.GroupBox2.SuspendLayout();
            this.GroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // GroupBox2
            // 
            this.GroupBox2.Controls.Add(this.g2RadioButton2);
            this.GroupBox2.Controls.Add(this.g2RadioButton1);
            this.GroupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GroupBox2.Location = new System.Drawing.Point(13, 120);
            this.GroupBox2.Name = "GroupBox2";
            this.GroupBox2.Size = new System.Drawing.Size(368, 37);
            this.GroupBox2.TabIndex = 19;
            this.GroupBox2.TabStop = false;
            this.GroupBox2.Text = "Self-Ties Allowed? ";
            this.GroupBox2.Enter += new System.EventHandler(this.GroupBox2_Enter);
            // 
            // g2RadioButton2
            // 
            this.g2RadioButton2.AutoSize = true;
            this.g2RadioButton2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.g2RadioButton2.Location = new System.Drawing.Point(270, 0);
            this.g2RadioButton2.Name = "g2RadioButton2";
            this.g2RadioButton2.Size = new System.Drawing.Size(56, 28);
            this.g2RadioButton2.TabIndex = 10;
            this.g2RadioButton2.Text = "No";
            this.g2RadioButton2.UseVisualStyleBackColor = true;
            this.g2RadioButton2.CheckedChanged += new System.EventHandler(this.g2RadioButton2_CheckedChanged);
            // 
            // g2RadioButton1
            // 
            this.g2RadioButton1.AutoSize = true;
            this.g2RadioButton1.Checked = true;
            this.g2RadioButton1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.g2RadioButton1.Location = new System.Drawing.Point(179, 0);
            this.g2RadioButton1.Name = "g2RadioButton1";
            this.g2RadioButton1.Size = new System.Drawing.Size(63, 28);
            this.g2RadioButton1.TabIndex = 9;
            this.g2RadioButton1.TabStop = true;
            this.g2RadioButton1.Text = "Yes";
            this.g2RadioButton1.UseVisualStyleBackColor = true;
            this.g2RadioButton1.CheckedChanged += new System.EventHandler(this.g2RadioButton1_CheckedChanged);
            // 
            // GroupBox1
            // 
            this.GroupBox1.Controls.Add(this.g1RadioButton1);
            this.GroupBox1.Controls.Add(this.g1RadioButton2);
            this.GroupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GroupBox1.Location = new System.Drawing.Point(13, 12);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(387, 65);
            this.GroupBox1.TabIndex = 18;
            this.GroupBox1.TabStop = false;
            this.GroupBox1.Text = "Network Type: ";
            this.GroupBox1.Enter += new System.EventHandler(this.GroupBox1_Enter);
            // 
            // g1RadioButton1
            // 
            this.g1RadioButton1.AutoSize = true;
            this.g1RadioButton1.Checked = true;
            this.g1RadioButton1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.g1RadioButton1.Location = new System.Drawing.Point(151, 0);
            this.g1RadioButton1.Name = "g1RadioButton1";
            this.g1RadioButton1.Size = new System.Drawing.Size(112, 28);
            this.g1RadioButton1.TabIndex = 4;
            this.g1RadioButton1.TabStop = true;
            this.g1RadioButton1.Text = "Unsigned";
            this.g1RadioButton1.UseVisualStyleBackColor = true;
            this.g1RadioButton1.CheckedChanged += new System.EventHandler(this.g1RadioButton1_CheckedChanged);
            // 
            // g1RadioButton2
            // 
            this.g1RadioButton2.AutoSize = true;
            this.g1RadioButton2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.g1RadioButton2.Location = new System.Drawing.Point(151, 32);
            this.g1RadioButton2.Name = "g1RadioButton2";
            this.g1RadioButton2.Size = new System.Drawing.Size(91, 28);
            this.g1RadioButton2.TabIndex = 5;
            this.g1RadioButton2.Text = "Signed";
            this.g1RadioButton2.UseVisualStyleBackColor = true;
            this.g1RadioButton2.CheckedChanged += new System.EventHandler(this.g1RadioButton2_CheckedChanged);
            // 
            // browseButton
            // 
            this.browseButton.BackColor = System.Drawing.SystemColors.ControlLight;
            this.browseButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.browseButton.Location = new System.Drawing.Point(314, 189);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(85, 30);
            this.browseButton.TabIndex = 17;
            this.browseButton.Text = "Browse";
            this.browseButton.UseVisualStyleBackColor = false;
            this.browseButton.Click += new System.EventHandler(this.browseButton_Click);
            // 
            // numTextBox
            // 
            this.numTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numTextBox.Location = new System.Drawing.Point(314, 77);
            this.numTextBox.Name = "numTextBox";
            this.numTextBox.Size = new System.Drawing.Size(85, 28);
            this.numTextBox.TabIndex = 16;
            this.numTextBox.Text = "1";
            this.numTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numTextBox.TextChanged += new System.EventHandler(this.numTextBox_TextChanged);
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.Location = new System.Drawing.Point(21, 80);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(269, 24);
            this.Label1.TabIndex = 15;
            this.Label1.Text = "Number of Random Networks: ";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "csv";
            this.openFileDialog1.Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*";
            this.openFileDialog1.Title = "Browse Input Files";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // inputBox
            // 
            this.inputBox.Enabled = false;
            this.inputBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.inputBox.Location = new System.Drawing.Point(13, 191);
            this.inputBox.Name = "inputBox";
            this.inputBox.Size = new System.Drawing.Size(295, 24);
            this.inputBox.TabIndex = 20;
            this.inputBox.TextChanged += new System.EventHandler(this.inputBox_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(23, 157);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 24);
            this.label2.TabIndex = 21;
            this.label2.Tag = "";
            this.label2.Text = "Input File: ";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // genButton
            // 
            this.genButton.BackColor = System.Drawing.SystemColors.ControlDark;
            this.genButton.Enabled = false;
            this.genButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.genButton.Location = new System.Drawing.Point(152, 235);
            this.genButton.Name = "genButton";
            this.genButton.Size = new System.Drawing.Size(103, 32);
            this.genButton.TabIndex = 22;
            this.genButton.Text = "Generate";
            this.genButton.UseVisualStyleBackColor = false;
            this.genButton.Click += new System.EventHandler(this.genButton_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(412, 282);
            this.Controls.Add(this.genButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.inputBox);
            this.Controls.Add(this.GroupBox2);
            this.Controls.Add(this.GroupBox1);
            this.Controls.Add(this.browseButton);
            this.Controls.Add(this.numTextBox);
            this.Controls.Add(this.Label1);
            this.Name = "Form2";
            this.Text = "Configuration Models";
            this.Load += new System.EventHandler(this.ConfigModel_Load);
            this.GroupBox2.ResumeLayout(false);
            this.GroupBox2.PerformLayout();
            this.GroupBox1.ResumeLayout(false);
            this.GroupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.GroupBox GroupBox2;
        internal System.Windows.Forms.RadioButton g2RadioButton2;
        internal System.Windows.Forms.RadioButton g2RadioButton1;
        internal System.Windows.Forms.GroupBox GroupBox1;
        internal System.Windows.Forms.RadioButton g1RadioButton1;
        internal System.Windows.Forms.RadioButton g1RadioButton2;
        internal System.Windows.Forms.Button browseButton;
        internal System.Windows.Forms.TextBox numTextBox;
        internal System.Windows.Forms.Label Label1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox inputBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button genButton;
    }
}

