using DataBaseConnector.Interfaces;
using DataBaseModels.DataBaseTables;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DataBaseConnector.Implementation
{
    class ColorsRepo : ICRUD<Color>
    {
        private readonly string _connectionString;
        public ColorsRepo(string connectionString)
        {
            _connectionString = connectionString;
        }
        public int Delete(object Id)
        {
            var id = Id as string;
            if (id == null)
                throw new Exception("Wrong type");

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("DELETE FROM Colors WHERE Id =@id");
                SqlParameter parameter = new SqlParameter("@id", id);
                command.Parameters.Add(parameter);
                var res = command.ExecuteNonQuery();
                return res;
            }
        }

        public IEnumerable<Color> GetAll()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                List<Color> colors = new List<Color>();
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Colors;");
                var res = command.ExecuteReader();
                if (res.HasRows)
                {
                    while (res.Read())
                    {
                        Color next = new Color();
                        next.Id = res.GetString(0);
                        next.Name = res.GetString(1);
                        colors.Add(next);
                    }
                }
                return colors;
            }
        }

        public Color GetById(object Id)
        {
            Color result = null;
            var id = Id as string;
            if (id == null)
                throw new Exception("Wrong type");
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Colors WHERE Id =@id;");
                command.Parameters.AddWithValue("@id", id);
                var res = command.ExecuteReader();
                if (res.FieldCount > 1)
                    throw new Exception("Invalid Model");
                if (res.FieldCount == 0)
                    return null;
                while (res.Read())
                {
                    result = new Color();
                    result.Id = res.GetString(0);
                    result.Name = res.GetString(1);
                }
            }
            return result;
        }

        public int Insert(Color item)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("INSERT INTO Colors (NAME) Values (@name);");
                SqlParameter parameter = new SqlParameter("@name", item.Name);
                command.Parameters.Add(parameter);
                var res = (int)command.ExecuteScalar();
                return res;
            }
        }

        public int Update(Color item)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("UPDATE Colors SET Name = @name WHERE Id =@id;");
                SqlParameter parameter = new SqlParameter("@name", item.Name);

                command.Parameters.AddWithValue("@name", item.Name);
                command.Parameters.AddWithValue("@id", item.Id);
                var res = command.ExecuteNonQuery();
                return res;
            }
        }
    }
}
