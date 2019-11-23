using Js.Dao;
using Js.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Js
{
    /// <summary>
    /// Lógica interna para Relatorios.xaml
    /// </summary>
    public partial class Relatorios : Window
    {
        public Relatorios()
        {

            InitializeComponent();
            CadastroDao cadastro = new CadastroDao();
            Dg.ItemsSource = cadastro.ListarCadastro();
        }

        private void Dg_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var cadt = Dg.SelectedItems as Cadastros;

            Cadastro cadastro = new Cadastro();
            cadastro.CarroTxt.Text = cadt.carro;
            cadastro.TxtPlaca.Text = cadt.placa;
            cadastro.NomeTxt.Text = cadt.nome;
            cadastro.DTxt.Text = cadt.data.ToString();
            cadastro.ShowDialog();
        }

        private void Dg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnSalvar_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Salvar Arquivo Texto";
            saveFileDialog.Filter = "Text File|.txt";
            saveFileDialog.FilterIndex = 0;
            saveFileDialog.DefaultExt = ".txt";
            saveFileDialog.RestoreDirectory = true;

            if (saveFileDialog.ShowDialog() == true)
            {
                FileStream stream1 = new FileStream(saveFileDialog.FileName, FileMode.OpenOrCreate);
                StreamWriter writer = new StreamWriter(stream1);
                writer.Write(txtTexto.Text);
                writer.Close();
                stream1.Close();
                txtTexto.Text = "";
                MessageBox.Show("salvo");
            }
        }

        private void btnAbrir_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                txtTexto.Text = "";
                Stream stream = File.Open(openFileDialog.FileName, FileMode.Open);
                StreamReader reader = new StreamReader(stream);
                string linha;
                while ((linha = reader.ReadLine()) != null)
                {
                    txtTexto.AppendText(linha);
                    txtTexto.AppendText(Environment.NewLine);
                }
                reader.Close();
                stream.Close();
            }
        }

        private void btnExcluir_Click(object sender, RoutedEventArgs e)
        {
            if (Dg.SelectedItem == null)
            {
                MessageBox.Show("selecione uma linha");
            }
            else
            {
                var a = Dg.SelectedItem as Cadastros;
                CadastroDao cadastroDao = new CadastroDao();
                cadastroDao.ExcluirCadastro(a);
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (Dg.SelectedItem == null)
            {
                MessageBox.Show("selecione uma linha");
            }
            else
            {
                var a = Dg.SelectedItem as Cadastros;
                CadastroDao cadastroDao = new CadastroDao();
                cadastroDao.Alterarcadastro(a);
            }
        }
    }
}
