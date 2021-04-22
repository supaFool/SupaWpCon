using System;
using System.Threading.Tasks;
using WordPressPCL;
using WordPressPCL.Models;

namespace SupaWPCon
{
    public class WPManager
    {
        private string url;
        private static string name, pw;
        private string m_restAPITag = "/wp-json";
        private static string m_fullRelUrl;

        public WPManager(string url, string name, string pass)
        {
            this.url = url;
            WPManager.name = name;
            WPManager.pw = pass;
            m_fullRelUrl = url + m_restAPITag;

        }

        public async Task<bool> CreatePost(string title, string content)
        {
            try
            {
                Console.WriteLine("Trying to post");
                WordPressClient client = await GetClient(name, pw);
                if (await client.IsValidJWToken())
                {
                    var post = new Post
                    {
                        Title = new Title(title),
                        Content = new Content(content)
                    };
                    await client.Posts.Create(post);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("could not post");
                Console.WriteLine("Error:" + e.Message);
            }
            Console.WriteLine("Post complete");
            return true;
        }

        public async Task<bool> CreateUser(string newusername, string newuserpw)
        {
            try
            {
                Console.WriteLine("Trying to post");
                WordPressClient client = await GetClient(name, pw);
                if (await client.IsValidJWToken())
                {
                    
                    //await client.Posts.Create(post);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("could not post");
                Console.WriteLine("Error:" + e.Message);
            }
            Console.WriteLine("Post complete");
            return true;
        }

        private static async Task<WordPressClient> GetClient(string name, string pass)
        {
            // JWT authentication
            var client = new WordPressClient(m_fullRelUrl);
            client.AuthMethod = AuthMethod.JWT;
            await client.RequestJWToken(name, pass);
            Console.WriteLine("Connected");
            return client;
        }
    }
}