namespace SignUp
{
    using System;
    using System.Collections.Generic;
    using PizzaMore.Data;
    using PizzaMore.Data.Models;
    using PizzaMore.Utility;

    public class StartUp
    {
        private const string DefaultPagePath = "../www/PizzaMore/SignUp.html";
        private const string DefaultHomePAge = "../www/PizzaMore/Home.html";
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
                
                var user = TryCreateUser(_requestParameters);

                if (user == null)
                {
                    ShowPage(DefaultIncorrectParamsPath);
                }

                AddUserToDb(user);

                ShowPage(DefaultHomePAge);
            }
        }

        private static void AddUserToDb(User user)
        {
            var context = new PizzaMoreContext();

            context.Users.Add(user);
            context.SaveChanges();
        }

        private static User TryCreateUser(IDictionary<string, string> parameters)
        {
            if (!ValidateInput(parameters))
            {
                return null;
            }

            var hashing = new PasswordHasher();

            var email = parameters["email"];
            var saltHash = hashing.SaltHash(parameters["password"]);


            /*****************///////
            var par = string.Empty;

            foreach (var requestParameter in _requestParameters)
            {
                par += requestParameter.Key + ";" + requestParameter.Value + ";---------;";
            }

            /****************************************/

            var user = new User()
            {
                Email = email,
                Hash = saltHash[0],
                Salt = saltHash[1],
                CreatedOn = DateTime.UtcNow,
                RequestParams = par
            };

            return user;
        }

        private static bool ValidateInput(IDictionary<string, string> parameters)
        {
            if (!parameters.ContainsKey("email") || !parameters.ContainsKey("password"))
            {
                return false;
            }

            if (!CheckConstraints(parameters["email"], parameters["password"]))
            {
                return false;
            }
            return true;
        }

        private static bool CheckConstraints(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || email.Length < 5)
            {
                return false;
            }

            if (string.IsNullOrEmpty(password) || password.Length < 4)
            {
                return false;
            }

            return true;
        }

        private static void ShowPage(string path)
        {
            Header.Print();
            WebUtil.PrintFileContent(path);
        }
    }
}
