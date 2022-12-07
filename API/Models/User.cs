using System.ComponentModel.DataAnnotations.Schema;

namespace TPFinalStrasbourg.Models;

[Table("ad_user")]
public class User
{
    private int id;
    private string name;
    private string email;
    private string password;
    private bool status;

    [Column("id")]
    public int Id
    {
        get => id;
        set => id = value;
    }

    [Column("name")]
    public string Name
    {
        get => name;
        set => name = value ?? throw new ArgumentNullException(nameof(name));
    }

    [Column("email")]
    public string Email
    {
        get => email;
        set => email = value ?? throw new ArgumentNullException(nameof(email));
    }

    [Column("password")]
    public string Password
    {
        get => password;
        set => password = value ?? throw new ArgumentNullException(nameof(password));
    }

    [Column("user_status")]
    public bool Status
    {
        get => status;
        set => status = value;
    }

    [ForeignKey("RoleId")]
    public Role Role { get; set; }

    [Column("role_id")]
    public int RoleId { get; set; }

    //public List<Comment> Comments { get; set; }

    public List<Ad> Ads { get; set; }
    
    public User()
    {
        //Comments = new();
        Ads = new();
    }
}