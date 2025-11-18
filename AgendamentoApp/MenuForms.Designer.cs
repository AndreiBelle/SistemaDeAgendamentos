namespace AgendamentoApp
{
    partial class MenuForms
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MenuForms));
            tabControlAgendamentos = new TabControl();
            tabPageConsultar = new TabPage();
            buttonEditar = new Button();
            textBoxObservacao = new TextBox();
            labelObservacao = new Label();
            dataGridViewAgendamentos = new DataGridView();
            buttonExcluir = new Button();
            buttonSalvar = new Button();
            dateTimePickerFim = new DateTimePicker();
            dateTimePickerInicio = new DateTimePicker();
            labelFim = new Label();
            labelInicio = new Label();
            textBoxDescricao = new TextBox();
            labelResponsavel = new Label();
            textBoxResponsavel = new TextBox();
            labelSalaDeReunião = new Label();
            labelDescricao = new Label();
            tabPageCadastrar = new TabPage();
            tabControlAgendamentos.SuspendLayout();
            tabPageConsultar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewAgendamentos).BeginInit();
            SuspendLayout();
            // 
            // tabControlAgendamentos
            // 
            tabControlAgendamentos.Controls.Add(tabPageConsultar);
            tabControlAgendamentos.Controls.Add(tabPageCadastrar);
            tabControlAgendamentos.Location = new Point(1, -26);
            tabControlAgendamentos.Name = "tabControlAgendamentos";
            tabControlAgendamentos.SelectedIndex = 0;
            tabControlAgendamentos.Size = new Size(997, 742);
            tabControlAgendamentos.TabIndex = 2;
            // 
            // tabPageConsultar
            // 
            tabPageConsultar.Controls.Add(buttonEditar);
            tabPageConsultar.Controls.Add(textBoxObservacao);
            tabPageConsultar.Controls.Add(labelObservacao);
            tabPageConsultar.Controls.Add(dataGridViewAgendamentos);
            tabPageConsultar.Controls.Add(buttonExcluir);
            tabPageConsultar.Controls.Add(buttonSalvar);
            tabPageConsultar.Controls.Add(dateTimePickerFim);
            tabPageConsultar.Controls.Add(dateTimePickerInicio);
            tabPageConsultar.Controls.Add(labelFim);
            tabPageConsultar.Controls.Add(labelInicio);
            tabPageConsultar.Controls.Add(textBoxDescricao);
            tabPageConsultar.Controls.Add(labelResponsavel);
            tabPageConsultar.Controls.Add(textBoxResponsavel);
            tabPageConsultar.Controls.Add(labelSalaDeReunião);
            tabPageConsultar.Controls.Add(labelDescricao);
            tabPageConsultar.Location = new Point(4, 29);
            tabPageConsultar.Name = "tabPageConsultar";
            tabPageConsultar.Padding = new Padding(3);
            tabPageConsultar.Size = new Size(989, 709);
            tabPageConsultar.TabIndex = 0;
            tabPageConsultar.Text = "tabPageConsulta";
            tabPageConsultar.UseVisualStyleBackColor = true;
            // 
            // buttonEditar
            // 
            buttonEditar.Enabled = false;
            buttonEditar.Location = new Point(372, 296);
            buttonEditar.Name = "buttonEditar";
            buttonEditar.Size = new Size(198, 42);
            buttonEditar.TabIndex = 45;
            buttonEditar.Text = "Editar";
            buttonEditar.UseVisualStyleBackColor = true;
            buttonEditar.Visible = false;
            // 
            // textBoxObservacao
            // 
            textBoxObservacao.Location = new Point(7, 223);
            textBoxObservacao.Multiline = true;
            textBoxObservacao.Name = "textBoxObservacao";
            textBoxObservacao.PlaceholderText = "Adicionar aqui caso precise de café, aperitivos, etc...";
            textBoxObservacao.Size = new Size(399, 34);
            textBoxObservacao.TabIndex = 44;
            // 
            // labelObservacao
            // 
            labelObservacao.AutoSize = true;
            labelObservacao.Location = new Point(7, 200);
            labelObservacao.Name = "labelObservacao";
            labelObservacao.Size = new Size(87, 20);
            labelObservacao.TabIndex = 43;
            labelObservacao.Text = "Observação";
            // 
            // dataGridViewAgendamentos
            // 
            dataGridViewAgendamentos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewAgendamentos.Location = new Point(0, 374);
            dataGridViewAgendamentos.Name = "dataGridViewAgendamentos";
            dataGridViewAgendamentos.RowHeadersWidth = 51;
            dataGridViewAgendamentos.Size = new Size(986, 335);
            dataGridViewAgendamentos.TabIndex = 42;
            dataGridViewAgendamentos.CellDoubleClick += dataGridViewAgendamentos_CellDoubleClick;
            dataGridViewAgendamentos.SelectionChanged += dataGridViewAgendamentos_SelectionChanged;
            // 
            // buttonExcluir
            // 
            buttonExcluir.Enabled = false;
            buttonExcluir.Location = new Point(779, 296);
            buttonExcluir.Name = "buttonExcluir";
            buttonExcluir.Size = new Size(198, 42);
            buttonExcluir.TabIndex = 41;
            buttonExcluir.Text = "Excluir Selecionado";
            buttonExcluir.UseVisualStyleBackColor = true;
            buttonExcluir.Click += buttonExcluir_Click;
            // 
            // buttonSalvar
            // 
            buttonSalvar.Location = new Point(7, 296);
            buttonSalvar.Name = "buttonSalvar";
            buttonSalvar.Size = new Size(138, 42);
            buttonSalvar.TabIndex = 40;
            buttonSalvar.Text = "Salvar";
            buttonSalvar.UseVisualStyleBackColor = true;
            buttonSalvar.Click += buttonSalvar_Click;
            // 
            // dateTimePickerFim
            // 
            dateTimePickerFim.CustomFormat = "dd/MM/yyyy HH:mm";
            dateTimePickerFim.Format = DateTimePickerFormat.Custom;
            dateTimePickerFim.Location = new Point(531, 155);
            dateTimePickerFim.Name = "dateTimePickerFim";
            dateTimePickerFim.Size = new Size(160, 27);
            dateTimePickerFim.TabIndex = 39;
            // 
            // dateTimePickerInicio
            // 
            dateTimePickerInicio.CustomFormat = "dd/MM/yyyy HH:mm";
            dateTimePickerInicio.Format = DateTimePickerFormat.Custom;
            dateTimePickerInicio.Location = new Point(531, 82);
            dateTimePickerInicio.Name = "dateTimePickerInicio";
            dateTimePickerInicio.Size = new Size(160, 27);
            dateTimePickerInicio.TabIndex = 38;
            // 
            // labelFim
            // 
            labelFim.AutoSize = true;
            labelFim.Location = new Point(531, 132);
            labelFim.Name = "labelFim";
            labelFim.Size = new Size(112, 20);
            labelFim.TabIndex = 37;
            labelFim.Text = "Fim da Reunião";
            // 
            // labelInicio
            // 
            labelInicio.AutoSize = true;
            labelInicio.Location = new Point(531, 59);
            labelInicio.Name = "labelInicio";
            labelInicio.Size = new Size(124, 20);
            labelInicio.TabIndex = 36;
            labelInicio.Text = "Inicio da Reunião";
            // 
            // textBoxDescricao
            // 
            textBoxDescricao.Location = new Point(7, 148);
            textBoxDescricao.Multiline = true;
            textBoxDescricao.Name = "textBoxDescricao";
            textBoxDescricao.Size = new Size(284, 34);
            textBoxDescricao.TabIndex = 35;
            // 
            // labelResponsavel
            // 
            labelResponsavel.AutoSize = true;
            labelResponsavel.Location = new Point(7, 59);
            labelResponsavel.Name = "labelResponsavel";
            labelResponsavel.Size = new Size(91, 20);
            labelResponsavel.TabIndex = 34;
            labelResponsavel.Text = "Responsável";
            // 
            // textBoxResponsavel
            // 
            textBoxResponsavel.Location = new Point(7, 82);
            textBoxResponsavel.Name = "textBoxResponsavel";
            textBoxResponsavel.Size = new Size(284, 27);
            textBoxResponsavel.TabIndex = 33;
            // 
            // labelSalaDeReunião
            // 
            labelSalaDeReunião.AutoSize = true;
            labelSalaDeReunião.Font = new Font("Segoe UI Emoji", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelSalaDeReunião.Location = new Point(264, 6);
            labelSalaDeReunião.Name = "labelSalaDeReunião";
            labelSalaDeReunião.Size = new Size(438, 37);
            labelSalaDeReunião.TabIndex = 31;
            labelSalaDeReunião.Text = "Agendamentos Sala de Reunião";
            // 
            // labelDescricao
            // 
            labelDescricao.AutoSize = true;
            labelDescricao.Location = new Point(7, 125);
            labelDescricao.Name = "labelDescricao";
            labelDescricao.Size = new Size(153, 20);
            labelDescricao.TabIndex = 32;
            labelDescricao.Text = "Descrição da Reunião";
            // 
            // tabPageCadastrar
            // 
            tabPageCadastrar.Location = new Point(4, 29);
            tabPageCadastrar.Name = "tabPageCadastrar";
            tabPageCadastrar.Padding = new Padding(3);
            tabPageCadastrar.Size = new Size(989, 709);
            tabPageCadastrar.TabIndex = 1;
            tabPageCadastrar.Text = "tabPage2";
            tabPageCadastrar.UseVisualStyleBackColor = true;
            // 
            // MenuForms
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(994, 715);
            Controls.Add(tabControlAgendamentos);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "MenuForms";
            Text = "Agendamentos da Sala de Reunião";
            Load += MenuForms_Load;
            tabControlAgendamentos.ResumeLayout(false);
            tabPageConsultar.ResumeLayout(false);
            tabPageConsultar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewAgendamentos).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private TabControl tabControlAgendamentos;
        private TabPage tabPageConsultar;
        private TabPage tabPageCadastrar;
        private Button buttonExcluir;
        private Button buttonSalvar;
        private DateTimePicker dateTimePickerFim;
        private DateTimePicker dateTimePickerInicio;
        private Label labelFim;
        private Label labelInicio;
        private TextBox textBoxDescricao;
        private Label labelResponsavel;
        private TextBox textBoxResponsavel;
        private Label labelSalaDeReunião;
        private Label labelDescricao;
        private DataGridView dataGridViewAgendamentos;
        private Label labelObservacao;
        private TextBox textBoxObservacao;
        private Button buttonEditar;
    }
}
