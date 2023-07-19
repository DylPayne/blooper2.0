namespace BlooperFE.Data
{
    public class ChatModel
    {
        public int from_id { get; set; }
        public string username { get; set; }
        public string slug { get; set; }

        public ChatModel(int from_id, string username)
        {
            this.from_id = from_id;
            this.username = username;
            this.slug = $"/chat/{from_id}#bottom-message";
        }
    }
}
