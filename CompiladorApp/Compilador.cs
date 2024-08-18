
using System.IO;
using System;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Net;
using System.ComponentModel;

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

        private void newButton_Click(object sender, EventArgs e)
        {
            //  Quando o botão novo for pressionado (ou a tecla de atalho correspondente), a ação deve ser:
            // limpar o editor1
            // , limpar a área para mensagens2
            // e limpar a barra de status3

        }

        private void openButton_Click(object sender, EventArgs e)
        {
            //Quando o botão abrir for pressionado(ou a tecla de atalho correspondente), a ação deve ser:
            //possibilitar a seleção de pasta / arquivo, carregar o arquivo selecionado no editor, limpar a área para
            //mensagens2
            // e atualizar a barra de status4
            //.Caso nenhum arquivo seja selecionado, o estado da interface,
            //anterior ao botão abrir ser pressionado, deve ser mantido, ou seja, o editor deve manter o texto que está sendo
            //editado, a área para mensagens não deve ser limpa e a barra de status não deve ser atualizada.Devem ser
            //abertos arquivos texto (extensão.txt) compatíveis com o Notepad.

        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            //Quando o botão salvar for pressionado(ou a tecla de atalho correspondente), a ação deve ser: (1)
            //se o arquivo for novo, possibilitar a seleção de pasta / nome do arquivo, salvar o conteúdo do editor no arquivo
            //com o nome e no local selecionados, manter no editor o texto que está sendo editado, limpar a área para
            //mensagens2
            // e atualizar a barra de status4
            //; (2) caso contrário, salvar as alterações do arquivo editado, manter
            //no editor o texto que está sendo editado, limpar a área para mensagens2
            // e manter a barra de status5
            //.Devem
            //ser salvos arquivos texto(extensão.txt) compatíveis com o Notepad. 
        }

        private void copyButton_Click(object sender, EventArgs e)
        {
            //A ação associada a cada um dos botões de edição e as teclas de atalho associadas(copiar,
            //colar, recortar) é a mesma encontrada nos editores de texto convencionais
        }

        private void pasteButton_Click(object sender, EventArgs e)
        {
            //A ação associada a cada um dos botões de edição e as teclas de atalho associadas(copiar,
            //colar, recortar) é a mesma encontrada nos editores de texto convencionais
        }

        private void cutButton_Click(object sender, EventArgs e)
        {
            //A ação associada a cada um dos botões de edição e as teclas de atalho associadas(copiar,
            //colar, recortar) é a mesma encontrada nos editores de texto convencionais
        }

        private void compileButton_Click(object sender, EventArgs e)
        {
            //Quando o botão compilar for pressionado(ou a tecla de atalho correspondente), a ação deve ser:
            //apresentar mensagem(compilação de programas ainda não foi implementada) na área para mensagens
        }

        private void teamButton_Click(object sender, EventArgs e)
        {
            //Quando o botão equipe for pressionado(ou a tecla de atalho correspondente), a ação deve ser:
            //apresentar os nomes dos componentes da equipe de desenvolvimento do compilador na área para mensagens.
        }
    }
}
