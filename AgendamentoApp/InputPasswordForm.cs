namespace AgendamentoApp
{
    public partial class InputPasswordForm : Form
    {
        public string SenhaDigitada { get; private set; }
        public InputPasswordForm()
        {
            InitializeComponent();
            textBoxSenha.PasswordChar = '*';
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            this.SenhaDigitada = textBoxSenha.Text;
            this.Close();
        }

        private void buttonCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
