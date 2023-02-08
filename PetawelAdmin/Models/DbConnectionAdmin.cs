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
