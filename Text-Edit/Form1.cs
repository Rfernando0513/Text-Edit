using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Text_Edit
{
    public partial class TextEdit : Form
    {
        StringReader leitura = null;
        public TextEdit()
        {
            InitializeComponent();
        }
 
        private void Novo()
        {
            richTextBox1.Clear();
            richTextBox1.Focus();
        }
        private void BtnNovo_Click(object sender, EventArgs e)
        {
            Novo();
        }

        private void novoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Novo();
        }

        private void Abrir()
        {
            this.openFileDialog1.Multiselect = false;
            this.openFileDialog1.Title = "Abrir Arquivo";
            openFileDialog1.InitialDirectory = @"C:\\Users\\Fernando\\Documents\\Curso-DotNet\\Text-Edit";
            openFileDialog1.Filter = "(*.PDF)|*.PDF";
            //validação de abertura
            DialogResult dr = this.openFileDialog1.ShowDialog();
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    FileStream arquivo = new FileStream(openFileDialog1.FileName, FileMode.OpenOrCreate, FileAccess.Read);
                    StreamReader textEdit_streamReader = new StreamReader(arquivo);
                    textEdit_streamReader.BaseStream.Seek(0, SeekOrigin.Begin);
                    this.richTextBox1.Text = "";
                    string linha = textEdit_streamReader.ReadLine();
                    while(linha != null)
                    {
                        this.richTextBox1.Text += linha + "\n";
                        linha = textEdit_streamReader?.ReadLine();
                    }
                    textEdit_streamReader.Close();
                }
                catch (Exception ex)
                {

                    MessageBox.Show("Erro na leitura: " + ex.Message, "Erro ao ler arquivo", MessageBoxButtons.OK, MessageBoxIcon.Error);;
                }
            }

        }
        private void BtnAbrir_Click(object sender, EventArgs e)
        {
            Abrir();
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Abrir();
        }

        private void Salvar()
        {
            try
            {
                if (this.saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    FileStream arquivo = new FileStream(saveFileDialog1.FileName, FileMode.OpenOrCreate, FileAccess.Write);
                    StreamWriter textEdit_streamWriter = new StreamWriter(arquivo);
                    textEdit_streamWriter.Flush();
                    textEdit_streamWriter.BaseStream.Seek(0, SeekOrigin.Begin);
                    textEdit_streamWriter.Write(this.richTextBox1.Text);
                    textEdit_streamWriter.Flush();
                    textEdit_streamWriter.Close();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro na gravação do arquivo: " + ex.Message, "Erro ao gravar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSalvar_Click(object sender, EventArgs e)
        {
            Salvar();
        }

        private void salvarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Salvar();
        }

        private void Imprimir()
        {
            printDialog1.Document = printDocument1;
            string Texto = this.richTextBox1.Text;
            leitura = new StringReader(Texto);

            if(printDialog1.ShowDialog() == DialogResult.OK)
            {
                this.printDocument1.Print();
            }
        }
        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Imprimir();
        }
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            float linhasPagina = 0;
            float PosicionamentoY = 0;
            int Contador = 0;
            float MarginEsquerda = e.MarginBounds.Left - 50;
            float MarginSuperior = e.MarginBounds.Top - 50;

            if(MarginEsquerda < 5)
            {
                MarginEsquerda = 20;
            }

            if(MarginSuperior < 5)
            {
                MarginSuperior = 20;
            }

            string Linha = null;
            Font Fonte = this.richTextBox1.Font;
            SolidBrush Pincel = new SolidBrush(Color.Black);
            linhasPagina = e.MarginBounds.Height / Fonte.GetHeight(e.Graphics);
            Linha = leitura.ReadLine();

            while(Contador < linhasPagina)
            {
                PosicionamentoY = (MarginSuperior + (Contador * Fonte.GetHeight(e.Graphics)));
                e.Graphics.DrawString(Linha, Fonte, Pincel, MarginEsquerda, PosicionamentoY, new StringFormat());
                Contador += 1;
                Linha = leitura.ReadLine();
            }
            if(Linha != null)
            {
                e.HasMorePages = true;
            }
            else{
                e.HasMorePages = false;
            }
            Pincel.Dispose();
        }

        private void Copiar()
        {
            if(richTextBox1.SelectionLength > 0)
            {
                richTextBox1.Copy();
            }
        }
        private void BtnCopiar_Click(object sender, EventArgs e)
        {
            Copiar();
        }
        private void copiarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Copiar();
        }

        private void Colar()
        {
            richTextBox1.Paste();
        }

        private void BtnColar_Click(object sender, EventArgs e)
        {
            Colar();
        }
        private void colarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Colar();
        }
        private void Negrito()
        {
            if (richTextBox1.SelectionFont != null)
            {
                FontStyle estiloFonte = richTextBox1.SelectionFont.Style;

                if (estiloFonte.HasFlag(FontStyle.Bold))
                {
                    estiloFonte &= ~FontStyle.Bold;
                }
                else
                {
                    estiloFonte |= FontStyle.Bold;
                }

                richTextBox1.SelectionFont = new Font(
                    richTextBox1.SelectionFont.FontFamily,
                    richTextBox1.SelectionFont.Size,
                    estiloFonte
                );
            }
        }


        private void BtnNegrito_Click(object sender, EventArgs e)
        {
            Negrito();
        }

        private void Italico()
        {
            if (richTextBox1.SelectionFont != null)
            {
                FontStyle estiloFonte = richTextBox1.SelectionFont.Style;

                if (estiloFonte.HasFlag(FontStyle.Italic))
                {
                    estiloFonte &= ~FontStyle.Italic;
                }
                else
                {
                    estiloFonte |= FontStyle.Italic;
                }

                richTextBox1.SelectionFont = new Font(
                    richTextBox1.SelectionFont.FontFamily,
                    richTextBox1.SelectionFont.Size,
                    estiloFonte
                );
            }
        }

        private void BtnItalico_Click(object sender, EventArgs e)
        {
            Italico();
        }

        private void Sublinhado()
        {
            if (richTextBox1.SelectionFont != null)
            {
                FontStyle estiloFonte = richTextBox1.SelectionFont.Style;

                if (estiloFonte.HasFlag(FontStyle.Underline))
                {
                    estiloFonte &= ~FontStyle.Underline;
                }
                else
                {
                    estiloFonte |= FontStyle.Underline;
                }

                richTextBox1.SelectionFont = new Font(
                    richTextBox1.SelectionFont.FontFamily,
                    richTextBox1.SelectionFont.Size,
                    estiloFonte
                );
            }
        }

        private void BtnSublinhado_Click(object sender, EventArgs e)
        {
            Sublinhado();
        }

        private void Fonte()
        {

        }
        private void BtnFonte_Click(object sender, EventArgs e)
        {

        }

        private void AlinharEsquerda()
        {
            richTextBox1.SelectionAlignment = HorizontalAlignment.Left;
        }
        private void esquerdaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AlinharEsquerda();
        }

        private void BtnEsquerda_Click(object sender, EventArgs e)
        {
            AlinharEsquerda();
        }


        private void AlinharCentro()
        {
            richTextBox1.SelectionAlignment = HorizontalAlignment.Center;
        }

        private void centralizarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AlinharCentro();
        }

        private void BtnCentro_Click(object sender, EventArgs e)
        {
            AlinharCentro();
        }

        private void AlinharDireita()
        {
            richTextBox1.SelectionAlignment = HorizontalAlignment.Right;
        }

        private void direitaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AlinharDireita();
        }
        private void BtnDireita_Click(object sender, EventArgs e)
        {
            AlinharDireita();
        }
    }
}
