namespace Home
{
    using System;
    using System.Collections.Generic;
    using PizzaMore.Data.Models;
    using PizzaMore.Utility;
    using System.Linq;
    using PizzaMore.Data;

    internal class StartUp
    {
        private const string DefaultLanguage = "EN";
        private static IDictionary<string, string> _requestParameters = new Dictionary<string, string>();
        private static ICookieCollection _cookieCollection;
        private static  Session Session;
        private static readonly Header Header = new Header();
        private static string _language;

        static void Main()
        {
            AddDefaultLanguageCookie();

            if (WebUtil.IsGet())
            {
                _requestParameters = WebUtil.RetrieveGetParameters();
                TryLogOut(_requestParameters);
            }
            else if (WebUtil.IsPost())
            {
                _requestParameters = WebUtil.RetrievePostParameters();

                if (_requestParameters.ContainsKey("language"))
                {
                    Header.Cookies.FirstOrDefault(x => x.Name == "lang").Value = _requestParameters["language"];
                }
            }

            SetLanguage();

            ShowPage();
        }

        private static void TryLogOut(IDictionary<string, string> requestParameters)
        {
            if (!requestParameters.ContainsKey("logout"))
            {
                return;
            }

            var value = requestParameters["logout"];

            if (value != "true")
            {
                return;
            }

            var db = new PizzaMoreContext();

            Session = WebUtil.GetSession();

            var currentSession = db.Sessions.FirstOrDefault(x => x.Id == Session.Id);

            db.Sessions.Remove(currentSession);

            db.SaveChanges();
        }

        private static void AddDefaultLanguageCookie()
        {
            _cookieCollection = WebUtil.GetCookies();

            if (!_cookieCollection.ContainsKey("lang"))
            {
                MakeLanguageCookie();
            }
            else
            {
                Header.AddCookie(_cookieCollection["lang"]);
            }
        }

        private static void MakeLanguageCookie(string lang = DefaultLanguage)
        {
            var cookie = new Cookie("lang", lang);
            Header.AddCookie(cookie);
        }

        private static void ShowPage()
        {
            Header.Print();

            switch (_language)
            {
                case "EN":
                    WebUtil.PrintFileContent("../www/PizzaMore/Home.html");
                    break;
                case "BG":
                    WebUtil.PrintFileContent("../www/PizzaMore/Home.html");
                    break;
                default:
                    WebUtil.PrintFileContent("../www/PizzaMore/404.html");
                    break;
            }
        }

        private static void SetLanguage()
        {
            var cookie = Header.Cookies.FirstOrDefault(x => x.Name == "lang");

            if (cookie == null)
            {
                //return;
            }

            _language = cookie.Value;
        }
    }
}
