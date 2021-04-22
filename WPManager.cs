using System;
using System.Threading.Tasks;
using WordPressPCL;
using WordPressPCL.Models;

namespace SupaWPCon
{
    public class WPManager
    {

        internal class User
        {
            public string UserName;
            public string Password;
        }

        internal class WPSite
        {
            public string URL;
            public string AdminUrl;
        }

        private User currentUser;
        private WPSite site;
        private string m_restAPITag = "/wp-json";

        public WPManager(string url, string name, string pass)
        {
                currentUser = new User
                {
                    UserName = name,
                    Password = pass
                };
                Console.WriteLine("Created User");

            site = new WPSite
            {
                URL = url,
                AdminUrl = url + m_restAPITag
            };
            Console.WriteLine($"Created Site {site.AdminUrl}");


        }

        public async Task<bool> CreatePost(string title, string content)
        {
            try
            {
                Console.WriteLine("Trying to post");
                WordPressClient client = await GetClient(currentUser.UserName, currentUser.Password);
                if (await client.IsValidJWToken())
                {
                    var post = new Post
                    {
                        Title = new Title(title),
                        Content = new Content(content)
                    };
                    await client.Posts.Create(post);
                    Console.WriteLine("Logging out of client.");
                    client.Logout();
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

        public bool PersistantUserIsSet { get => currentUser != null; }

        public void KillPersistantUser()
        {
            currentUser = null;
        }

        public void SetPersistantUser(string username, string password)
        {
            currentUser = new User
            {
                UserName = username,
                Password = password
            };
        }

        public async Task<bool> CreatePost(string title, string content, string username, string password)
        {
            try
            {
                Console.WriteLine("Trying to post");
                WordPressClient client = await GetClient(username, password);
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
                WordPressClient client = await GetClient(currentUser.UserName, currentUser.Password);
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

        private async Task<WordPressClient> GetClient(string name, string pass)
        {
            // JWT authentication
            var client = new WordPressClient(site.AdminUrl);
            client.AuthMethod = AuthMethod.JWT;
            await client.RequestJWToken(name, pass);
            Console.WriteLine("Connected");
            return client;
        }
    }
}