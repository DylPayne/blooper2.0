using Blooper.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Blooper.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<UserModel> Post(UserModel user)
        {
            string spName = @"post_user";
            SqlConnection connection = new SqlConnection("Data Source=DYLANPAYNE;Initial Catalog=blooper2.0;Integrated Security=True;TrustServerCertificate=true");
            connection.Open();

            SqlCommand command = new SqlCommand(spName, connection);
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter paramUsername = new SqlParameter("@username", user.username);
            SqlParameter paramRole = new SqlParameter("@role", user.role);
            command.Parameters.Add(paramUsername);
            command.Parameters.Add(paramRole);

            try
            {
                command.ExecuteNonQuery();
                connection.Close();
                return CreatedAtAction(nameof(Post), "successfully created user " + user.username);
            }
            catch (Exception e)
            {
                connection.Close();
                return BadRequest(e.Message);
            }
        }


        [HttpGet("get")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<UserModel> Get(string username)
        {
            string spName = @"get_user";
            SqlConnection connection = new SqlConnection("Data Source=DYLANPAYNE;Initial Catalog=blooper2.0;Integrated Security=True;TrustServerCertificate=true");
            connection.Open();

            SqlCommand command = new SqlCommand(spName, connection);
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter sqlParameter = new SqlParameter("@username", username);
            command.Parameters.Add(sqlParameter);

            try
            {
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                UserModel user = new UserModel(reader.GetInt32(0), reader.GetString(1), reader.GetString(2));
                connection.Close();
                return Ok(user);
            }
            catch (Exception e)
            {
                connection.Close();
                return BadRequest(e.Message);
            }
        }

        [HttpGet("getall")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<UserModel[]> GetAll()
        {
            string spName = @"get_users";
            SqlConnection connection = new SqlConnection("Data Source=DYLANPAYNE;Initial Catalog=blooper2.0;Integrated Security=True;TrustServerCertificate=true");
            connection.Open();

            SqlCommand command = new SqlCommand(spName, connection);
            command.CommandType = CommandType.StoredProcedure;

            try
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    List<UserModel> users = new List<UserModel>();
                    while (reader.Read())
                    {
                        users.Add(new UserModel(reader.GetInt32(0), reader.GetString(1), reader.GetString(2)));
                    }
                    connection.Close();
                    return Ok(users.ToArray());
                }
            } catch (Exception e)
            {
                connection.Close();
                return BadRequest(e.Message);
            }
        }
    }
}
