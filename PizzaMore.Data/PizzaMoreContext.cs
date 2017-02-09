namespace PizzaMore.Data
{
    using System.Data.Entity;
    using PizzaMore.Data.Models;

    public class PizzaMoreContext : DbContext
    {
        // Your context has been configured to use a 'PizzaMoreContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'PizzaMore.Data.PizzaMoreContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'PizzaMoreContext' 
        // connection string in the application configuration file.

        public PizzaMoreContext()
            : base("name=PizzaMoreContext")
        {
            
        }
        
        // public virtual DbSet<MyEntity> MyEntities { get; set; }
        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Session> Sessions { get; set; }

        public virtual DbSet<Pizza> Pizzas { get; set; }
    }
}