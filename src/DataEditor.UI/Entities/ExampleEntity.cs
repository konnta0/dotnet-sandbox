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
    [ColumnOrder(1)]
    public int Id { get; init; }

    /// <summary>
    /// ユーザ名
    /// </summary>
    [Required]
    [MaxLength(50)]
    [Column(TypeName = "nvarchar(50)")]
    [ColumnOrder(2)]
    public string Name { get; init; }

    /// <summary>
    /// 組織ID
    /// </summary>
    [ColumnOrder(4)]
    public int OrganizationId { get; init; }

    /// <summary>
    /// 組織ID
    /// </summary>
    [ColumnOrder(3)]
    public int OrganizationId2 { get; init; }
}