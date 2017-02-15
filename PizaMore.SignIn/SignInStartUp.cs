namespace PizaMore.SignIn
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using PizzaMore.Data;
    using PizzaMore.Data.Models;
    using PizzaMore.Utility;

    class SignInStartUp
    {
        private const string DefaultPagePath = "../www/PizzaMore/SignIn.html";
        private const string DefaultHomePage = "../www/PizzaMore/Home.html";
        private const string DefaultIncorrectParamsPath = "../www/PizzaMore/404.html";
        private static IDictionary<string, string> _requestParameters = new Dictionary<string, string>();
        public static Header Header = new Header();

        static void Main()
        {
            if (WebUtil.IsGet())
            {
                ShowPage(DefaultPagePath);
            }
            else if (WebUtil.IsPost())
            {
                _requestParameters = WebUtil.RetrievePostParameters();

                var user = IsUserExists(_requestParameters);

                if (user == null)
                {
                    ShowPage(DefaultIncorrectParamsPath);
                }

                CreateSession(user);

                ShowPage(DefaultHomePage);
            }
        }

        private static void CreateSession(User user)
        {
            var db = new PizzaMoreContext();

            var session = new Session()
            {
                UserId = user.Id,
                User = user,
                CreatedOn = DateTime.UtcNow
            };

            db.Sessions.Add(session);

            db.SaveChanges();
        }

        private static User IsUserExists(IDictionary<string, string> requestParameters)
        {
            if (!requestParameters.ContainsKey("email") || !requestParameters.ContainsKey("password"))
            {
                return null;
            }

            var email = requestParameters["email"];
            var password = requestParameters["password"];

            var db = new PizzaMoreContext();

            var user = db.Users.FirstOrDefault(x => x.Email == email);

            if (user == null)
            {
                return null;
            }

            var isExists = PasswordHasher.Verify(user.Salt, user.Hash, password);

            if (!isExists)
            {
                return null;
            }
            
            return user;
        }

        public static void ShowPage(string pagePath)
        {
            Header.Print();
            WebUtil.PrintFileContent(pagePath);
        }
    }
}
