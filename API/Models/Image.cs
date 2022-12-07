using System.ComponentModel.DataAnnotations.Schema;

namespace TPFinalStrasbourg.Models
{
    [Table("image")]
    public class Image
    {
        private int id;
        private string url;

        [Column("id")]
        public int ID 
        { 
            get => id;
            set => id = value;
        }

        [Column("url")]
        public string Url
        {
            get => url;
            set => url = value ?? throw new ArgumentNullException(nameof(url));
        }

        [Column("ad_id")]
        public int AdId { get; set; }

        [ForeignKey("AdId")]
        public Ad Ad { get; set; }

    }
}