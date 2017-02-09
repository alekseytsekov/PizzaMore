namespace PizzaMore.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Session :BaseModel
    {
        [Key]
        public string Id { get; set; }
        public int UserId { get; set; }

        public virtual User User { get; set; }

        public override string ToString()
        {
            return $"{this.Id}\t{this.UserId}";
        }
    }
}
