namespace PizzaMore.AddPizza
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using PizzaMore.Data;
    using PizzaMore.Data.Models;
    using PizzaMore.Utility;

    class AddPizza
    {
        private const string DefaultIncorrectParamsPath = "../www/PizzaMore/404.html";
        private static string AddPizzaHtmlPath = "../www/PizzaMore/addpizza.html";

        private static Session session;
        private static Header header;
        private static IDictionary<string, string> requestParameters = new Dictionary<string, string>();
        static void Main()
        {
            session = WebUtil.GetSession();

            if (session == null)
            {
                WebUtil.PageNotAllowed();
            }

            if (WebUtil.IsGet())
            {
                ShowPage(AddPizzaHtmlPath);
            }
            else if (WebUtil.IsPost())
            {
                requestParameters = WebUtil.RetrievePostParameters();

                var isSuccess = TryCreatePizza(requestParameters);

                if (!isSuccess)
                {
                    WebUtil.PrintFileContent(DefaultIncorrectParamsPath);
                }

                ShowPage(AddPizzaHtmlPath);
            }
            else
            {
                WebUtil.PageNotAllowed();
            }
        }

        private static bool TryCreatePizza(IDictionary<string, string> requestParameters)
        {
            if (!requestParameters.ContainsKey("title") || requestParameters.ContainsKey("recipe") || requestParameters.ContainsKey("url"))
            {
                return false;
            }

            var title = requestParameters["title"];
            var recipe = requestParameters["recipe"];
            var imgUrl = requestParameters["url"];
            
            var pizza = new Pizza()
            {
                Title = title,
                Recipe = recipe,
                ImgUrl = imgUrl,
                CreatedOn = DateTime.UtcNow,
                OwnerId = session.UserId,
                Owner = session.User
            };

            session.User.Pizzas.Add(pizza);

            var db = new PizzaMoreContext();

            db.Users.AddOrUpdate(session.User);

            return true;
        }

        private static void ShowPage(string path)
        {
            header.Print();
            WebUtil.PrintFileContent(path);
        }
    }
}
