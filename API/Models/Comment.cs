using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TPFinalStrasbourg.Models;

[Table("comment")]
public class Comment
{
    private int id;
    private string text;
    private DateTime date;
    private string status;

    [Column("id")]
    public int Id { get { return id; } set { id = value; } }

    [Column("text")]
    public string Text { get { return text; } set { text = value; } }

    [Column("date")]
    public DateTime Date { get { return date; } set { date = value; } }

    [Column("status")]
    public string Status { get { return status; } set { status = value; } }

    //[ForeignKey("UserId")]
    //[JsonIgnore]
    //public User User { get; set; }

    [Column("user_id")]
    public int UserId { get; set; }

    [ForeignKey("AdId")]
    public Ad Ad { get; set; }

    [Column("ad_id")]
    public int AdId { get; set; }
    

}
