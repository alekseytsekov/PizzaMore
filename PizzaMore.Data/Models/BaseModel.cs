namespace PizzaMore.Data.Models
{
    using System;

    public class BaseModel
    {
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
