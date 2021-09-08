using DataBaseConnector.Interfaces;
using DataBaseModels.DataBaseTables;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DataBaseConnector.Implementation
{
    class ProductsRepo : ICRUD<Product>
    {
        private readonly string _connectionString;
        public ProductsRepo(string connectionString)
        {
            _connectionString = connectionString;
        }
        public int Delete(object Id)
        {
            var id = Id as int?;
            if (id == null)
                throw new Exception("Wrong type");

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("DELETE FROM Products WHERE Id =@id");
                SqlParameter parameter = new SqlParameter("@id", id);
                command.Parameters.Add(parameter);
                var res = command.ExecuteNonQuery();
                return res;
            }
        }

        public IEnumerable<Product> GetAll()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                List<Product> products = new List<Product>();
                connection.Open();
                SqlCommand command = new SqlCommand(@"SELECT * 
                                        FROM Products p JOIN Categories ct ON p.CategoryId = ct.Id 
                                        JOIN Colors cr ON p.ColorId = cr.Id;");
                var res = command.ExecuteReader();
                if (res.HasRows)
                {
                    while (res.Read())
                    {
                        Product next = new Product();
                        next.Id = res.GetInt32(0);
                        next.Name = res.GetString(1);
                        next.Cost = res.GetFloat(2);
                        next.Count = res.GetInt32(3);
                        next.Category = new Category() { Id = res.GetInt32(4), Name = res.GetString(5) };
                        next.Color = new Color() { Id = res.GetString(6), Name = res.GetString(7) };
                        next.PhotoPath = res.GetString(8);
                        products.Add(next);
                    }
                }
                return products;
            }
        }

        public Product GetById(object Id)
        {
            Product result = null;
            var id = Id as int?;
            if (id == null)
                throw new Exception("Wrong type");
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(@"SELECT * 
                                        FROM Products p JOIN Categories ct ON p.CategoryId = ct.Id 
                                        JOIN Colors cr ON p.ColorId = cr.Id Products WHERE Id =@id;");
                command.Parameters.AddWithValue("@id", id);
                var res = command.ExecuteReader();
                if (res.FieldCount > 1)
                    throw new Exception("Invalid Model");
                if (res.FieldCount == 0)
                    return null;
                while (res.Read())
                {
                    result = new Product();
                    result.Id = res.GetInt32(0);
                    result.Name = res.GetString(1);
                    result.Cost = res.GetFloat(2);
                    result.Count = res.GetInt32(3);
                    result.Category = new Category() { Id = res.GetInt32(4), Name = res.GetString(5) };
                    result.Color = new Color() { Id = res.GetString(6), Name = res.GetString(7) };
                    result.PhotoPath = res.GetString(8);
                }
            }
            return result;
        }

        public int Insert(Product item)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(@"INSERT INTO Products (Name, Cost, Count, CategoryId, ColorId, PhotoPath) 
                                                    VALUES (@name, @cost, @count, @categoryId, @colorId, @photo);");
                command.Parameters.AddWithValue("@name", item.Name);
                command.Parameters.AddWithValue("@cost", item.Cost);
                command.Parameters.AddWithValue("@count", item.Count);
                command.Parameters.AddWithValue("@categoryId", item.Category.Id);
                command.Parameters.AddWithValue("@colorId", item.Color.Id);
                command.Parameters.AddWithValue("@photo", item.PhotoPath);
                var res = (int)command.ExecuteScalar();
                return res;
            }
        }

        public int Update(Product item)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(@"UPDATE Colors SET 
                                                    Name = @name,
                                                    Cost =@cost,
                                                    Count =@count,
                                                    CategoryId =@categotyId,
                                                    ColorId =@colorId,
                                                    PhotoPath =@photo
                                                    WHERE Id =@id;");
                command.Parameters.AddWithValue("@name", item.Name);
                command.Parameters.AddWithValue("@cost", item.Cost);
                command.Parameters.AddWithValue("@count", item.Count);
                command.Parameters.AddWithValue("@categoryId", item.Category.Id);
                command.Parameters.AddWithValue("@colorId", item.Color.Id);
                command.Parameters.AddWithValue("@photo", item.PhotoPath);
                command.Parameters.AddWithValue("@id", item.Id);
                var res = command.ExecuteNonQuery();
                return res;
            }
        }
    }
}
