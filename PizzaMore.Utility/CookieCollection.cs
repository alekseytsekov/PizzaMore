namespace PizzaMore.Utility
{
    using System.Collections.Generic;

    public class CookieCollection : ICookieCollection
    {
        private readonly IDictionary<string,Cookie> _cookieCollection;

        public CookieCollection()
        {
            this._cookieCollection = new Dictionary<string, Cookie>();
        }

        public void AddCookie(Cookie cookie)
        {
            this._cookieCollection.Add(cookie.Name, cookie);
        }

        public void RemoveCookie(string cookieName)
        {
            this._cookieCollection.Remove(cookieName);
        }

        public bool ContainsKey(string key)
        {
            return this._cookieCollection.ContainsKey(key);
        }

        public int Count
        {
            get { return this._cookieCollection.Count; }
        }

        public Cookie this[string key]
        {
            get { return this._cookieCollection[key]; }
            set { this._cookieCollection[key] = value; }
        }
    }
}
