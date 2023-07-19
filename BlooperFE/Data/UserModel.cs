namespace BlooperFE.Data
{
    public class UserModel
    {
        public int id { get; set; }
        public string username { get; set; }
        public string role { get; set; }

        // Class for logging in
        public UserModel(string username)
        {
            this.username = username;
        }

        public UserModel() { }
    }
}
