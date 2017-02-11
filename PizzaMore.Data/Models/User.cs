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
        [Key]
        public int Id { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Salt { get; set; }

        [Required]
        public string Hash { get; set; }

        public string RequestParams { get; set; }

        public virtual ICollection<Pizza> Pizzas
        {
            get { return this._pizzas; }
            set { this._pizzas = value; }
        }
    }
}
