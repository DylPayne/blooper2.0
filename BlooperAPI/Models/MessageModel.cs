using Microsoft.Data.SqlClient;
using System.Text;

namespace Blooper.Models
{
    public class MessageModel
    {
        public int id { get; }
        public string text { get; set; }
        public int to_id { get; set; }
        public int from_id { get; set; }

        public MessageModel(int id, string text, int to_id, int from_id)
        {
            this.id = id;
            this.text = text;
            this.to_id = to_id;
            this.from_id = from_id;
        }
        public MessageModel(string text, int to_id, int from_id)
        {
            this.text = text;
            this.to_id = to_id;
            this.from_id = from_id;
        }

        // This method is used to get the list of bloopers from the database
        // I'm doing this because if I call API endpoint I will have to make the Bloop method async
        // I cannot call the Get method from the Bloop method because it is not static
        private static List<BlooperModel> GetBloopers()
        {
            SqlConnection connection = new SqlConnection("Data Source=DYLANPAYNE;Initial Catalog=blooper2.0;Integrated Security=True;TrustServerCertificate=true");
            connection.Open();

            SqlCommand command = new SqlCommand("SELECT * FROM Bloopers", connection);

            List<BlooperModel> bloopers = new List<BlooperModel>();
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
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

        public static string Bloop(string text)
        {
            List<BlooperModel> blooperList = GetBloopers();

            string message = text;

            // Checking if the blooper is a phrase
            foreach (BlooperModel blooper in blooperList)
            {
                if (blooper.word.Contains(" "))
                {
                    Console.WriteLine("Phrase: " + blooper.word);
                    if (text.ToLower().Contains(blooper.word.ToLower()))
                    {
                        Console.WriteLine("Text contains phrase");
                        int c = 0;
                        string replacement = "";
                        while (c < blooper.word.Length)
                        {
                            replacement += "*";
                            c++;
                        }
                        message = message.ToLower().Replace(blooper.word.ToLower(), replacement);

                    }
                }
            }

            // Converting text to array of words
            Console.WriteLine(message);
            string[] words = message.Split(' ');

            // Looping through each word in the array
            int i = 0;
            foreach (string word in words)
            {
                int j = 0;
                foreach (BlooperModel blooper in blooperList)
                {
                    // Converting words to lower case to make sure the words are the same
                    if (blooper.word.ToLower() == word.ToLower())
                    {
                        // Replacing the word with asterisks
                        string asterisks = "";
                        while (j < word.Length)
                        {
                            asterisks += "*";
                            j++;
                        }
                        // Replacing the word in the array with the asterisks
                        words[i] = asterisks;
                    }
                }
                i++;
            }

            return text = string.Join(" ", words);
        }
    }
}
