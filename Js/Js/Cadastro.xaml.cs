using Js.Dao;
using Js.Models;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for Cadastro.xaml
    /// </summary>
    public partial class Cadastro : Window
    {
        public Cadastro()
        {
            InitializeComponent();
        }

        private void BtnSalvar_Click(object sender, RoutedEventArgs e)
        {
            Cadastros cadastros = new Cadastros()
            {
                carro = CarroTxt.Text,
                data = Convert.ToDateTime(DTxt.Text),
                nome = NomeTxt.Text,
                placa = TxtPlaca.Text
            };

            CadastroDao cadastroDao = new CadastroDao();
            cadastroDao.CadastrarCadastro(cadastros);
            MessageBox.Show("Cadastrado com sucesso");


        }
    }
}
