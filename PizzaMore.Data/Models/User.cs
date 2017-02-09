namespace PizzaMore.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class User : BaseModel
    {
        private ICollection<Pizza> _pizzas;

        public User()
        {
            this._pizzas = new List<Pizza>();
        }

        public int Id { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(4)]
        public string Password { get; set; }

        public virtual ICollection<Pizza> Pizzas
        {
            get { return this._pizzas; }
            set { this._pizzas = value; }
        }
    }
}
