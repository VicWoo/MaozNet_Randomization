﻿namespace NetworkGUI.Forms
{
    partial class ABMForm
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
            this.nText = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.netText = new System.Windows.Forms.TextBox();
            this.netIdent = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.minbox = new System.Windows.Forms.TextBox();
            this.stepbox = new System.Windows.Forms.TextBox();
            this.maxbox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.paramsButton = new System.Windows.Forms.Button();
            this.prefAttachButton = new System.Windows.Forms.RadioButton();
            this.homophilyButton = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // nText
            // 
            this.nText.Enabled = false;
            this.nText.Location = new System.Drawing.Point(164, 92);
            this.nText.Name = "nText";
            this.nText.Size = new System.Drawing.Size(115, 20);
            this.nText.TabIndex = 1;
            this.nText.Text = "10";
            this.nText.Visible = false;
            this.nText.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Enabled = false;
            this.label1.Location = new System.Drawing.Point(60, 99);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "# of Nodes";
            this.label1.Visible = false;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(60, 99);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "# of Networks";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // netText
            // 
            this.netText.Location = new System.Drawing.Point(164, 92);
            this.netText.Name = "netText";
            this.netText.Size = new System.Drawing.Size(115, 20);
            this.netText.TabIndex = 2;
            this.netText.Text = "10";
            this.netText.TextChanged += new System.EventHandler(this.netText_TextChanged);
            // 
            // netIdent
            // 
            this.netIdent.Location = new System.Drawing.Point(164, 64);
            this.netIdent.Name = "netIdent";
            this.netIdent.Size = new System.Drawing.Size(115, 20);
            this.netIdent.TabIndex = 0;
            this.netIdent.Text = "1";
            this.netIdent.TextChanged += new System.EventHandler(this.textBox1_TextChanged_1);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(60, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Starting Network ID";
            // 
            // minbox
            // 
            this.minbox.Location = new System.Drawing.Point(40, 143);
            this.minbox.Name = "minbox";
            this.minbox.Size = new System.Drawing.Size(56, 20);
            this.minbox.TabIndex = 4;
            this.minbox.Text = "10";
            this.minbox.TextChanged += new System.EventHandler(this.minbox_TextChanged);
            // 
            // stepbox
            // 
            this.stepbox.Location = new System.Drawing.Point(102, 144);
            this.stepbox.Name = "stepbox";
            this.stepbox.Size = new System.Drawing.Size(56, 20);
            this.stepbox.TabIndex = 5;
            this.stepbox.Text = "10";
            this.stepbox.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // maxbox
            // 
            this.maxbox.Location = new System.Drawing.Point(164, 143);
            this.maxbox.Name = "maxbox";
            this.maxbox.Size = new System.Drawing.Size(56, 20);
            this.maxbox.TabIndex = 6;
            this.maxbox.Text = "100";
            this.maxbox.TextChanged += new System.EventHandler(this.maxbox_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(56, 167);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(24, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Min";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(112, 168);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Step";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(174, 169);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(27, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Max";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(66, 119);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(126, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "Network Size Parameters";
            // 
            // paramsButton
            // 
            this.paramsButton.Location = new System.Drawing.Point(93, 291);
            this.paramsButton.Name = "paramsButton";
            this.paramsButton.Size = new System.Drawing.Size(75, 23);
            this.paramsButton.TabIndex = 7;
            this.paramsButton.Text = "Go";
            this.paramsButton.UseVisualStyleBackColor = true;
            this.paramsButton.Click += new System.EventHandler(this.paramsButton_Click);
            // 
            // prefAttachButton
            // 
            this.prefAttachButton.AutoSize = true;
            this.prefAttachButton.Checked = true;
            this.prefAttachButton.Location = new System.Drawing.Point(3, 3);
            this.prefAttachButton.Name = "prefAttachButton";
            this.prefAttachButton.Size = new System.Drawing.Size(135, 17);
            this.prefAttachButton.TabIndex = 15;
            this.prefAttachButton.TabStop = true;
            this.prefAttachButton.Text = "Preferential Attachment";
            this.prefAttachButton.UseVisualStyleBackColor = true;
            this.prefAttachButton.CheckedChanged += new System.EventHandler(this.prefAttachButton_CheckedChanged);
            // 
            // homophilyButton
            // 
            this.homophilyButton.AutoSize = true;
            this.homophilyButton.Location = new System.Drawing.Point(3, 23);
            this.homophilyButton.Name = "homophilyButton";
            this.homophilyButton.Size = new System.Drawing.Size(74, 17);
            this.homophilyButton.TabIndex = 16;
            this.homophilyButton.Text = "Homophily";
            this.homophilyButton.UseVisualStyleBackColor = true;
            this.homophilyButton.CheckedChanged += new System.EventHandler(this.homophilyButton_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.prefAttachButton);
            this.panel1.Controls.Add(this.homophilyButton);
            this.panel1.Location = new System.Drawing.Point(49, 185);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(152, 45);
            this.panel1.TabIndex = 17;
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.CheckOnClick = true;
            this.checkedListBox1.Enabled = false;
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Items.AddRange(new object[] {
            "Enemy",
            "Democracy",
            "Cultism"});
            this.checkedListBox1.Location = new System.Drawing.Point(60, 236);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(132, 49);
            this.checkedListBox1.TabIndex = 18;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(83, 35);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 19;
            this.button1.Text = "File";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            this.button1.Enabled = false;

            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(12, 12);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(118, 17);
            this.radioButton1.TabIndex = 20;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Random Generated";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.randomButton_CheckedChanged);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(12, 38);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(65, 17);
            this.radioButton2.TabIndex = 21;
            this.radioButton2.Text = "UsrInput";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.usrInputButton_CheckedChanged);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(164, 38);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(115, 20);
            this.textBox1.TabIndex = 22;
            this.textBox1.Text = "input";
            // 
            // ABMForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 323);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.checkedListBox1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.paramsButton);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.maxbox);
            this.Controls.Add(this.stepbox);
            this.Controls.Add(this.minbox);
            this.Controls.Add(this.netIdent);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.netText);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nText);
            this.Name = "ABMForm";
            this.Text = "ABM Model";
            this.Load += new System.EventHandler(this.ABMForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion


        private System.Windows.Forms.TextBox nText;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox netText;
        private System.Windows.Forms.TextBox netIdent;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox minbox;
        private System.Windows.Forms.TextBox stepbox;
        private System.Windows.Forms.TextBox maxbox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button paramsButton;
        private System.Windows.Forms.RadioButton prefAttachButton;
        private System.Windows.Forms.RadioButton homophilyButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.TextBox textBox1;
    }
}