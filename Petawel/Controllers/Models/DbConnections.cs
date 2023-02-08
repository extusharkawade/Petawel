using Microsoft.AspNetCore.Mvc.ModelBinding;
using Petawel.DTO;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Petawel.Controllers.Models
{
    public class DbConnections
    {



        private readonly IConfiguration _configuration;
        SqlConnection sqlConnection;

        public DbConnections(IConfiguration configuration)
        {
            _configuration = configuration;
            sqlConnection = new SqlConnection(_configuration.GetConnectionString("conn").ToString());


        }

        public Response FindProductById(int id)
        {
            Response response = new();
            SqlCommand sqlCommand = new SqlCommand("select * from products where prod_id=" + id, sqlConnection);
            sqlConnection.Open();
            // SqlDataReader reader = sqlCommand.ExecuteReader();
            using (SqlDataReader reader = sqlCommand.ExecuteReader())
            {
                if (reader.Read())
                {
                    try
                    {
                        response.Product = new ProductModel();
                        response.Product.ProdId = (int)reader["prod_id"];
                        response.Product.ProdName = (string)reader["prod_name"];
                        //   response.Product.ProdName = reader[2].ToString();
                        response.Product.ProdPrice = (float)(double)reader["price"];
                        response.Product.ProdDetails = (string)reader["prod_details"];
                        response.Product.AvailableQuantity = (int)reader["available_quantity"];
                        response.Product.ImagePath = (string)reader["image_path"];

                        response.StatusMessage = "Product Retrieved Successfully";
                        response.StatusCode = 200;
                    }
                    catch (Exception ex)
                    {
                        response.StatusMessage = "BAD REQUEST";
                        response.StatusCode = 500;
                    }




                }
                else
                {
                    response.StatusMessage = "No Product Found";
                    response.StatusCode = 100;
                }
            }
            sqlConnection.Close();
            return response;
        }


    
        public Response getAllProduct()
        {
            Response responseresponse = new Response();
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
                    responseresponse.StatusCode = 200;
                    responseresponse.StatusMessage = "OK";
                    responseresponse.Products = products;
                }
                else
                {
                    responseresponse.StatusCode = 100;
                    responseresponse.StatusMessage = "Failed";
                }
            }
            else
            {
                responseresponse.StatusCode = 100;
                responseresponse.StatusMessage = "Failed";
            }
            return responseresponse;

        }

        public Response UpdateProduct(int id, ProductModel productModel)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("Update products SET prod_name = '" + productModel.ProdName + "', price = '" + productModel.ProdPrice + "', prod_details = '" + productModel.ProdDetails + "', available_quantity = '" + productModel.AvailableQuantity + "', image_path = '" + productModel.ImagePath + "' where prod_id = '" + id + "'", sqlConnection);
            //SqlCommand cmd = new SqlCommand("Update products SET prod_name = '" + temp + "' where prod_id = 1 ", sqlConnection);
            sqlConnection.Open();
            int i = cmd.ExecuteNonQuery();
            sqlConnection.Close();
            if (i > 0)
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

        public Response Registration(Registration registration)
        {
            Response response = new Response();
            SqlCommand sqlCommand = new SqlCommand("INSERT INTO users (name,contact,email,password) VALUES('"+registration.Name+"','"+registration.contact+"','"+registration.Email+"','"+registration.Password+"')",sqlConnection);
            sqlConnection.Open();       
            var i = sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Registration Successful";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Registration Failed";
            }

            return response;
        }

        //Give input in swagger as int 1 for food , 2 for Toy, 3 for cleaners as categories.
        public Response ProductbyCategory(int id, SqlConnection sqlConnection)
        {
            Response response = new Response();
            SqlDataAdapter da = new SqlDataAdapter("SELECT s.prod_name, s.prod_id, s.available_quantity, s.image_path, s.price,s.prod_details,s.price, c.category_id, category_name FROM products s INNER JOIN category c ON c.category_id = s.category_id WHERE c.category_id=" + id, sqlConnection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            List<ProductModel> products = new List<ProductModel>();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ProductModel model = new ProductModel();
                    model.category_id = Convert.ToInt32(dt.Rows[i]["category_id"]);
                    model.category_name = Convert.ToString(dt.Rows[i]["category_name"]);
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
                    response.StatusCode = 200;
                    response.StatusMessage = "OK";
                    response.Products =products;
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
            return response;
        }
        public Response SaveProduct(SaveProductDto productModel)
        {

            Response response = new Response();
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
        /*
        public Response checkCredentials(String email, string password, SqlConnection sqlConnection)
        {
            Response response = new Response();

            SqlCommand sqlCommand = new SqlCommand("select * from users where email='" + email+"' and password='"+password+"';", sqlConnection) ;
            sqlConnection.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();
  

            if (reader.Read())
            {
                response.StatusCode = 200;
                response.StatusMessage="Login successfulll";

            }
            else
            {
                response.StatusCode = 401;
                response.StatusMessage = "Login failed";
            }

            sqlConnection.Close();

            return response;
        }
        */


        public Dictionary<string, string> Credentials()
        {
            Response response = new Response();

            try
            { 
            SqlDataAdapter da = new SqlDataAdapter("Select email,password from users", sqlConnection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            Dictionary<string, string> crednetials =
                       new Dictionary<string, string>();

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    string email = Convert.ToString(dt.Rows[i]["email"]);
                    string password = Convert.ToString(dt.Rows[i]["password"]);

                    crednetials.Add(email, password);
                }

                sqlConnection.Close();

                return crednetials;
            }
            else
            {
                return null;
            }
            }
            catch(Exception e)
            {
                return null;
            }


        }

        public Response getAllCategories()
        {
            Response response = new Response();
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
      

    }

}
