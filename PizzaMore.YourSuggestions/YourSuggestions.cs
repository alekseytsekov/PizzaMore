namespace PizzaMore.YourSuggestions
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using PizzaMore.Data;
    using PizzaMore.Data.Models;
    using PizzaMore.Utility;

    class YourSuggestions
    {
        private const string YourSuggestionsTopPagePath = "../www/PizzaMore/yoursuggestions-top.html";
        private const string YourSuggestionsBottomPagePath = "../www/PizzaMore/yoursuggestions-bottom.html";
        private const string DefaultIncorrectParamsPath = "../www/PizzaMore/404.html";

        public static Header Header = new Header();
        public static Session Session;
        private static IDictionary<string, string> requestParameters = new Dictionary<string, string>();

        static void Main()
        {
            Session session = WebUtil.GetSession();

            if (session == null)
            {
                WebUtil.PageNotAllowed();
            }

            if (WebUtil.IsGet())
            {
                ShowPage();
            }
            else if (WebUtil.IsPost())
            {
                requestParameters = WebUtil.RetrievePostParameters();
                DeletePizza(requestParameters);
                ShowPage();
            }
            else
            {
                WebUtil.PrintFileContent(DefaultIncorrectParamsPath);
            }
        }

        private static void DeletePizza(IDictionary<string, string> requestParameters)
        {
            if (!requestParameters.ContainsKey("pizzaId"))
            {
                return;
            }

            var pizzaId = requestParameters["pizzaId"];

            int id;

            if (!int.TryParse(pizzaId, out id))
            {
                return;
            }

            var db = new PizzaMoreContext();

            var user = db.Users.FirstOrDefault(x => x.Id == Session.UserId);

            if (user == null)
            {
                return;
            }

            var pizza = user.Pizzas.FirstOrDefault(x => x.Id == id);

            user.Pizzas.Remove(pizza);
            db.Pizzas.Remove(pizza);
            
            db.Users.AddOrUpdate(user);
            db.SaveChanges();
        }

        private static void ShowPage()
        {
            Header.Print();
            WebUtil.PrintFileContent(YourSuggestionsTopPagePath);
            PrintListOfSuggestedItems();
            WebUtil.PrintFileContent(YourSuggestionsBottomPagePath);
        }

        private static void PrintListOfSuggestedItems()
        {
            var db = new PizzaMoreContext();

            var suggestedPizzas = db.Pizzas.Where(x => x.OwnerId == Session.UserId);

            Console.WriteLine("<ul>");
            foreach (var pizzas in suggestedPizzas)
            {
                Console.WriteLine("<form method=\"POST\">");
                Console.WriteLine($"<li>" +
                                  $"<a href=\"DetailsPizza.exe?pizzaid={pizzas.Id}\">{pizzas.Title}</a> " +
                                  $"<input type=\"hidden\" name=\"pizzaId\" value=\"{pizzas.Id}\"/>" +
                                  $" <input type=\"submit\" class=\"btn btn-sm btn-danger\" value=\"X\"/>" +
                                  $"</li>");
                Console.WriteLine("</form>");
            }
            Console.WriteLine("</ul>");

        }
    }
}
