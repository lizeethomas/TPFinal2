using System.ComponentModel.DataAnnotations.Schema;

namespace TPFinalStrasbourg.Models;

[Table("role")]
public class Role
{
    [Column("id")]
    public int Id { get; set; }

    [Column("role")]
    public string RoleUser { get; set; }
}