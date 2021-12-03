using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//-------------Database-------------------
using MongoDB.Driver;
using MongoDB.Bson;
//-------------News API --------------------
using NewsAPI;
using NewsAPI.Models;
using NewsAPI.Constants;
using System.Net;





namespace newsFeed
{
    class Program
    {
        static String urlForKeywords = "https://newsapi.org/v2/everything?q=";

        static String Region = "&country=us&";
        static String Key = "&apiKey=64982db81bfc475abe1ee2fe1aada3b8";
        static String urlForNormal = "https://newsapi.org/v2/top-headlines?country=us&category=";
        static async Task Main(string[] args)
        {
            
            //var settings = MongoClientSettings.FromConnectionString("mongodb+srv://biscuit_admin:Buttered_Biscuits00@biscuitcluster0.xom7t.mongodb.net/CS490?retryWrites=true&w=majority");
            //var client = new MongoClient(settings);

            //var database = client.GetDatabase("CS490");


            MongoClient dbClient = new MongoClient("mongodb+srv://biscuit_admin:Buttered_Biscuits00@biscuitcluster0.xom7t.mongodb.net/CS490?retryWrites=true&w=majority");

            var dbList = dbClient.ListDatabases().ToList();

            Console.WriteLine("The list of databases on this server is: ");
            foreach (var db in dbList)
            {
                Console.WriteLine(db);
            }

            //---------------------------------------------------------------


            //------------------time settings--------------------------------

            
            



            //-------------------------------------------------------------
            var database = dbClient.GetDatabase("CS490");
            var collection = database.GetCollection<BsonDocument>("users");
            List<users> users = new List<users>();
            await collection.Find(new BsonDocument()).ForEachAsync(d => users.Add(dataBase.GetUsersMain(d)));
            foreach(var user in users)
            {
                await keywords_NewsFeedAsync(user);
                //await normal_NewsFeedAsync(user);
                
            }
            await normalAll_NewsFeedAsync();

            Console.ReadLine();


            //---------------------------------------------------------------
            String keywords = "Apple";
            var collectionSite = database.GetCollection<BsonDocument>("site");
            
            //--------------------------------------------------------------------------
            BsonDocument newsData;
            List<Article> articles = await NewsApi.GetArticlesMain(urlForKeywords + keywords + Key);
            foreach(var article in articles)
            {
                newsData = new BsonDocument
                    {
                        { "title",checkNull(article.title)},
                        { "author",checkNull(article.author)},
                        { "description",checkNull(article.description)},
                        { "url",checkNull(article.url)},
                        { "publishedAt",article.publishedAt},
                        { "urlToImage", checkNull(article.urlToImage) }

                    };
                //await collectionSite.InsertOneAsync(newsData);
            }

            //---------------------------------------------------------------------------
            /*var newsApiClient = new NewsApiClient("64982db81bfc475abe1ee2fe1aada3b8");
            var articlesResponse = newsApiClient.GetEverything(new EverythingRequest
            {
                Q = "Apple",
                SortBy = SortBys.Relevancy,
                Language = Languages.EN,
                From = new DateTime(2021, 11, 26)
            });
            if (articlesResponse.Status == Statuses.Ok)
            {
                // total results found
                Console.WriteLine(articlesResponse.TotalResults.ToString());
                // here's the first 20
                foreach (var article in articlesResponse.Articles)
                {

          
                     newsData = new BsonDocument
                    {
                        { "title",checkNull(article.Title)},
                        { "author",checkNull(article.Author)},
                        { "description",checkNull(article.Description)},
                        { "url",checkNull(article.Url)},
                        { "publishedAt",article.PublishedAt}

                    };*/
            //collectionSite.InsertOneAsync(newsData);

            /*// title
            Console.WriteLine(article.Title);
            // author
            Console.WriteLine(article.Author);
            // description
            Console.WriteLine(article.Description);
            // url
            Console.WriteLine(article.Url);
            // published at
            Console.WriteLine(article.PublishedAt);
            }
        }
        else
        {
            Console.WriteLine(articlesResponse.Status);
        }
        Console.ReadLine();*/


        }

        private static String checkNull(String obj)
        {
            if(obj == null)
            {
                return " ";
            }
            else
            {
                return obj.ToString();
            }
        }

        private static async Task keywords_NewsFeedAsync(users obj)
        {
            MongoClient dbClient = new MongoClient("mongodb+srv://biscuit_admin:Buttered_Biscuits00@biscuitcluster0.xom7t.mongodb.net/CS490?retryWrites=true&w=majority");
            var database = dbClient.GetDatabase("CS490");
            var collection = database.GetCollection<BsonDocument>("site");
            foreach(var keyword in obj.keywords)
            {
                List<Article> articles = await NewsApi.GetArticlesMain(urlForKeywords + keyword + Key);
                BsonDocument newsData;
                foreach (var article in articles)
                {
                                newsData = new BsonDocument
                                    {
                                        { "title",checkNull(article.title)},
                                        { "author",checkNull(article.author)},
                                        { "description",checkNull(article.description)},
                                        { "url",checkNull(article.url)},
                                        { "publishedAt",article.publishedAt},
                                        { "urlToImage", checkNull(article.urlToImage) }

                                    };
                                await collection.InsertOneAsync(newsData);
                }
            }
            
            
        }

        private static async Task normal_NewsFeedAsync(users obj)
        {
            MongoClient dbClient = new MongoClient("mongodb+srv://biscuit_admin:Buttered_Biscuits00@biscuitcluster0.xom7t.mongodb.net/CS490?retryWrites=true&w=majority");
            var database = dbClient.GetDatabase("CS490");
            var collection = database.GetCollection<BsonDocument>(obj.username);
            if (obj.science)
            {
                List<Article> articles = await NewsApi.GetArticlesMain(urlForNormal + "science" + Key);
                BsonDocument newsData;
                foreach (var article in articles)
                {
                    newsData = new BsonDocument
                    {
                        { "title",checkNull(article.title)},
                        { "author",checkNull(article.author)},
                        { "description",checkNull(article.description)},
                        { "url",checkNull(article.url)},
                        { "publishedAt",article.publishedAt},
                        { "urlToImage", checkNull(article.urlToImage) }

                    };
                    await collection.InsertOneAsync(newsData);
                }
                Console.WriteLine("science feeds");
            }
            if (obj.business)
            {
                List<Article> articles = await NewsApi.GetArticlesMain(urlForNormal + "business" + Key);
                BsonDocument newsData;
                foreach (var article in articles)
                {
                    newsData = new BsonDocument
                    {
                        { "title",checkNull(article.title)},
                        { "author",checkNull(article.author)},
                        { "description",checkNull(article.description)},
                        { "url",checkNull(article.url)},
                        { "publishedAt",article.publishedAt},
                        { "urlToImage", checkNull(article.urlToImage) }

                    };
                    await collection.InsertOneAsync(newsData);
                }
                Console.WriteLine("business feeds");
            }
            if (obj.entertainment)
            {
                List<Article> articles = await NewsApi.GetArticlesMain(urlForNormal + "entertainment" + Key);
                BsonDocument newsData;
                foreach (var article in articles)
                {
                    newsData = new BsonDocument
                    {
                        { "title",checkNull(article.title)},
                        { "author",checkNull(article.author)},
                        { "description",checkNull(article.description)},
                        { "url",checkNull(article.url)},
                        { "publishedAt",article.publishedAt},
                        { "urlToImage", checkNull(article.urlToImage) }

                    };
                    await collection.InsertOneAsync(newsData);
                }
                Console.WriteLine("entertainment feeds");

            }
             if (obj.general)
            {
                List<Article> articles = await NewsApi.GetArticlesMain(urlForNormal + "general" + Key);
                BsonDocument newsData;
                foreach (var article in articles)
                {
                    newsData = new BsonDocument
                    {
                        { "title",checkNull(article.title)},
                        { "author",checkNull(article.author)},
                        { "description",checkNull(article.description)},
                        { "url",checkNull(article.url)},
                        { "publishedAt",article.publishedAt},
                        { "urlToImage", checkNull(article.urlToImage) }

                    };
                    await collection.InsertOneAsync(newsData);
                }
                Console.WriteLine("general feeds");
            }
            if (obj.health)
            {
                List<Article> articles = await NewsApi.GetArticlesMain(urlForNormal + "health" + Key);
                BsonDocument newsData;
                foreach (var article in articles)
                {
                    newsData = new BsonDocument
                    {
                        { "title",checkNull(article.title)},
                        { "author",checkNull(article.author)},
                        { "description",checkNull(article.description)},
                        { "url",checkNull(article.url)},
                        { "publishedAt",article.publishedAt},
                        { "urlToImage", checkNull(article.urlToImage) }

                    };
                    await collection.InsertOneAsync(newsData);
                }
                Console.WriteLine("health feeds");
            }
            if (obj.sports)
            {
                List<Article> articles = await NewsApi.GetArticlesMain(urlForNormal + "sports" + Key);
                BsonDocument newsData;
                foreach (var article in articles)
                {
                    newsData = new BsonDocument
                    {
                        { "title",checkNull(article.title)},
                        { "author",checkNull(article.author)},
                        { "description",checkNull(article.description)},
                        { "url",checkNull(article.url)},
                        { "publishedAt",article.publishedAt},
                        { "urlToImage", checkNull(article.urlToImage) }

                    };
                    await collection.InsertOneAsync(newsData);
                }
                Console.WriteLine("sports feeds");
            }
            if (obj.technology)
            {
                List<Article> articles = await NewsApi.GetArticlesMain(urlForNormal + "technology" + Key);
                BsonDocument newsData;
                foreach (var article in articles)
                {
                    newsData = new BsonDocument
                    {
                        { "title",checkNull(article.title)},
                        { "author",checkNull(article.author)},
                        { "description",checkNull(article.description)},
                        { "url",checkNull(article.url)},
                        { "publishedAt",article.publishedAt},
                        { "urlToImage", checkNull(article.urlToImage) }

                    };
                    await collection.InsertOneAsync(newsData);
                }
                Console.WriteLine("tech feeds");
            }

        }

        private static async Task normalAll_NewsFeedAsync()
        {
            MongoClient dbClient = new MongoClient("mongodb+srv://biscuit_admin:Buttered_Biscuits00@biscuitcluster0.xom7t.mongodb.net/CS490?retryWrites=true&w=majority");
            var database = dbClient.GetDatabase("CS490");
            var collection = database.GetCollection<BsonDocument>("site");
            if (true)
            {
                List<Article> articles = await NewsApi.GetArticlesMain(urlForNormal + "science" + Key);
                BsonDocument newsData;
                foreach (var article in articles)
                {
                    newsData = new BsonDocument
                    {
                        { "title",checkNull(article.title)},
                        { "author",checkNull(article.author)},
                        { "description",checkNull(article.description)},
                        { "url",checkNull(article.url)},
                        { "publishedAt",article.publishedAt},
                        { "urlToImage", checkNull(article.urlToImage) },
                        { "catagory", "science" }

                    };
                    await collection.InsertOneAsync(newsData);
                }
                Console.WriteLine("science feeds");
            }
            if (true)
            {
                List<Article> articles = await NewsApi.GetArticlesMain(urlForNormal + "business" + Key);
                BsonDocument newsData;
                foreach (var article in articles)
                {
                    newsData = new BsonDocument
                    {
                        { "title",checkNull(article.title)},
                        { "author",checkNull(article.author)},
                        { "description",checkNull(article.description)},
                        { "url",checkNull(article.url)},
                        { "publishedAt",article.publishedAt},
                        { "urlToImage", checkNull(article.urlToImage) },
                        { "catagory", "business" }

                    };
                    await collection.InsertOneAsync(newsData);
                }
                Console.WriteLine("business feeds");
            }
            if (true)
            {
                List<Article> articles = await NewsApi.GetArticlesMain(urlForNormal + "entertainment" + Key);
                BsonDocument newsData;
                foreach (var article in articles)
                {
                    newsData = new BsonDocument
                    {
                        { "title",checkNull(article.title)},
                        { "author",checkNull(article.author)},
                        { "description",checkNull(article.description)},
                        { "url",checkNull(article.url)},
                        { "publishedAt",article.publishedAt},
                        { "urlToImage", checkNull(article.urlToImage) },
                        { "catagory", "entertainment" }

                    };
                    await collection.InsertOneAsync(newsData);
                }
                Console.WriteLine("entertainment feeds");

            }
            if (true)
            {
                List<Article> articles = await NewsApi.GetArticlesMain(urlForNormal + "general" + Key);
                BsonDocument newsData;
                foreach (var article in articles)
                {
                    newsData = new BsonDocument
                    {
                        { "title",checkNull(article.title)},
                        { "author",checkNull(article.author)},
                        { "description",checkNull(article.description)},
                        { "url",checkNull(article.url)},
                        { "publishedAt",article.publishedAt},
                        { "urlToImage", checkNull(article.urlToImage) },
                        { "catagory", "general" }

                    };
                    await collection.InsertOneAsync(newsData);
                }
                Console.WriteLine("general feeds");
            }
            if (true)
            {
                List<Article> articles = await NewsApi.GetArticlesMain(urlForNormal + "health" + Key);
                BsonDocument newsData;
                foreach (var article in articles)
                {
                    newsData = new BsonDocument
                    {
                        { "title",checkNull(article.title)},
                        { "author",checkNull(article.author)},
                        { "description",checkNull(article.description)},
                        { "url",checkNull(article.url)},
                        { "publishedAt",article.publishedAt},
                        { "urlToImage", checkNull(article.urlToImage) },
                        { "catagory", "health"}

                    };
                    await collection.InsertOneAsync(newsData);
                }
                Console.WriteLine("health feeds");
            }
            if (true)
            {
                List<Article> articles = await NewsApi.GetArticlesMain(urlForNormal + "sports" + Key);
                BsonDocument newsData;
                foreach (var article in articles)
                {
                    newsData = new BsonDocument
                    {
                        { "title",checkNull(article.title)},
                        { "author",checkNull(article.author)},
                        { "description",checkNull(article.description)},
                        { "url",checkNull(article.url)},
                        { "publishedAt",article.publishedAt},
                        { "urlToImage", checkNull(article.urlToImage) },
                        { "catagory", "sports"}

                    };
                    await collection.InsertOneAsync(newsData);
                }
                Console.WriteLine("sports feeds");
            }
            if (true)
            {
                List<Article> articles = await NewsApi.GetArticlesMain(urlForNormal + "technology" + Key);
                BsonDocument newsData;
                foreach (var article in articles)
                {
                    newsData = new BsonDocument
                    {
                        { "title",checkNull(article.title)},
                        { "author",checkNull(article.author)},
                        { "description",checkNull(article.description)},
                        { "url",checkNull(article.url)},
                        { "publishedAt",article.publishedAt},
                        { "urlToImage", checkNull(article.urlToImage) },
                        { "catagory", "technology"}

                    };
                    await collection.InsertOneAsync(newsData);
                }
                Console.WriteLine("tech feeds");
            }

        }
    }
}
