using Blooper.Models;
using Microsoft.AspNetCore.Mvc;

namespace Blooper.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessageController
    {
        [HttpPost]
        public MessageModel Post([FromBody] string text)
        {
            return MessageModel.Bloop(text);
        }
    }
}
