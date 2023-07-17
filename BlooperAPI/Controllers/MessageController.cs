using Blooper.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Blooper.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessageController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<MessageModel> Post(string text, int to_id, int from_id)
        {
            string bloopedMessage = MessageModel.Bloop(text);

            string spName = @"post_message";
            SqlConnection connection = new SqlConnection("Data Source=DYLANPAYNE;Initial Catalog=blooper2.0;Integrated Security=True;TrustServerCertificate=true");
            connection.Open();

            SqlCommand command = new SqlCommand(spName, connection);
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter paramText = new SqlParameter("@text", bloopedMessage);
            SqlParameter paramToId = new SqlParameter("@to_id", to_id);
            SqlParameter paramFromId = new SqlParameter("@from_id", from_id);
            command.Parameters.Add(paramText);
            command.Parameters.Add(paramToId);
            command.Parameters.Add(paramFromId);

            try
            {
                command.ExecuteNonQuery();
                connection.Close();
                return CreatedAtAction(nameof(Post), "successfully created message " + bloopedMessage);
            }
            catch (Exception e)
            {
                connection.Close();
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<MessageModel> Get(int toId, int fromId)
        {
            string spName = @"get_messages";
            SqlConnection connection = new SqlConnection("Data Source=DYLANPAYNE;Initial Catalog=blooper2.0;Integrated Security=True;TrustServerCertificate=true");
            connection.Open();

            SqlCommand command = new SqlCommand(spName, connection);
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter paramToId = new SqlParameter("@to_id", toId);
            SqlParameter paramFromId = new SqlParameter("@from_id", fromId);
            command.Parameters.Add(paramToId);
            command.Parameters.Add(paramFromId);

            try
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    List<MessageModel> messages = new List<MessageModel>();
                    while (reader.Read())
                    {
                        MessageModel message = new MessageModel(
                            Convert.ToInt32(reader["id"]),
                            Convert.ToString(reader["text"]),
                            Convert.ToInt32(reader["to_id"]),
                            Convert.ToInt32(reader["from_id"])
                        );
                        messages.Add(message);
                    }
                    connection.Close();
                    return Ok(messages.ToArray());
                }
            } catch (Exception e)
            {
                connection.Close();
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<MessageModel> Delete(int id)
        {
            string spName = @"delete_message";
            SqlConnection connection = new SqlConnection("Data Source=DYLANPAYNE;Initial Catalog=blooper2.0;Integrated Security=True;TrustServerCertificate=true");
            connection.Open();

            SqlCommand command = new SqlCommand(spName, connection);
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter paramId = new SqlParameter("@id", id);
            command.Parameters.Add(paramId);

            try
            {
                command.ExecuteNonQuery();
                connection.Close();
                return Ok("successfully deleted message " + id);
            }
            catch (Exception e)
            {
                connection.Close();
                return BadRequest(e.Message);
            }
        }
    }
}
