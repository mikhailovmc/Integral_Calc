
namespace VMCurs
{
    partial class MainForm
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
            this.NameLabel = new System.Windows.Forms.Label();
            this.InvToInpLabel = new System.Windows.Forms.Label();
            this.InputBox = new System.Windows.Forms.TextBox();
            this.EqvButton = new System.Windows.Forms.Button();
            this.graphicBox = new System.Windows.Forms.PictureBox();
            this.bBox = new System.Windows.Forms.TextBox();
            this.aBox = new System.Windows.Forms.TextBox();
            this.invToErrLabel = new System.Windows.Forms.Label();
            this.errBox = new System.Windows.Forms.TextBox();
            this.ResLabel = new System.Windows.Forms.Label();
            this.resBox = new System.Windows.Forms.TextBox();
            this.GrafLabel = new System.Windows.Forms.Label();
            this.refButton = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.graphicBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            this.SuspendLayout();
            // 
            // NameLabel
            // 
            this.NameLabel.AutoSize = true;
            this.NameLabel.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.NameLabel.Location = new System.Drawing.Point(12, 9);
            this.NameLabel.Name = "NameLabel";
            this.NameLabel.Size = new System.Drawing.Size(356, 50);
            this.NameLabel.TabIndex = 0;
            this.NameLabel.Text = "Вычисление определенного интеграла \r\nметодом парабол Симпсона";
            // 
            // InvToInpLabel
            // 
            this.InvToInpLabel.AutoSize = true;
            this.InvToInpLabel.Location = new System.Drawing.Point(12, 121);
            this.InvToInpLabel.Name = "InvToInpLabel";
            this.InvToInpLabel.Size = new System.Drawing.Size(106, 15);
            this.InvToInpLabel.TabIndex = 1;
            this.InvToInpLabel.Text = "Введите интеграл:";
            // 
            // InputBox
            // 
            this.InputBox.Location = new System.Drawing.Point(144, 121);
            this.InputBox.Name = "InputBox";
            this.InputBox.Size = new System.Drawing.Size(224, 23);
            this.InputBox.TabIndex = 2;
            this.InputBox.Text = "(5x^2 + sin(x))dx";
            // 
            // EqvButton
            // 
            this.EqvButton.Location = new System.Drawing.Point(374, 121);
            this.EqvButton.Name = "EqvButton";
            this.EqvButton.Size = new System.Drawing.Size(46, 23);
            this.EqvButton.TabIndex = 3;
            this.EqvButton.Text = "=";
            this.EqvButton.UseVisualStyleBackColor = true;
            this.EqvButton.Click += new System.EventHandler(this.EqvButton_Click);
            // 
            // graphicBox
            // 
            this.graphicBox.Location = new System.Drawing.Point(12, 321);
            this.graphicBox.Name = "graphicBox";
            this.graphicBox.Size = new System.Drawing.Size(408, 261);
            this.graphicBox.TabIndex = 14;
            this.graphicBox.TabStop = false;
            // 
            // bBox
            // 
            this.bBox.Location = new System.Drawing.Point(117, 70);
            this.bBox.Name = "bBox";
            this.bBox.Size = new System.Drawing.Size(30, 23);
            this.bBox.TabIndex = 5;
            this.bBox.Text = "1";
            // 
            // aBox
            // 
            this.aBox.Location = new System.Drawing.Point(117, 167);
            this.aBox.Name = "aBox";
            this.aBox.Size = new System.Drawing.Size(30, 23);
            this.aBox.TabIndex = 6;
            this.aBox.Text = "0";
            // 
            // invToErrLabel
            // 
            this.invToErrLabel.AutoSize = true;
            this.invToErrLabel.Location = new System.Drawing.Point(12, 207);
            this.invToErrLabel.Name = "invToErrLabel";
            this.invToErrLabel.Size = new System.Drawing.Size(130, 15);
            this.invToErrLabel.TabIndex = 7;
            this.invToErrLabel.Text = "Введите погрешность:";
            // 
            // errBox
            // 
            this.errBox.Location = new System.Drawing.Point(149, 207);
            this.errBox.Name = "errBox";
            this.errBox.Size = new System.Drawing.Size(100, 23);
            this.errBox.TabIndex = 8;
            this.errBox.Text = "0,00000003";
            // 
            // ResLabel
            // 
            this.ResLabel.AutoSize = true;
            this.ResLabel.Location = new System.Drawing.Point(12, 251);
            this.ResLabel.Name = "ResLabel";
            this.ResLabel.Size = new System.Drawing.Size(63, 15);
            this.ResLabel.TabIndex = 9;
            this.ResLabel.Text = "Результат:";
            // 
            // resBox
            // 
            this.resBox.Location = new System.Drawing.Point(81, 251);
            this.resBox.Name = "resBox";
            this.resBox.Size = new System.Drawing.Size(287, 23);
            this.resBox.TabIndex = 10;
            // 
            // GrafLabel
            // 
            this.GrafLabel.AutoSize = true;
            this.GrafLabel.Location = new System.Drawing.Point(149, 302);
            this.GrafLabel.Name = "GrafLabel";
            this.GrafLabel.Size = new System.Drawing.Size(126, 15);
            this.GrafLabel.TabIndex = 11;
            this.GrafLabel.Text = "График погрешности";
            // 
            // refButton
            // 
            this.refButton.Location = new System.Drawing.Point(174, 588);
            this.refButton.Name = "refButton";
            this.refButton.Size = new System.Drawing.Size(75, 23);
            this.refButton.TabIndex = 12;
            this.refButton.Text = "Справка";
            this.refButton.UseVisualStyleBackColor = true;
            this.refButton.Click += new System.EventHandler(this.refButton_Click);
            // 
            // textBox1
            // 
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Font = new System.Drawing.Font("Segoe UI", 26F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBox1.Location = new System.Drawing.Point(117, 99);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(25, 62);
            this.textBox1.TabIndex = 15;
            this.textBox1.Text = "∫";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(12, 292);
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(43, 23);
            this.numericUpDown1.TabIndex = 16;
            this.numericUpDown1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.Visible = false;
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Location = new System.Drawing.Point(374, 589);
            this.numericUpDown2.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown2.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(43, 23);
            this.numericUpDown2.TabIndex = 17;
            this.numericUpDown2.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown2.Visible = false;
            this.numericUpDown2.ValueChanged += new System.EventHandler(this.numericUpDown2_ValueChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(432, 623);
            this.Controls.Add(this.numericUpDown2);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.refButton);
            this.Controls.Add(this.GrafLabel);
            this.Controls.Add(this.resBox);
            this.Controls.Add(this.ResLabel);
            this.Controls.Add(this.errBox);
            this.Controls.Add(this.invToErrLabel);
            this.Controls.Add(this.aBox);
            this.Controls.Add(this.bBox);
            this.Controls.Add(this.graphicBox);
            this.Controls.Add(this.EqvButton);
            this.Controls.Add(this.InputBox);
            this.Controls.Add(this.InvToInpLabel);
            this.Controls.Add(this.NameLabel);
            this.Name = "MainForm";
            this.Text = "Калькулятор интегралов";
            ((System.ComponentModel.ISupportInitialize)(this.graphicBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label NameLabel;
        private System.Windows.Forms.Label InvToInpLabel;
        private System.Windows.Forms.TextBox InputBox;
        private System.Windows.Forms.Button EqvButton;
        private System.Windows.Forms.PictureBox graphicBox;
        private System.Windows.Forms.TextBox bBox;
        private System.Windows.Forms.TextBox aBox;
        private System.Windows.Forms.Label invToErrLabel;
        private System.Windows.Forms.TextBox errBox;
        private System.Windows.Forms.Label ResLabel;
        private System.Windows.Forms.TextBox resBox;
        private System.Windows.Forms.Label GrafLabel;
        private System.Windows.Forms.Button refButton;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
    }
}

