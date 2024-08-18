
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
            //  Quando o bot�o novo for pressionado (ou a tecla de atalho correspondente), a a��o deve ser:
            // limpar o editor1
            // , limpar a �rea para mensagens2
            // e limpar a barra de status3

        }

        private void openButton_Click(object sender, EventArgs e)
        {
            //Quando o bot�o abrir for pressionado(ou a tecla de atalho correspondente), a a��o deve ser:
            //possibilitar a sele��o de pasta / arquivo, carregar o arquivo selecionado no editor, limpar a �rea para
            //mensagens2
            // e atualizar a barra de status4
            //.Caso nenhum arquivo seja selecionado, o estado da interface,
            //anterior ao bot�o abrir ser pressionado, deve ser mantido, ou seja, o editor deve manter o texto que est� sendo
            //editado, a �rea para mensagens n�o deve ser limpa e a barra de status n�o deve ser atualizada.Devem ser
            //abertos arquivos texto (extens�o.txt) compat�veis com o Notepad.

        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            //Quando o bot�o salvar for pressionado(ou a tecla de atalho correspondente), a a��o deve ser: (1)
            //se o arquivo for novo, possibilitar a sele��o de pasta / nome do arquivo, salvar o conte�do do editor no arquivo
            //com o nome e no local selecionados, manter no editor o texto que est� sendo editado, limpar a �rea para
            //mensagens2
            // e atualizar a barra de status4
            //; (2) caso contr�rio, salvar as altera��es do arquivo editado, manter
            //no editor o texto que est� sendo editado, limpar a �rea para mensagens2
            // e manter a barra de status5
            //.Devem
            //ser salvos arquivos texto(extens�o.txt) compat�veis com o Notepad. 
        }

        private void copyButton_Click(object sender, EventArgs e)
        {
            //A a��o associada a cada um dos bot�es de edi��o e as teclas de atalho associadas(copiar,
            //colar, recortar) � a mesma encontrada nos editores de texto convencionais
        }

        private void pasteButton_Click(object sender, EventArgs e)
        {
            //A a��o associada a cada um dos bot�es de edi��o e as teclas de atalho associadas(copiar,
            //colar, recortar) � a mesma encontrada nos editores de texto convencionais
        }

        private void cutButton_Click(object sender, EventArgs e)
        {
            //A a��o associada a cada um dos bot�es de edi��o e as teclas de atalho associadas(copiar,
            //colar, recortar) � a mesma encontrada nos editores de texto convencionais
        }

        private void compileButton_Click(object sender, EventArgs e)
        {
            //Quando o bot�o compilar for pressionado(ou a tecla de atalho correspondente), a a��o deve ser:
            //apresentar mensagem(compila��o de programas ainda n�o foi implementada) na �rea para mensagens
        }

        private void teamButton_Click(object sender, EventArgs e)
        {
            //Quando o bot�o equipe for pressionado(ou a tecla de atalho correspondente), a a��o deve ser:
            //apresentar os nomes dos componentes da equipe de desenvolvimento do compilador na �rea para mensagens.
        }
    }
}
