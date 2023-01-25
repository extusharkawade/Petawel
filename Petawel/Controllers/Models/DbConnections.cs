using System.Data;
using System.Data.SqlClient;

namespace Petawel.Controllers.Models
{
    public class DbConnections
    {
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
