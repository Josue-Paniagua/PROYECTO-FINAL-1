namespace PROYECTOFINAL1
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
            lblTema = new Label();
            lblResultado = new Label();
            txtPrompt = new TextBox();
            btnConsultar = new Button();
            rtbResultado = new RichTextBox();
            btnAbrirWord = new Button();
            btnAbrirPPT = new Button();
            btnExportar = new Button();
            SuspendLayout();
            // 
            // lblTema
            // 
            lblTema.AutoSize = true;
            lblTema.BackColor = Color.DarkCyan;
            lblTema.BorderStyle = BorderStyle.FixedSingle;
            lblTema.Font = new Font("Segoe UI", 12F);
            lblTema.Location = new Point(24, 32);
            lblTema.Name = "lblTema";
            lblTema.Size = new Size(205, 23);
            lblTema.TabIndex = 0;
            lblTema.Text = "Ingrese su pregunta o tema:";
            // 
            // lblResultado
            // 
            lblResultado.AutoSize = true;
            lblResultado.BackColor = Color.DarkGoldenrod;
            lblResultado.BorderStyle = BorderStyle.FixedSingle;
            lblResultado.Font = new Font("Segoe UI", 12F);
            lblResultado.Location = new Point(44, 179);
            lblResultado.Name = "lblResultado";
            lblResultado.Size = new Size(84, 23);
            lblResultado.TabIndex = 1;
            lblResultado.Text = "Resultado:";
            // 
            // txtPrompt
            // 
            txtPrompt.BackColor = SystemColors.ActiveCaption;
            txtPrompt.Cursor = Cursors.IBeam;
            txtPrompt.Location = new Point(235, 21);
            txtPrompt.Multiline = true;
            txtPrompt.Name = "txtPrompt";
            txtPrompt.Size = new Size(250, 72);
            txtPrompt.TabIndex = 2;
            txtPrompt.TextChanged += txtPrompt_TextChanged;
            // 
            // btnConsultar
            // 
            btnConsultar.BackColor = Color.DarkCyan;
            btnConsultar.Location = new Point(573, 32);
            btnConsultar.Name = "btnConsultar";
            btnConsultar.Size = new Size(93, 62);
            btnConsultar.TabIndex = 3;
            btnConsultar.Text = "Consultar AI";
            btnConsultar.UseVisualStyleBackColor = false;
            btnConsultar.Click += btnConsultar_Click;
            // 
            // rtbResultado
            // 
            rtbResultado.BackColor = Color.DarkSlateBlue;
            rtbResultado.Cursor = Cursors.IBeam;
            rtbResultado.Location = new Point(109, 202);
            rtbResultado.Name = "rtbResultado";
            rtbResultado.ReadOnly = true;
            rtbResultado.Size = new Size(345, 183);
            rtbResultado.TabIndex = 4;
            rtbResultado.Text = "";
            // 
            // btnAbrirWord
            // 
            btnAbrirWord.BackColor = SystemColors.HotTrack;
            btnAbrirWord.FlatStyle = FlatStyle.Popup;
            btnAbrirWord.Font = new Font("Monotype Corsiva", 15F);
            btnAbrirWord.ForeColor = SystemColors.ButtonFace;
            btnAbrirWord.Location = new Point(505, 202);
            btnAbrirWord.Name = "btnAbrirWord";
            btnAbrirWord.Size = new Size(120, 66);
            btnAbrirWord.TabIndex = 5;
            btnAbrirWord.Text = "Crear word";
            btnAbrirWord.UseVisualStyleBackColor = false;
            btnAbrirWord.Click += btnAbrirWord_Click;
            // 
            // btnAbrirPPT
            // 
            btnAbrirPPT.BackColor = Color.DarkOrange;
            btnAbrirPPT.Font = new Font("Segoe Script", 14F);
            btnAbrirPPT.ForeColor = Color.Black;
            btnAbrirPPT.Location = new Point(505, 293);
            btnAbrirPPT.Name = "btnAbrirPPT";
            btnAbrirPPT.Size = new Size(120, 79);
            btnAbrirPPT.TabIndex = 6;
            btnAbrirPPT.Text = "AbrirPower Point";
            btnAbrirPPT.UseVisualStyleBackColor = false;
            btnAbrirPPT.Click += btnAbrirPPT_Click_1;
            // 
            // btnExportar
            // 
            btnExportar.BackColor = Color.Olive;
            btnExportar.FlatStyle = FlatStyle.Popup;
            btnExportar.Font = new Font("Monotype Corsiva", 15F);
            btnExportar.ForeColor = SystemColors.ButtonFace;
            btnExportar.Location = new Point(668, 252);
            btnExportar.Name = "btnExportar";
            btnExportar.Size = new Size(120, 66);
            btnExportar.TabIndex = 7;
            btnExportar.Text = "Crear Carpeta ";
            btnExportar.UseVisualStyleBackColor = false;
            btnExportar.Click += btnExportar_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnExportar);
            Controls.Add(btnAbrirPPT);
            Controls.Add(btnAbrirWord);
            Controls.Add(rtbResultado);
            Controls.Add(btnConsultar);
            Controls.Add(txtPrompt);
            Controls.Add(lblResultado);
            Controls.Add(lblTema);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTema;
        private Label lblResultado;
        private TextBox txtPrompt;
        private Button btnConsultar;
        private RichTextBox rtbResultado;
        private Button btnAbrirWord;
        private Button btnAbrirPPT;
        private Button btnExportar;
    }
}
