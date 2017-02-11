namespace PizzaMore.Utility
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using PizzaMore.Data;
    using PizzaMore.Data.Models;

    public static class WebUtil
    {
        public static bool IsGet()
        {
            var method = Environment.GetEnvironmentVariable("REQUEST_METHOD");

            return method.ToLower() == "get";
        }

        public static bool IsPost()
        {
            var method = Environment.GetEnvironmentVariable("REQUEST_METHOD");

            return method.ToLower() == "post";
        }

        public static IDictionary<string, string> RetrieveGetParameters()
        {
            var queryString = Environment.GetEnvironmentVariable("QUERY_STRING");

            var result = RetrieveRequestParameters(queryString);

            if (string.IsNullOrWhiteSpace(queryString))
            {
                return null;
            }

            return result;
        }

        public static IDictionary<string, string> RetrievePostParameters()
        {
            var queryString = Console.ReadLine();

            var result = RetrieveRequestParameters(queryString);

            if (string.IsNullOrWhiteSpace(queryString))
            {
                return null;
            }

            return result;
        }

        public static ICookieCollection GetCookies()
        {
            var cookieString = Environment.GetEnvironmentVariable("HTTP_COOKIE");

            if (string.IsNullOrEmpty(cookieString))
            {
                return null;
            }

            var cookieCollection = new CookieCollection();

            var keyValuePairs = cookieString.Trim().Split(';');

            foreach (var keyValuePair in keyValuePairs)
            {
                var cookiePair = keyValuePair.Trim().Split('=');

                var cookie = new Cookie()
                {
                    Name = cookiePair[0].Trim(),
                    Value = cookiePair[1].Trim()
                };

                cookieCollection.AddCookie(cookie);
            }

            return cookieCollection;
        }

        public static Session GetSession()
        {
            var cookiesCollection = GetCookies();

            if (!cookiesCollection.ContainsKey(Constants.SidCookie))
            {
                return null;
            }

            var cookie = cookiesCollection[Constants.SidCookie];

            var db = new PizzaMoreContext();

            var session = db.Sessions.FirstOrDefault(x => x.Id == cookie.Value);

            if (session == null)
            {
                return null;
            }
            
            return session;
        }

        public static void PrintFileContent(string path)
        {
            var content = File.ReadAllText(path);

            Console.WriteLine(content);
        }

        private static IDictionary<string, string> RetrieveRequestParameters(string queryString)
        {
            if (string.IsNullOrWhiteSpace(queryString))
            {
                return null;
            }

            var result = new Dictionary<string, string>();

            queryString = WebUtility.UrlDecode(queryString);

            var keyValuePairs = queryString.Trim().Split('&');

            foreach (var keyValuePair in keyValuePairs)
            {
                var splitted = keyValuePair.Split('=');

                string value = null;

                if (splitted.Length > 1)
                {
                    value = splitted[1];
                }

                result.Add(splitted[0], value );
            }

            return result;
        }
    }
}
