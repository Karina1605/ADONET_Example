using DataBaseConnector.Interfaces;
using DataBaseModels.DataBaseTables;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DataBaseConnector.Implementation
{
    class CategoriesRepo : ICRUD<Category>
    {
        private readonly string _connectionString;
        public CategoriesRepo(string connectionString)
        {
            _connectionString = connectionString;
        }
        public int Delete(object Id)
        {
            var id = Id as int?;
            if (id == null)
                throw new Exception("Wrong type");

            using (SqlConnection connection =new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("DELETE FROM Categories WHERE Id =@id");
                SqlParameter parameter = new SqlParameter("@id", id);
                command.Parameters.Add(parameter);
                var res = command.ExecuteNonQuery();
                return res;
            }
        }

        public IEnumerable<Category> GetAll()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                List<Category> categories = new List<Category>();
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Categories;");
                var res = command.ExecuteReader();
                if (res.HasRows)
                {
                    while (res.Read())
                    {
                        Category next = new Category();
                        next.Id = res.GetInt32(0);
                        next.Name = res.GetString(1);
                        categories.Add(next);
                    }
                }
                return categories;
            }
        }

        public Category GetById(object Id)
        {
            Category result = null;
            var id = Id as int?;
            if (id == null)
                throw new Exception("Wrong type");
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Categories WHERE Id =@id;");
                command.Parameters.AddWithValue("@id", id);
                var res = command.ExecuteReader();
                if (res.FieldCount > 1)
                    throw new Exception("Invalid Model");
                if (res.FieldCount == 0)
                    return null;
                while (res.Read())
                {
                    result = new Category();
                    result.Id = res.GetInt32(0);
                    result.Name = res.GetString(1);
                }
            }
            return result;
        }

        public int Insert(Category item)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("INSERT INTO Categories (NAME) Values (@name);");
                SqlParameter parameter = new SqlParameter("@name", item.Name);
                command.Parameters.Add(parameter);
                var res = (int)command.ExecuteScalar();
                return res;
            }
        }

        public int Update(Category item)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("UPDATE Categories SET Name = @name WHERE Id =@id;");
                SqlParameter parameter = new SqlParameter("@name", item.Name);

                command.Parameters.AddWithValue("@name", item.Name);
                command.Parameters.AddWithValue("@id", item.Id);
                var res = command.ExecuteNonQuery() ;
                return res;
            }
        }
    }
}
