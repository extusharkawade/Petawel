using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System;
using System.Data.SqlClient;
using PetawelAdmin.DTO;

namespace PetawelAdmin.Models
{
    public class DbConnectionAdmin
    {

        private readonly IConfiguration _configuration;
        SqlConnection sqlConnection;

        public DbConnectionAdmin(IConfiguration configuration)
        {
            _configuration = configuration;
            sqlConnection = new SqlConnection(_configuration.GetConnectionString("conn").ToString());
            Console.WriteLine("sqlconnection Success");
        }

        public ResponseAdmin getProduct()
        {
            Console.WriteLine("sqlconnection Success");
            ResponseAdmin response = new ResponseAdmin();
            SqlCommand sqlCommand = new SqlCommand("Select * from products", sqlConnection);
            Console.WriteLine("sqlconnection", sqlCommand.ToString());
            sqlConnection.Open();

            SqlDataReader data = sqlCommand.ExecuteReader();
            //Console.WriteLine("sqlconnection i ", i.ToString());
            sqlConnection.Close();
            if (data != null)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Successful";
            }
            else
            {
                response.StatusCode = 500;
                response.StatusMessage = "not working";
            }
            return response;
        }

        public ResponseAdmin ProductbyCategory(int id, SqlConnection sqlConnection)
        {
            ResponseAdmin response = new ResponseAdmin();
            SqlCommand sqlCommand = new SqlCommand("select * from category where category_id=" + id, sqlConnection);
            sqlConnection.Open();
            using (SqlDataReader reader = sqlCommand.ExecuteReader())
            {
                if (reader.Read())
                {
                    response.category = new Category();
                    response.category.Id = (int)reader["category_id"];
                    response.category.CategoryName = (String)reader["category_name"];
                    response.category.ImagePath = (String)reader["image_path"];

                    response.StatusMessage = "Product Found";
                    response.StatusCode = 200;
                }
                else
                {
                    response.StatusMessage = "Product Found";
                    response.StatusCode = 100;
                }
            }
            sqlConnection.Close();
            return response;
        }
        public ResponseAdmin getAllCategories()
        {
            ResponseAdmin response = new ResponseAdmin();
            SqlDataAdapter da = new SqlDataAdapter("Select * from category", sqlConnection);
            DataTable dt = new DataTable();
            da.Fill(dt);

            List<DTO.Category> categories = new List<DTO.Category>();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DTO.Category model = new DTO.Category();
                    model.Id = Convert.ToInt32(dt.Rows[i]["category_id"]);
                    model.CategoryName = Convert.ToString(dt.Rows[i]["category_name"]);
                    model.ImagePath = Convert.ToString(dt.Rows[i]["image_path"]);

                    categories.Add(model);
                }
                if (categories.Count > 0)
                {
                    response.StatusCode = 200;
                    response.StatusMessage = "OK";
                    response.categories = categories;
                }
                else
                {
                    response.StatusCode = 100;
                    response.StatusMessage = "Failed";
                }
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Failed";
            }
            sqlConnection.Close();
            return response;

        }

        public Boolean findAdmin(LoginDto loginDto)
        {
            string email = loginDto.email;
            string password = loginDto.Password;
            SqlCommand sqlCommand = new SqlCommand("Select * from users where email='" + email + "' and password='" + password + "' and isAdmin=1", sqlConnection);
            Console.WriteLine("sqlconnection", sqlCommand.ToString());
            sqlConnection.Open();

        //    SqlDataReader data = sqlCommand.ExecuteReader();
           
            using (SqlDataReader reader = sqlCommand.ExecuteReader())
            {
              
                if (reader.Read())
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
        }
    }
}
