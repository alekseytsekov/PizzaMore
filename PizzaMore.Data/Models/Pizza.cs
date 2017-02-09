namespace PizzaMore.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Pizza : BaseModel
    {
        public int Id { get; set; }

        [Required]
        [MinLength(3), MaxLength(50)]
        public string Title { get; set; }

        [MaxLength(1000)]
        public string Recipe { get; set; }

        [MaxLength(100)]
        public string ImgUrl { get; set; }

        public int UpVotes { get; set; }

        public int DownVotes { get; set; }

        [ForeignKey("Owner")]
        public int OwnerId { get; set; }

        public virtual User Owner { get; set; }
    }
}
