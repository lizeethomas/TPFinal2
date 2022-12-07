using System.ComponentModel.DataAnnotations.Schema;

namespace TPFinalStrasbourg.Models;

[Table("ad")]
public class Ad
{
    private int id;
    private string title;
    private DateTime date;
    private string status;
    private string description;

    [Column("id")]
    public int Id
    {
        get => id;
        set => id = value;
    }

    [Column("title")]
    public string Title
    {
        get => title;
        set => title = value ?? throw new ArgumentNullException(nameof(title));
    }

    [Column("date")]
    public DateTime Date
    {
        get => date;
        set => date = value;
    }

    [Column("status")]
    public string Status
    {
        get => status;
        set => status = value ?? throw new ArgumentNullException(nameof(status));
    }

    [Column("description")]
    public string Description
    {
        get => description;
        set => description = value ?? throw new ArgumentNullException(nameof(description));
    }

    [Column("category_id")]
    public int CategoryId { get; set; }

    [ForeignKey("CategoryId")]
    public Category Category { get; set; }

    [Column("user_id")]
    public int UserId { get; set; }

    [ForeignKey("UserId")]
    public User User { get; set; }

    public List<Image> Images { get; set; }
    
    public List<Comment> Comments { get; set; }

    public Ad()
    {
        Images = new();
        Comments = new();
    }
}