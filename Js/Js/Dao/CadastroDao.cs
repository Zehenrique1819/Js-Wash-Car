using Js.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace Js.Dao
{
    class CadastroDao
    {
        MySqlCommand command = new MySqlCommand();
        public void CadastrarCadastro(Cadastros cad)
        {
            MySqlConnection conn = new SqlConection().Criar();
            command = new MySqlCommand("INSERT INTO Cadastro (Nome, carro, placa, data) values (@nome, @carro, @placa, @data)", conn);
            command.Parameters.Add(new MySqlParameter("nome", cad.nome));
            command.Parameters.Add(new MySqlParameter("carro", cad.carro));
            command.Parameters.Add(new MySqlParameter("placa", cad.placa));
            command.Parameters.Add(new MySqlParameter("data", cad.data));
            command.Prepare();
            try
            {
                command.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }
        }

        public List<Cadastros> ListarCadastro()
        {
            List<Cadastros> cad = new List<Cadastros>();
            MySqlConnection conn = new SqlConection().Criar();
            command = new MySqlCommand("SELECT * FROM cadastro", conn);
            try
            {
                MySqlDataReader dr = command.ExecuteReader();
                cad = ConvertDataReaderToList(dr);
            }
            finally
            {
                conn.Close();
            }
            return cad;
        }

        public void Alterarcadastro(Cadastros cad)
        {
            MySqlConnection conn = new SqlConection().Criar();
            try
            {
                command = new MySqlCommand("update Cadastro set nome = ?, carro = ?, placa = ?, data =? where id = ?", conn);
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@nome", cad.nome);
                command.Parameters.AddWithValue("@carro", cad.carro);
                command.Parameters.AddWithValue("@id", cad.id_cadastro);
                command.Parameters.AddWithValue("@placa", cad.placa);
                command.Parameters.AddWithValue("@data", cad.data);

                command.CommandType = CommandType.Text;
                command.ExecuteNonQuery();

                conn.Close();
            }
            catch (Exception e)
            {
                throw new Exception("Erro {0}", e);
            }
        }

        public void ExcluirCadastro(Cadastros cad)
        {
            MySqlConnection conn = new SqlConection().Criar();
            command = new MySqlCommand("DELETE FROM Cadastro where id = @id ", conn);
            command.Parameters.Add(new MySqlParameter("id", cad.id_cadastro));
            command.Prepare();
            try
            {
                command.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }
        }

        private List<Cadastros> ConvertDataReaderToList(MySqlDataReader dreader)
        {
            List<Cadastros> cad = new List<Cadastros>();
            while (dreader.Read())
            {
                Cadastros cadastros = new Cadastros()
                {
                    id_cadastro = Convert.ToInt32(dreader["id_cadastro"]),
                    nome = dreader["nome"].ToString(),
                    carro = Convert.ToString(dreader["carro"]),
                    placa = Convert.ToString(dreader["placa"]),
                    data = Convert.ToDateTime(dreader["data"]),

                };
                cad.Add(cadastros);
            }
            return cad;
        }
    }
}
