using ApiAgendamento.Models;
using System.Net.Http.Json;

namespace AgendamentoApp
{
    public partial class MenuForms : Form
    {

        public MenuForms()
        {
            InitializeComponent();
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://192.168.3.254:5000");
            HoraInicio = DateTime.Now;
            CarregarSala();
            ConfigurarTimer();
        }

        private System.Windows.Forms.Timer TimerGrid;

        List<SalaItem> SalaSelecionada = new List<SalaItem>
        {
            new SalaItem {Valor = Sala.Sala_12, ValorNome = "SALA 12ª"},
            new SalaItem {Valor = Sala.Sala_13, ValorNome = "SALA 13º"},
        };

        private void CarregarSala()
        {
            comboBoxSala.DataSource = SalaSelecionada;
            comboBoxSala.DisplayMember = "ValorNome";
            comboBoxSala.ValueMember = "Valor";
            comboBoxSala.SelectedIndex = -1;
        }


        private void ConfigurarTimer()
        {
            timerGrid = new System.Windows.Forms.Timer();
            timerGrid.Interval = 240000;
            timerGrid.Tick += timerGrid_Tick;
            timerGrid.Start();
        }

        private async void timerGrid_Tick(object sender, EventArgs e)
        {
            if (idSelecionado == 0 && (!string.IsNullOrEmpty(textBoxDescricao.Text) || !string.IsNullOrEmpty(textBoxResponsavel.Text)))
            {
                return;
            }

            await CarregarAgendamentosAsync(true);
        }

        private void LimparDados()
        {
            textBoxDescricao.Clear();
            textBoxResponsavel.Clear();
            textBoxObservacao.Clear();
            dateTimePickerInicio.Value = DateTime.Now;
            dateTimePickerFim.Value = DateTime.Now;
            idSelecionado = 0;
            comboBoxSala.SelectedIndex = -1;
        }


        public async Task CarregarAgendamentosAsync(bool silencioso = false)
        {
            DateTime TimerAtual = DateTime.Now;

            Cursor.Current = Cursors.WaitCursor;
            buttonSalvar.Enabled = false;

            try
            {
                if (!silencioso)
                {
                    Cursor.Current = Cursors.WaitCursor;
                    buttonSalvar.Enabled = false;
                }

                string dataInicio = DateTime.Now.ToString("yyyy-MM-dd");
                string dataFim = DateTime.Now.AddDays(30).ToString("yyyy-MM-dd");


                var ListaAgendamentos = await httpClient.GetFromJsonAsync<List<Agendamento>>($"/api/Agendamentoes/filtro?inicio={dataInicio}&fim={dataFim}");

                dataGridViewAgendamentos.DataSource = ListaAgendamentos;

                dataGridViewAgendamentos.Columns["salaSelecionada"].DisplayIndex = 0;

                dataGridViewAgendamentos.Columns["DatahorarioInicio"].HeaderText = "Início";
                dataGridViewAgendamentos.Columns["DataHoraFim"].HeaderText = "Fim";
                dataGridViewAgendamentos.Columns["Responsavel"].HeaderText = "Responsável";
                dataGridViewAgendamentos.Columns["Titulo"].HeaderText = "Título";
                dataGridViewAgendamentos.Columns["Observacoes"].HeaderText = "Observações";
                dataGridViewAgendamentos.Columns["salaSelecionada"].HeaderText = "Sala de Reunião";

                dataGridViewAgendamentos.Columns["Titulo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewAgendamentos.Columns["Observacoes"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewAgendamentos.Columns["Id"].Visible = false;




            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possível carregar os agendamentos: " + ex.Message);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                buttonSalvar.Enabled = true;



            }

        }



        private readonly HttpClient httpClient;

        private int idSelecionado = 0;

        private DateTime HoraInicio;



        private async void buttonSalvar_Click(object sender, EventArgs e)
        {
            if (dateTimePickerInicio.Value < DateTime.Now.AddMinutes(-1))
            {
                MessageBox.Show("Não é possível agendar em uma data ou hora no passado.",
                        "Erro de Validação", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (dateTimePickerFim.Value <= dateTimePickerInicio.Value)
            {
                MessageBox.Show("A data/hora final deve ser *depois* da data/hora inicial.",
                                "Erro de Validação", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (comboBoxSala.SelectedItem == null)
            {
                MessageBox.Show("È necessário selecionar qual SALA de reunião você deseja marcar", "Sala não selecionada!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (textBoxResponsavel.Text != null && textBoxObservacao.Text != null && textBoxDescricao.Text != null)
            {
                buttonSalvar.Enabled = false;
                var sala = (Sala)comboBoxSala.SelectedValue;
                var novoAgendamento = new Agendamento
                {
                    Titulo = textBoxDescricao.Text.Trim(),
                    Responsavel = textBoxResponsavel.Text.Trim(),
                    Observacoes = textBoxObservacao.Text.Trim(),
                    DatahorarioInicio = dateTimePickerInicio.Value,
                    DataHoraFim = dateTimePickerFim.Value,
                    Id = idSelecionado,
                    salaSelecionada = sala

                };

                try
                {
                    HttpResponseMessage resposta;


                    if (idSelecionado == 0)
                    {
                        resposta = await httpClient.PostAsJsonAsync("/api/Agendamentoes", novoAgendamento);
                    }
                    else
                    {
                        resposta = await httpClient.PutAsJsonAsync($"/api/Agendamentoes/{idSelecionado}", novoAgendamento);
                    }
                    if (resposta.IsSuccessStatusCode)
                    {
                        string feedback = (idSelecionado == 0) ? "Agendamento salvo com Sucesso!" : "Agendamento Atualizado com sucesso!";
                        MessageBox.Show(feedback);

                        LimparDados();
                        await CarregarAgendamentosAsync();
                    }
                    else
                    {
                        string erro = await resposta.Content.ReadAsStringAsync();
                        MessageBox.Show("Erro ao salvar: " + erro);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro de conexão: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Preencha todos os campos para registrar o atendimento");
            }

            buttonSalvar.Enabled = true;
            LimparDados();

            buttonSalvar.Text = "Salvar";
        }

        private void buttonNovo_Click(object sender, EventArgs e)
        {
            tabControlAgendamentos.SelectTab(tabPageCadastrar);
            LimparDados();
        }

        private async void MenuForms_Load(object sender, EventArgs e)
        {
            await CarregarAgendamentosAsync();

        }

        private void dataGridViewAgendamentos_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewAgendamentos.SelectedRows.Count > 0)
            {
                DataGridViewRow linhaSelecionada = dataGridViewAgendamentos.SelectedRows[0];

                idSelecionado = Convert.ToInt32(linhaSelecionada.Cells["Id"].Value);

                buttonExcluir.Enabled = true;
            }
            else
            {
                idSelecionado = 0;
                buttonExcluir.Enabled = false;
            }
        }

        private async void buttonExcluir_Click(object sender, EventArgs e)
        {
            string senhaCorreta = "senha";
            using (InputPasswordForm formsenha = new InputPasswordForm())
            {

                if (formsenha.ShowDialog() == DialogResult.OK)
                {

                    string senhaDigitada = formsenha.SenhaDigitada;

                    if (senhaDigitada == senhaCorreta)
                    {
                        MessageBox.Show("Senha correta! Ação Liberada.");

                        if (idSelecionado == 0)
                        {
                            MessageBox.Show("Por favor, selecione um agendamento na tabela para deletar");
                            return;
                        }

                        DialogResult confirmacao = MessageBox.Show("Tem certeza que deseja deletar esse agendamento?", "Confirmar Exclusão", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                        if (confirmacao == DialogResult.Yes)
                        {
                            try
                            {
                                HttpResponseMessage resposta = await httpClient.DeleteAsync($"/api/agendamentoes/{idSelecionado}");

                                if (resposta.IsSuccessStatusCode)
                                {
                                    MessageBox.Show("Agendamento apagado com Sucesso");
                                    await CarregarAgendamentosAsync();

                                }
                                else
                                {
                                    MessageBox.Show("Falha ao deletar o agendamento.");
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Erro de conexão: " + ex.Message);
                            }

                        }

                        LimparDados();

                    }
                    else
                    {
                        MessageBox.Show("Senha incorreta!", "Acesso Negado",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Operação cancelada.");
                }
            }
        }
        private void dataGridViewAgendamentos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow linhaSleceionada = dataGridViewAgendamentos.Rows[e.RowIndex];
                SalaItem sala = (SalaItem)comboBoxSala.SelectedItem;

                Agendamento agendamento = linhaSleceionada.DataBoundItem as Agendamento;

                if (agendamento != null)
                {
                    textBoxDescricao.Text = agendamento.Titulo;
                    textBoxObservacao.Text = agendamento.Observacoes;
                    textBoxResponsavel.Text = agendamento.Responsavel;
                    dateTimePickerFim.Value = agendamento.DataHoraFim;
                    dateTimePickerInicio.Value = agendamento.DatahorarioInicio;
                    comboBoxSala.SelectedItem = SalaSelecionada.FirstOrDefault(s => s.Valor == agendamento.salaSelecionada);
                    idSelecionado = agendamento.Id;

                    buttonSalvar.Text = "Atualizar";
                }
  

            }
        }

        private void dataGridViewAgendamentos_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridViewAgendamentos.Columns[e.ColumnIndex].Name == "salaSelecionada")
            {
                Sala ValorSala = (Sala)e.Value;

                if (ValorSala == Sala.Sala_12)
                {
                    e.Value = "SALA 12ª";
                    e.FormattingApplied = true;
                }

                else if (ValorSala == Sala.Sala_13)
                {
                    e.Value = "SALA 13ª";
                    e.FormattingApplied = true;
                }
            }

            if (this.dataGridViewAgendamentos.Columns[e.ColumnIndex].Name == "salaSelecionada")
            {
                if(e.Value != null && e.Value.ToString() == "SALA 13ª")
                {
                    dataGridViewAgendamentos.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightSkyBlue;
                    dataGridViewAgendamentos.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Black;
                }
            }
        }

        private void textBoxResponsavel_TextChanged(object sender, EventArgs e)
        {

        }
    }

}
