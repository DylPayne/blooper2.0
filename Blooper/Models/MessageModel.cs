using Microsoft.Data.SqlClient;

namespace Blooper.Models
{
    public class MessageModel
    {
        public string text { get; set; }

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

        public static MessageModel Bloop(string text)
        {
            List<BlooperModel> blooperList = GetBloopers();

            // Converting text to array of words
            string[] words = text.Split(' ');

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

            return new MessageModel { text = string.Join(" ", words) };
        }
    }
}
