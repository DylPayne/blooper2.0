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
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<BlooperModel> Post([FromBody] string word)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            SqlCommand command = new SqlCommand("INSERT INTO Bloopers (word) VALUES (@word)", connection);
            SqlParameter paramWord = new SqlParameter("@word", word);
            command.Parameters.Add(paramWord);

            try
            {
                command.ExecuteNonQuery();
                connection.Close();
                return CreatedAtAction(nameof(Post), "successfully created blooper " + word);
            } catch (Exception e)
            {
                connection.Close();
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<BlooperModel[]> Get() 
        {   
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            SqlCommand command = new SqlCommand("SELECT * FROM Bloopers", connection);

            try
            {
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
                    connection.Close();

                    return Ok(bloopers.ToArray());
                }
            }
            catch (Exception e)
            {
                connection.Close();
                return BadRequest(e.Message);
            }
            
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<BlooperModel> Delete([FromBody] int id)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            SqlCommand command = new SqlCommand("DELETE FROM Bloopers WHERE id = @id", connection);
            SqlParameter paramId = new SqlParameter("@id", id);
            command.Parameters.Add(paramId);

            try
            {
                command.ExecuteNonQuery();
                connection.Close();
                return Ok("Successfully deleted blooper " + id);
            } catch (Exception e)
            {
                connection.Close();
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<BlooperModel> Put([FromBody] BlooperModel blooper)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            SqlCommand command = new SqlCommand("UPDATE Bloopers SET word = @word WHERE id = @id", connection);
            SqlParameter paramId = new SqlParameter("@id", blooper.id);
            SqlParameter paramWord = new SqlParameter("@word", blooper.word);
            command.Parameters.Add(paramId);
            command.Parameters.Add(paramWord);

            try
            {
                command.ExecuteNonQuery();
                connection.Close();
                return Ok("Successfully updated blooper " + blooper.id);
            } catch (Exception e)
            {
                connection.Close();
                return BadRequest(e.Message);
            }
        }
    }
}
