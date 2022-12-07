using System.ComponentModel.DataAnnotations.Schema;

namespace TPFinalStrasbourg.Models;

[Table("category")]
public class Category
{
    private int id;
    private string name;

    [Column("id")]
    public int Id { get { return id; } set { id = value; } }

    [Column("name")]
    public string Name { get { return name; } set { name = value; } }
}