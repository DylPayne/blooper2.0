namespace BlooperFE.Data
{
    public class MessageModel
    {
        public int id { get; set; }
        public string text { get; set; }
        public string to_id { get; set; }
        public int from_id { get; set; }
        public string edit_slug { get; set; }

        public MessageModel()
        {

        }

        public MessageModel(int id, string text, string to_id, int from_id)
        {
            this.id = id;
            this.text = text;
            this.to_id = to_id;
            this.from_id = from_id;
            this.edit_slug = $"/chat/{id}/edit";
        }
        public MessageModel(string text, string to_id, string from_id)
        {
            this.text = text;
            this.to_id = to_id;
            this.from_id = int.Parse(from_id);
        }
    }
}
