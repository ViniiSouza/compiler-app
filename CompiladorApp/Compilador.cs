
namespace CompiladorApp
{
    public partial class Compilador : Form
    {
        public Compilador()
        {
            InitializeComponent();

            DefinirTamanhoMinimo();
        }

        private void DefinirTamanhoMinimo()
        {
            int largura = 910;
            int altura = 600;

            int larguraTotal = largura + (this.Width - this.ClientSize.Width);
            int alturaTotal = altura + (this.Height - this.ClientSize.Height);

            this.MinimumSize = new Size(larguraTotal, alturaTotal);
        }
    }
}
