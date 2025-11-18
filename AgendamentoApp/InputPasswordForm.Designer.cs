namespace AgendamentoApp
{
    partial class InputPasswordForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InputPasswordForm));
            labelSenha = new Label();
            textBoxSenha = new TextBox();
            buttonOk = new Button();
            buttonCancelar = new Button();
            SuspendLayout();
            // 
            // labelSenha
            // 
            labelSenha.AutoSize = true;
            labelSenha.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelSenha.Location = new Point(47, 63);
            labelSenha.Name = "labelSenha";
            labelSenha.Size = new Size(190, 31);
            labelSenha.TabIndex = 0;
            labelSenha.Text = "Digite sua senha";
            // 
            // textBoxSenha
            // 
            textBoxSenha.Location = new Point(47, 130);
            textBoxSenha.Name = "textBoxSenha";
            textBoxSenha.PasswordChar = '*';
            textBoxSenha.Size = new Size(190, 27);
            textBoxSenha.TabIndex = 1;
            // 
            // buttonOk
            // 
            buttonOk.DialogResult = DialogResult.OK;
            buttonOk.Location = new Point(12, 242);
            buttonOk.Name = "buttonOk";
            buttonOk.Size = new Size(94, 29);
            buttonOk.TabIndex = 2;
            buttonOk.Text = "OK";
            buttonOk.UseVisualStyleBackColor = true;
            buttonOk.Click += buttonOk_Click;
            // 
            // buttonCancelar
            // 
            buttonCancelar.DialogResult = DialogResult.Cancel;
            buttonCancelar.Location = new Point(197, 242);
            buttonCancelar.Name = "buttonCancelar";
            buttonCancelar.Size = new Size(94, 29);
            buttonCancelar.TabIndex = 3;
            buttonCancelar.Text = "Cancelar";
            buttonCancelar.UseVisualStyleBackColor = true;
            buttonCancelar.Click += buttonCancelar_Click;
            // 
            // InputPasswordForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(303, 283);
            Controls.Add(buttonCancelar);
            Controls.Add(buttonOk);
            Controls.Add(textBoxSenha);
            Controls.Add(labelSenha);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "InputPasswordForm";
            Text = "InputPasswordForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelSenha;
        private TextBox textBoxSenha;
        private Button buttonOk;
        private Button buttonCancelar;
    }
}