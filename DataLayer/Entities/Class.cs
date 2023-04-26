using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Entities;

[Index(nameof(Code), IsUnique = true)]
public class Class : BaseEntity
{
    #region Properties and Indexers
    [Required]
    public string Code { get; set; } = null!;

    [Required]
    public string Name { get; set; } = null!;

    public User Teacher { get; set; } = null!;
    public int TeacherId { get; set; }
    #endregion
}
