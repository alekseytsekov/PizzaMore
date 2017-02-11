namespace PizzaMore.Utility
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Header
    {
        private ICollection<Cookie> _cookies;

        public Header()
        {
            this.Type = "Content-type: text/html";
            this._cookies = new List<Cookie>();
        }
        public string Type { get; set; }

        public string Location { get; private set; }

        public ICollection<Cookie> Cookies
        {
            get { return this._cookies; }
            set { this._cookies = value; }
        }

        public void AddLocation(string location)
        {
            this.Location = $"Location: {location}";
        }

        public void AddCookie(Cookie cookie)
        {
            this.Cookies.Add(cookie);
        }

        public void Print()
        {
            Console.WriteLine(this.ToString());
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine(this.Type);

            if (this.Cookies.Any())
            {
                foreach (var cookie in this.Cookies)
                {
                    result.AppendLine($"Set-Cookie: {cookie}");
                }
            }

            if (!string.IsNullOrEmpty(this.Location))
            {
                result.AppendLine(this.Location);
            }

            for (int i = 0; i < 2; i++)
            {
                result.AppendLine(Environment.NewLine);
            }

            return result.ToString();
        }
    }
}
