using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Blooper.Models;

namespace Blooper
{
    [ApiController]
    [Route("[controller]")]
    public class BloopersController : Controller
    {
        static string connectionString = "Data Source=DYLANPAYNE;Initial Catalog=blooper2.0;Integrated Security=True;TrustServerCertificate=true";

        [HttpPost]
        public string Post([FromBody] string word)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            SqlCommand command = new SqlCommand("INSERT INTO Bloopers (word) VALUES (@word)", connection);
            SqlParameter paramWord = new SqlParameter("@word", word);
            command.Parameters.Add(paramWord);

            command.ExecuteNonQuery();

            connection.Close();

            return "successfully created blooper " + word;
        }

        [HttpGet]
        public List<BlooperModel> Get() 
        {   
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            SqlCommand command = new SqlCommand("SELECT * FROM Bloopers", connection);

            List<BlooperModel> bloopers = new List<BlooperModel>();
            using(SqlDataReader reader = command.ExecuteReader())
            {
                while(reader.Read())
                {
                    BlooperModel blooper = new BlooperModel();
                    blooper.id = reader.GetInt32(0);
                    blooper.word = reader.GetString(1);
                    bloopers.Add(blooper);
                }
            }

            connection.Close();

            return bloopers;
        }

        [HttpDelete]
        public string Delete([FromBody] int id)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            SqlCommand command = new SqlCommand("DELETE FROM Bloopers WHERE id = @id", connection);
            SqlParameter paramId = new SqlParameter("@id", id);
            command.Parameters.Add(paramId);

            command.ExecuteNonQuery();

            connection.Close();

            return "Successfully deleted blooper " + id;
        }

        [HttpPut]
        public string Put([FromBody] BlooperModel blooper)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            SqlCommand command = new SqlCommand("UPDATE Bloopers SET word = @word WHERE id = @id", connection);
            SqlParameter paramId = new SqlParameter("@id", blooper.id);
            SqlParameter paramWord = new SqlParameter("@word", blooper.word);
            command.Parameters.Add(paramId);
            command.Parameters.Add(paramWord);

            command.ExecuteNonQuery();

            connection.Close();

            return "Successfully updated blooper " + blooper.id;
        }
    }
}
