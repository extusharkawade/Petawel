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

        public ResponseAdmin FindProductById(int id)
        {
            ResponseAdmin ResponseAdmin = new();
            SqlCommand sqlCommand = new SqlCommand("select * from products where prod_id=" + id, sqlConnection);
            sqlConnection.Open();
            // SqlDataReader reader = sqlCommand.ExecuteReader();
            using (SqlDataReader reader = sqlCommand.ExecuteReader())
            {
                if (reader.Read())
                {
                    try
                    {
                        ResponseAdmin.Product = new ProductModel();
                        ResponseAdmin.Product.ProdId = (int)reader["prod_id"];
                        ResponseAdmin.Product.ProdName = (string)reader["prod_name"];
                        //   ResponseAdmin.Product.ProdName = reader[2].ToString();
                        ResponseAdmin.Product.ProdPrice = (float)(double)reader["price"];
                        ResponseAdmin.Product.ProdDetails = (string)reader["prod_details"];
                        ResponseAdmin.Product.AvailableQuantity = (int)reader["available_quantity"];
                        ResponseAdmin.Product.ImagePath = (string)reader["image_path"];

                        ResponseAdmin.StatusMessage = "Product Retrieved Successfully";
                        ResponseAdmin.StatusCode = 200;
                    }
                    catch (Exception ex)
                    {
                        ResponseAdmin.StatusMessage = "BAD REQUEST";
                        ResponseAdmin.StatusCode = 500;
                    }




                }
                else
                {
                    ResponseAdmin.StatusMessage = "No Product Found";
                    ResponseAdmin.StatusCode = 100;
                }
            }
            sqlConnection.Close();
            return ResponseAdmin;
        }



        public ResponseAdmin getAllProduct()
        {
            ResponseAdmin ResponseAdminResponseAdmin = new ResponseAdmin();
            SqlDataAdapter da = new SqlDataAdapter("Select * from products", sqlConnection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            List<ProductModel> products = new List<ProductModel>();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ProductModel model = new ProductModel();
                    model.ProdId = Convert.ToInt32(dt.Rows[i]["prod_id"]);
                    model.ProdName = Convert.ToString(dt.Rows[i]["prod_name"]);
                    model.ProdPrice = Convert.ToInt32(dt.Rows[i]["price"]);
                    model.ProdDetails = Convert.ToString(dt.Rows[i]["prod_details"]);
                    model.AvailableQuantity = Convert.ToInt32(dt.Rows[i]["available_quantity"]);
                    model.ImagePath = Convert.ToString(dt.Rows[i]["image_path"]);
                    products.Add(model);
                }
                if (products.Count > 0)
                {
                    ResponseAdminResponseAdmin.StatusCode = 200;
                    ResponseAdminResponseAdmin.StatusMessage = "OK";
                    ResponseAdminResponseAdmin.Products = products;
                }
                else
                {
                    ResponseAdminResponseAdmin.StatusCode = 100;
                    ResponseAdminResponseAdmin.StatusMessage = "Failed";
                }
            }
            else
            {
                ResponseAdminResponseAdmin.StatusCode = 100;
                ResponseAdminResponseAdmin.StatusMessage = "Failed";
            }
            return ResponseAdminResponseAdmin;

        }

        public ResponseAdmin UpdateProduct(int id, ProductModel productModel)
        {
            ResponseAdmin ResponseAdmin = new ResponseAdmin();
            SqlCommand cmd = new SqlCommand("Update products SET prod_name = '" + productModel.ProdName + "', price = '" + productModel.ProdPrice + "', prod_details = '" + productModel.ProdDetails + "', available_quantity = '" + productModel.AvailableQuantity + "', image_path = '" + productModel.ImagePath + "' where prod_id = '" + id + "'", sqlConnection);
            //SqlCommand cmd = new SqlCommand("Update products SET prod_name = '" + temp + "' where prod_id = 1 ", sqlConnection);
            sqlConnection.Open();
            int i = cmd.ExecuteNonQuery();
            sqlConnection.Close();
            if (i > 0)
            {
                ResponseAdmin.StatusCode = 200;
                ResponseAdmin.StatusMessage = "Successful";
            }
            else
            {
                ResponseAdmin.StatusCode = 500;
                ResponseAdmin.StatusMessage = "not working";
            }
            return ResponseAdmin;
        }

        public ResponseAdmin SaveProduct(SaveProductDto productModel)
        {

            ResponseAdmin response = new ResponseAdmin();
            SqlCommand cmd = new SqlCommand("insert into products values('" + productModel.ProdName + "' ,'" + productModel.ProdPrice + "','" + productModel.ProdDetails + "','" + productModel.AvailableQuantity + "', '" + productModel.ImagePath + "','" + productModel.CatId + "');", sqlConnection);

            sqlConnection.Open();
            try
            {
                int i = cmd.ExecuteNonQuery();
                sqlConnection.Close();
                if (i > 0)
                {
                    //   response.Product = productModel;
                    response.StatusCode = 200;
                    response.StatusMessage = "Successful";

                }
                else
                {
                    response.StatusCode = 500;
                    response.StatusMessage = "not working";
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = 404;
                response.StatusMessage = "Internal server error";
            }

            return response;

        }

        public ResponseAdmin ProductbyCategory(int id, SqlConnection sqlConnection)
        {
           ResponseAdmin ResponseAdmin = new ResponseAdmin();
            SqlCommand sqlCommand = new SqlCommand("select * from category where category_id=" + id, sqlConnection);
            sqlConnection.Open();
            using (SqlDataReader reader = sqlCommand.ExecuteReader())
            {
                try { 
                if (reader.Read())
                {
                    ResponseAdmin.category = new Category();
                    ResponseAdmin.category.Id = (int)reader["category_id"];
                    ResponseAdmin.category.CategoryName = (String)reader["category_name"];
                    ResponseAdmin.category.ImagePath = (String)reader["image_path"];

                    ResponseAdmin.StatusMessage = "Product Found";
                    ResponseAdmin.StatusCode = 200;
                }
                else
                {
                    ResponseAdmin.StatusMessage = "Product Found";
                    ResponseAdmin.StatusCode = 100;
                }
                }
                catch(Exception e)
                {
                    Console.WriteLine("Exception in product by category " + e);
                }
            }
            sqlConnection.Close();
            return ResponseAdmin;
        }
        public ResponseAdmin getAllCategories()
        {
           ResponseAdmin ResponseAdmin = new ResponseAdmin();
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
                    ResponseAdmin.StatusCode = 200;
                    ResponseAdmin.StatusMessage = "OK";
                    ResponseAdmin.categories = categories;
                }
                else
                {
                    ResponseAdmin.StatusCode = 100;
                    ResponseAdmin.StatusMessage = "Failed";
                }
            }
            else
            {
                ResponseAdmin.StatusCode = 100;
                ResponseAdmin.StatusMessage = "Failed";
            }
            sqlConnection.Close();
            return ResponseAdmin;

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

        public ResponseAdmin AddNewCategory(Category category, SqlConnection sqlConnection)
        {
            SqlCommand cmd = new SqlCommand("insert into category values('" + category.CategoryName+ "' ,'" + category.ImagePath+ "' , '"+category.Main_id+"');", sqlConnection);
            ResponseAdmin responseAdmin = new ResponseAdmin();
            sqlConnection.Open();
            try
            {
                int i = cmd.ExecuteNonQuery();
                sqlConnection.Close();
                if (i > 0)
                {
                    responseAdmin.StatusCode = 200;
                    responseAdmin.StatusMessage = "New Category inserted Successfully.";
                    responseAdmin.category = category;

                }
                else
                {
                    responseAdmin.StatusCode = 500;
                    responseAdmin.StatusMessage = "Unable to add new category";
                }
            }
            catch (Exception ex)
            {
                responseAdmin.StatusCode = 404;
                responseAdmin.StatusMessage = "Internal server error" +ex;
            }
                    return responseAdmin;
        }
    }
}
