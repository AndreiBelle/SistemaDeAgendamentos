using ApiAgendamento.Models;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace AgendamentoApp
{
    public partial class MenuForms : Form
    {


        private void LimparDados()
        {
            textBoxDescricao.Clear();
            textBoxResponsavel.Clear();
            textBoxObservacao.Clear();
            dateTimePickerInicio.Value = DateTime.Now;
            dateTimePickerFim.Value = DateTime.Now;
            idSelecionado = 0;
        }

        private async Task CarregarAgendamentosAsync()
        {

            try
            {
         
                string dataInicio = DateTime.Now.ToString("yyyy-MM-dd");
                string dataFim = DateTime.Now.AddDays(30).ToString("yyyy-MM-dd");

      
                var ListaAgendamentos = await httpClient.GetFromJsonAsync<List<Agendamento>>($"/api/Agendamentoes/filtro?inicio={dataInicio}&fim={dataFim}");

                dataGridViewAgendamentos.DataSource = ListaAgendamentos;

                dataGridViewAgendamentos.Columns["DatahorarioInicio"].HeaderText = "Início";
                dataGridViewAgendamentos.Columns["DataHoraFim"].HeaderText = "Fim";
                dataGridViewAgendamentos.Columns["Responsavel"].HeaderText = "Responsável";
                dataGridViewAgendamentos.Columns["Titulo"].HeaderText = "Título";
                dataGridViewAgendamentos.Columns["Observacoes"].HeaderText = "Observações";

                dataGridViewAgendamentos.Columns["Titulo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewAgendamentos.Columns["Observacoes"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewAgendamentos.Columns["Id"].Visible = false;


            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possível carregar os agendamentos: " + ex.Message);
            }
        }

        private readonly HttpClient httpClient;

        private int idSelecionado = 0;

        public MenuForms()
        {
            InitializeComponent();
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://192.168.3.254:5000");
        }

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

            if (textBoxResponsavel.Text != null && textBoxObservacao.Text != null && textBoxDescricao.Text != null)
            {
                var novoAgendamento = new Agendamento
                {
                    Titulo = textBoxDescricao.Text.Trim(),
                    Responsavel = textBoxResponsavel.Text.Trim(),
                    Observacoes = textBoxObservacao.Text.Trim(),
                    DatahorarioInicio = dateTimePickerInicio.Value,
                    DataHoraFim = dateTimePickerFim.Value,
                    Id = idSelecionado
                    
                };

                try
                {
                    HttpResponseMessage resposta;

                    if(idSelecionado == 0)
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

            LimparDados();
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
            string senhaCorreta = "Admin#TFL123";
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
        }   }
                    private void dataGridViewAgendamentos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
                    {
                        if (e.RowIndex >= 0)
                        {
                            DataGridViewRow linhaSleceionada = dataGridViewAgendamentos.Rows[e.RowIndex];

                            Agendamento agendamento = linhaSleceionada.DataBoundItem as Agendamento;

                            if (agendamento != null)
                            {
                                textBoxDescricao.Text = agendamento.Titulo;
                                textBoxObservacao.Text = agendamento.Observacoes;
                                textBoxResponsavel.Text = agendamento.Responsavel;
                                dateTimePickerFim.Value = agendamento.DataHoraFim;
                                dateTimePickerInicio.Value = agendamento.DatahorarioInicio;

                                idSelecionado = agendamento.Id;
                            }
                        }
                    }
    }
}
