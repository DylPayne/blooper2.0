using Microsoft.AspNetCore.Identity;

namespace Blooper.Models
{
    public class UserModel
    {
        public int id { get; }
        public string username { get; set; }
        public string role { get; set; }

        public UserModel(int id, string username, string role)
        {
            this.id = id;
            this.username = username;
            this.role = role;
        }
    }
}
