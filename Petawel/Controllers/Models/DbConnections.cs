using System.Data;
using System.Data.SqlClient;

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
        public Response FindProductById(int id, SqlConnection sqlConnection)
        {
            Response response = new();
            SqlCommand sqlCommand = new SqlCommand("select * from products where prod_id="+id,sqlConnection);
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
                    catch(Exception ex)
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

        public Response getAllProduct(SqlConnection sqlConnection)
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
        public Response UpdateProduct(int id, SqlConnection sqlConnection, ProductModel productModel)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("Update products SET prod_name = '" + productModel.ProdName + "', price = '" + productModel.ProdPrice + "', prod_details = '" + productModel.ProdDetails + "', available_quantity = '" + productModel.AvailableQuantity + "', image_path = '" + productModel.ImagePath + "' where prod_id = '"+id+"'", sqlConnection);
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
    }
}
