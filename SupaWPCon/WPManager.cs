using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WordPressPCL;
using WordPressPCL.Models;

namespace SupaWPCon
{
    public class WPManager
    {
        private string url;
        private string m_restAPITag = "/wp-json";
        private static string m_fullRelUrl;

        public WPManager(string url)
        {
            this.url = url;
            m_fullRelUrl = url + m_restAPITag;
            CreatePost().Wait();
            Console.In.ReadLine();
        }

        private static async Task CreatePost()
        {
            try
            {
                WordPressClient client = await GetClient();
                if (await client.IsValidJWToken())
                {
                    var post = new Post
                    {
                        Title = new Title("Post from Pillager app"),
                        Content = new Content("My Content for post")
                        
                    };
                    await client.Posts.Create(post);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error:" + e.Message);
            }
        }

        private static async Task<WordPressClient> GetClient()
        {
            // JWT authentication
            var client = new WordPressClient(m_fullRelUrl);
            client.AuthMethod = AuthMethod.JWT;
            await client.RequestJWToken("webmaster", "pass");
            return client;
        }
    }
}
