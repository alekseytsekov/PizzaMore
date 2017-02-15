namespace PizzaMore.DetailsPizza
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using PizzaMore.Data;
    using PizzaMore.Data.Models;
    using PizzaMore.Utility;

    class DetailsPizza
    {
        public static Header Header = new Header();
        private static IDictionary<string, string> requestParameters = new Dictionary<string, string>();
        static void Main()
        {
            if (WebUtil.IsGet())
            {
                requestParameters = WebUtil.RetrieveGetParameters();

                var pizza = GetPizza(requestParameters);

                if (pizza == null)
                {
                    WebUtil.PageNotAllowed();
                }

                PrinPage(pizza);
            }
            else
            {
                WebUtil.PageNotAllowed();
            }
        }

        private static void PrinPage(Pizza pizza)
        {
            Header.Print();

            Header.Print();
            Console.WriteLine("<!doctype html><html lang=\"en\"><head><meta charset=\"UTF-8\" /><title>PizzaMore - Details</title><meta name=\"viewport\" content=\"width=device-width, initial-scale=1\" /><link rel=\"stylesheet\" href=\"/PizzaMore/bootstrap/css/bootstrap.min.css\" /><link rel=\"stylesheet\" href=\"/PizzaMore/css/signin.css\" /></head><body><div class=\"container\">");
            Console.WriteLine("<div class=\"jumbotron\">");
            Console.WriteLine("<a class=\"btn btn-danger\" href=\"Menu.exe\">All Suggestions</a>");
            Console.WriteLine($"<h3>{pizza.Title}</h3>");
            Console.WriteLine($"<img src=\"{pizza.ImgUrl}\" width=\"300px\"/>");
            Console.WriteLine($"<p>{pizza.Recipe}</p>");
            Console.WriteLine($"<p>Up: {pizza.UpVotes}</p>");
            Console.WriteLine($"<p>Down: {pizza.DownVotes}</p>");
            Console.WriteLine("</div>");
            Console.WriteLine("</div><script src=\"/PizzaMore/jquery/jquery-3.1.1.js\"></script><script src=\"/PizzaMore/bootstrap/js/bootstrap.min.js\"></script></body></html>");

        }

        private static Pizza GetPizza(IDictionary<string, string> requestParameters)
        {
            if (!requestParameters.ContainsKey("pizzaid"))
            {
                return null;
            }

            int id;

            if (!int.TryParse(requestParameters["pizzaid"], out id))
            {
                return null;
            }

            var db = new PizzaMoreContext();

            var pizza = db.Pizzas.FirstOrDefault(x => x.Id == id);

            return pizza;
        }
    }
}
