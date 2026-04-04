namespace RedBug
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            label1 = new Label();
            label2 = new Label();
            panel4 = new Panel();
            panel5 = new Panel();
            panel1 = new Panel();
            panel2 = new Panel();
            panel3 = new Panel();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.FromArgb(26, 0, 0);
            label1.FlatStyle = FlatStyle.System;
            label1.Font = new Font("Segoe UI", 19.8000011F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.FromArgb(58, 0, 0);
            label1.Location = new Point(390, 9);
            label1.Name = "label1";
            label1.Size = new Size(118, 46);
            label1.TabIndex = 3;
            label1.Text = "HOME";
            label1.Click += label9_Click;
            label1.MouseLeave += label1_MouseLeave;
            label1.MouseHover += label1_MouseHover;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 19.8000011F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.ForeColor = SystemColors.ControlDark;
            label2.Location = new Point(739, 9);
            label2.Name = "label2";
            label2.Size = new Size(97, 46);
            label2.TabIndex = 4;
            label2.Text = "INFO";
            label2.Click += label2_Click;
            label2.MouseLeave += label2_MouseLeave;
            label2.MouseHover += label2_MouseHover;
            // 
            // panel4
            // 
            panel4.BackColor = Color.Transparent;
            panel4.Location = new Point(-1, 75);
            panel4.Name = "panel4";
            panel4.Size = new Size(1250, 573);
            panel4.TabIndex = 5;
            // 
            // panel5
            // 
            panel5.BackColor = Color.FromArgb(34, 34, 34);
            panel5.Location = new Point(12, 58);
            panel5.Name = "panel5";
            panel5.Size = new Size(1220, 1);
            panel5.TabIndex = 6;
            // 
            // panel1
            // 
            panel1.Location = new Point(21, 9);
            panel1.Name = "panel1";
            panel1.Size = new Size(363, 41);
            panel1.TabIndex = 7;
            // 
            // panel2
            // 
            panel2.Location = new Point(874, 9);
            panel2.Name = "panel2";
            panel2.Size = new Size(363, 41);
            panel2.TabIndex = 8;
            // 
            // panel3
            // 
            panel3.Location = new Point(492, 9);
            panel3.Name = "panel3";
            panel3.Size = new Size(241, 41);
            panel3.TabIndex = 9;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(26, 0, 0);
            ClientSize = new Size(1243, 650);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(panel5);
            Controls.Add(panel4);
            Controls.Add(label2);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            Text = "RedBug";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label1;
        private Label label2;
        private Panel panel4;
        private Panel panel5;
        private Panel panel1;
        private Panel panel2;
        private Panel panel3;
    }
}
