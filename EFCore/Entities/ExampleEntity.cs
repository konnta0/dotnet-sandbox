using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCore.Entities;

[Table("Examples")]
public sealed class ExampleEntity
{
    /// <summary>
    /// ユーザID
    /// </summary>
    [Key]
    public int Id { get; init; }

    /// <summary>
    /// ユーザ名
    /// </summary>
    [Required]
    [MaxLength(50)]
    [Column(TypeName = "nvarchar(50)")]
    public string Name { get; init; }

    /// <summary>
    /// 組織ID
    /// </summary>
    public int OrganizationId { get; init; }

}