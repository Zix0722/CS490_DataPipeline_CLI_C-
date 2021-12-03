using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;
using Newtonsoft.Json;

namespace newsFeed
{
    class dataBase
    {
        private String connectingStr = "mongodb+srv://biscuit_admin:Buttered_Biscuits00@biscuitcluster0.xom7t.mongodb.net/CS490?retryWrites=true&w=majority";

        public void Connect()
        {
            MongoClient dbClient = new MongoClient(connectingStr);
        }
        public  static users GetUsersMain(BsonDocument user)
        {
            MongoClient dbClient = new MongoClient("mongodb+srv://biscuit_admin:Buttered_Biscuits00@biscuitcluster0.xom7t.mongodb.net/CS490?retryWrites=true&w=majority");
            var database = dbClient.GetDatabase("CS490");
            var collection = database.GetCollection<BsonDocument>("users");
            /*var document = new BsonDocument
            {
                {"username", username }
            };*/
            var document1 = collection.Find(user).FirstOrDefault();
            var result = document1.ToList();
            users returnVal = new users
            {
                _id = (ObjectId)result[0].Value,
                email = (string)result[1].Value,
                firstName = (string)result[2].Value,
                lastName = (string)result[3].Value,
                password = (string)result[4].Value,
                username = (string)result[5].Value,
                keywords = new List<String>(),
                business = (bool)result[7].Value,
                entertainment = (bool)result[8].Value,
                general = (bool)result[9].Value,
                health = (bool)result[10].Value,
                science = (bool)result[11].Value,
                sports = (bool)result[12].Value,
                technology = (bool)result[13].Value
            };

            BsonArray doc = document1.GetValue("keywords").AsBsonArray;
            if (doc != null)
            {
                foreach(var keyword in doc)
                {
                    returnVal.keywords.Add(keyword.ToString());
                    Console.WriteLine(keyword.ToString() + " DONE!");
                }
            }
            

            return returnVal;
        }
    }

    public class users
    {
        public ObjectId _id { get; set; }
        //public String _id { get; set; }
        public String email { get; set; }
        public String firstName { get; set; }
        public String lastName { get; set; }
        public String password { get; set; }

        public String username { get; set; }
        public List<String> keywords { get; set; }
        public bool business { get; set; }
        public bool entertainment { get; set; }
        public bool general { get; set; }
        public bool health { get; set; }
        public bool science { get; set; }

        public bool sports { get; set; }

        public bool technology { get; set; }
    }
}
