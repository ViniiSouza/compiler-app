using LexicalAnalyzer;
using System;
using System.Windows.Forms;

namespace CompiladorApp
{
    public partial class Compilador : Form
    {
        private string _filePath = null;
        public Compilador()
        {
            InitializeComponent();

            DefinirTamanhoMinimo();
            this.KeyPreview = true;
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
            lineNumberRtb.richTextBox.Clear();
            messagesTextBox.Clear();
            statusBarLabel.Text = String.Empty;
            _filePath = String.Empty;
        }

        private void openButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Arquivos de texto (*.txt)|*.txt|All files (*.*)|*.*",
                Title = "Abrir arquivo de texto",
                Multiselect = false
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string fileContent = File.ReadAllText(openFileDialog.FileName);

                    lineNumberRtb.richTextBox.Text = fileContent;

                    messagesTextBox.Clear();

                    _filePath = openFileDialog.FileName;

                    statusBarLabel.Text = _filePath;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao abrir o arquivo: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_filePath))
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "Arquivos de texto (*.txt)|*.txt|All files (*.*)|*.*",
                    Title = "Salvar arquivo de texto",
                    DefaultExt = "txt"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        File.WriteAllText(saveFileDialog.FileName, lineNumberRtb.richTextBox.Text);

                        _filePath = saveFileDialog.FileName;

                        messagesTextBox.Clear();

                        statusBarLabel.Text = _filePath;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(Text = "Erro ao salvar o arquivo: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                try
                {
                    File.WriteAllText(_filePath, lineNumberRtb.richTextBox.Text);

                    messagesTextBox.Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(Text = "Erro ao salvar o arquivo: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void copyButton_Click(object sender, EventArgs e)
        {
            if (lineNumberRtb.richTextBox.SelectionLength > 0)
            {
                lineNumberRtb.richTextBox.Copy();
            }
        }

        private void pasteButton_Click(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText())
            {
                lineNumberRtb.richTextBox.Paste();
            }
        }

        private void cutButton_Click(object sender, EventArgs e)
        {
            if (lineNumberRtb.richTextBox.SelectionLength > 0)
            {
                lineNumberRtb.richTextBox.Cut();
            }
        }

        private void compileButton_Click(object sender, EventArgs e)
        {
            string sourceCode = lineNumberRtb.richTextBox.Text;
            Lexico scanner = new();
            
            scanner.SetInput(sourceCode);

            List<string> tokenList = new List<string>();
            bool error = false;

            try
            {
                Token t = null;
                while ((t = scanner.NextToken()) != null)
                {
                    int index = lineNumberRtb.richTextBox.GetCharIndexFromPosition(lineNumberRtb.richTextBox.GetPositionFromCharIndex(t.GetPosition()));
                    tokenList.Add($"linha {lineNumberRtb.richTextBox.GetLineFromCharIndex(index) + 1} - {GetTokenClassName(t.GetId())} - {t.GetLexeme()}");
                }
            }
            catch (LexicalError errorLexical) 
            {
                error = true;
                int index = lineNumberRtb.richTextBox.GetCharIndexFromPosition(lineNumberRtb.richTextBox.GetPositionFromCharIndex(errorLexical.GetPosition()));
                messagesTextBox.Rtf = string.Format(@"{{\rtf1\ansi linha {0}: \b {1}\b0  {2} }}", lineNumberRtb.richTextBox.GetLineFromCharIndex(index) + 1, errorLexical.GetLexeme(), errorLexical.Message);
            }

            if (!error)
            {
                messagesTextBox.Clear();
                if (tokenList.Any())
                    messagesTextBox.Text = string.Join(Environment.NewLine, tokenList);

                messagesTextBox.AppendText(Environment.NewLine + "Programa compilado com sucesso.");
            }
        }

        private void teamButton_Click(object sender, EventArgs e)
        {
            messagesTextBox.Text = "Cristian Monster\r\nLucas de Farias Teixeira\r\nVinícius Gabriel de Souza";
        }

        private void Compilador_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.N)
            {
                newButton_Click(sender, e);
            }
            else if (e.Control && e.KeyCode == Keys.O)
            {
                openButton_Click(sender, e);
            }
            else if (e.Control && e.KeyCode == Keys.S)
            {
                saveButton_Click(sender, e);
            }
            else if (e.KeyCode == Keys.F7)
            {
                compileButton_Click(sender, e);
            }
            else if (e.KeyCode == Keys.F1)
            {
                teamButton_Click(sender, e);
            }
            // os itens abaixo já funcionam de maneira nativa, validar se futuramente será preciso corrigir
            //else if (e.Control && e.KeyCode == Keys.C)
            //{
            //    copyButton_Click(sender, e);
            //}
            //else if (e.Control && e.KeyCode == Keys.V)
            //{
            //    pasteButton_Click(sender, e);
            //}
            //else if (e.Control && e.KeyCode == Keys.X)
            //{
            //    cutButton_Click(sender, e);
            //}
        }

        private string GetTokenClassName(int tokenId)
        {
            if (tokenId >= 2 && tokenId <= 17)
                return "símbolo especial";
            else if (tokenId >= 19 && tokenId <= 31)
                return "palavra reservada";
            else
                switch (tokenId) 
                {
                    case 32:
                        return "identificador";
                    case 33:
                        return "constante_int";
                    case 34:
                        return "constante_float";
                    case 35:
                        return "constante_string";
                    default:
                        return "classe não esperada";
                }
        }
    }
}
