using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EFCore;

namespace DataEditor.UI.Entities;

[Table("ExamplesSchedule")]
public class EventSchedule
{
    [Key]
    [ColumnOrder(1)]
    public int Id { get; set; }

    [MaxLength(50)]
    [Column(TypeName = "nvarchar(50)")]
    public string Name { get; set; } = string.Empty;

    [MaxLength(50)]
    [Column(TypeName = "nvarchar(50)")]
    public string Description { get; set; } = string.Empty;

    
    public DateTimeOffset StartAt { get; set; }
    public DateTimeOffset EndAt { get; set; }
    
    public bool IsActive { get; set; }
}
