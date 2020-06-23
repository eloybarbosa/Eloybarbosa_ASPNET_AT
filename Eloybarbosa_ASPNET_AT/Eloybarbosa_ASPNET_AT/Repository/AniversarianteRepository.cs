using Dapper;
using Eloybarbosa_ASPNET_AT.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Eloybarbosa_ASPNET_AT.Repository
{
    public class AniversarianteRepository
    {
        private String ConnectionString { get; set; }

        public AniversarianteRepository(IConfiguration configuration)
        {
            this.ConnectionString = configuration.GetConnectionString("Conexao");
        }

        public void Save(Aniversariante aniversariante)
        {
            using (SqlConnection connection = new SqlConnection(this.ConnectionString))
            {
                string sql = "INSERT INTO ANIVERSARIANTE(NOME, SOBRENOME, DATANASCIMENTO) VALUES (@P1, @P2, @P3)";

                var command = connection.CreateCommand();
                command.CommandText = sql;
                command.Parameters.AddWithValue("P1", aniversariante.Nome);
                command.Parameters.AddWithValue("P2", aniversariante.Sobrenome);
                command.Parameters.AddWithValue("P3", aniversariante.Nascimento);
                command.CommandType = System.Data.CommandType.Text;

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();

            }
        }

        public void Update(Aniversariante aniversariante)
        {
            using (SqlConnection connection = new SqlConnection(this.ConnectionString))
            {
                string sql = @"
                                UPDATE ANIVERSARIANTE
                                SET NOME = @P1,
                                SOBRENOME = @P2,
                                DATANASCIMENTO = @P3
                                WHERE ID = @P4;
                ";

                var command = connection.CreateCommand();
                command.CommandText = sql;
                command.Parameters.AddWithValue("P1", aniversariante.Nome);
                command.Parameters.AddWithValue("P2", aniversariante.Sobrenome);
                command.Parameters.AddWithValue("P3", aniversariante.Nascimento);
                command.Parameters.AddWithValue("P4", aniversariante.Id);
                command.CommandType = System.Data.CommandType.Text;

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();

            }
        }

        public void Delete(Aniversariante aniversariante)
        {
            using (SqlConnection connection = new SqlConnection(this.ConnectionString))
            {
                string sql = @"
                                DELETE FROM ANIVERSARIANTE
                                WHERE ID = @P1
                ";

                var command = connection.CreateCommand();
                command.CommandText = sql;
                command.Parameters.AddWithValue("P1", aniversariante.Id);
                command.CommandType = System.Data.CommandType.Text;

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();

            }
        }
        public List<Aniversariante> GetAll()
        {
            List<Aniversariante> result = new List<Aniversariante>();

            using (SqlConnection connection = new SqlConnection(this.ConnectionString))
            {
                string sql = @"
                                SELECT ID, NOME, SOBRENOME, DATANASCIMENTO
                                FROM ANIVERSARIANTE
                                ORDER BY DATANASCIMENTO ASC
                ";

                var command = connection.CreateCommand();
                command.CommandText = sql;
                command.CommandType = System.Data.CommandType.Text;

                connection.Open();

                SqlDataReader dr = command.ExecuteReader();

                while (dr.Read())
                {
                    result.Add(new Aniversariante()
                    {
                        Id = dr.GetInt32("ID"),
                        Nome = dr.GetString("NOME"),
                        Sobrenome = dr.GetString("SOBRENOME"),
                        Nascimento = dr.GetDateTime("DATANASCIMENTO"),
                    });
                }

                connection.Close();
            }

            return result;
        }

        public Aniversariante GetById(int id)
        {
            Aniversariante result = null;

            using (SqlConnection connection = new SqlConnection(this.ConnectionString))
            {
                string sql = @"
                                SELECT ID, NOME, SOBRENOME, DATANASCIMENTO
                                FROM ANIVERSARIANTE
                                WHERE ID = @P1
                ";

                var command = connection.CreateCommand();
                command.CommandText = sql;
                command.Parameters.AddWithValue("P1", id);
                command.CommandType = System.Data.CommandType.Text;

                connection.Open();

                SqlDataReader dr = command.ExecuteReader();

                while (dr.Read())
                {
                    result = new Aniversariante()
                    {
                        Id = dr.GetInt32("ID"),
                        Nome = dr.GetString("NOME"),
                        Sobrenome = dr.GetString("SOBRENOME"),
                        Nascimento = dr.GetDateTime("DATANASCIMENTO"),
                    };
                }

                connection.Close();
            }

            return result;
        }

        public List<Aniversariante> Search(string query)
        {
            List<Aniversariante> result = new List<Aniversariante>();

            using (SqlConnection connection = new SqlConnection(this.ConnectionString))
            {
                string sql = @"
                                SELECT ID, NOME, SOBRENOME, DATANASCIMENTO
                                FROM ANIVERSARIANTE
                                WHERE ID (NOME LIKE '%' + @P1 + '%' OR SOBRENOME LIKE '%' + @P2 + '%')
                                ORDER BY DATANASCIMENTO ASC";

                result = connection.Query<Aniversariante>(sql, new { P1 = query, P2 = query }).ToList();

            }

            return result;
        }


    }
}

